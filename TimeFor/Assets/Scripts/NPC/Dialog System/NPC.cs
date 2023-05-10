using UnityEngine;
using UnityEngine.AI;
using System.Collections.Generic;
using System.Collections;

public class NPC : MonoBehaviour
{
    [Header("Старт диалога")]
    public Dialog startDialog;

    private Dialog currentDialog;
    private DialogManager dialogManager;

    [Header("Components")]
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

        currentDialog = startDialog;
    }

    private void Update()
    {
        Time = Curve.Evaluate(UnityEngine.Time.deltaTime / Duraction);

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
        if (currentDialog.answers != null)
        {
            dialogManager.NextDialog();
        }
    }
}