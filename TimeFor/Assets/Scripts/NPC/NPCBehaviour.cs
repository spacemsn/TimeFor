using System.Collections.Generic;
using System.Collections;
using UnityEngine.UI;
using UnityEngine;
using TMPro;
using UnityEngine.AI;


public class NPCBehaviour : MonoBehaviour, IMoveBehavior
{
    public string name;

    [Header("Старт диалога")]
    public Dialog startDialog;

    private Dialog currentDialog;
    private DialogManager dialogManager;

    [Header("Components")]
    public Transform NPC_UI;
    public Transform camera;
    private Animator animator;
    private NavMeshAgent agent;

    [Header("Animation Curve")]
    [SerializeField] private AnimationCurve Curve;

    [Range(0, 1)]
    public float Time;
    public float Duraction = 15f;

    void Start()
    {
        dialogManager = FindObjectOfType<DialogManager>();
        camera = GameObject.FindGameObjectWithTag("MainCamera").transform;

        startDialog.name = name;
        currentDialog = startDialog;
    }

    private void Update()
    {
        Time = Curve.Evaluate(UnityEngine.Time.deltaTime / Duraction);

    }

    private void LateUpdate()
    {
        NPC_UI.LookAt(camera.position, Vector3.up);
    }

    public string GetCurrentNameDialogText()
    {
        currentDialog.name = name;
        return currentDialog.name;
    }

    public string GetCurrentDialogText()
    {
        return currentDialog.dialogText;
    }

    public Answer[] GetAnswers()
    {
        return currentDialog.answers;
    }

    public void SetNextDialog(Answer answer)
    {
        currentDialog = answer.nextDialog;
        if (currentDialog.answers.Length > 0)
        {
            dialogManager.NextDialog();
        }
        else
        {
            dialogManager.EndDialog();
            currentDialog = startDialog;
        }
    }

    public void Movement()
    {

    }
}
