using System.Collections.Generic;
using System.Collections;
using UnityEngine.UI;
using UnityEngine;
using TMPro;
using UnityEngine.AI;


public class NPCBehaviour : MonoBehaviour, IMoveBehavior
{
    [Header("NPC")]
    private NPCObject phrases;

    [Header("Components")]
    private Animator animator;
    private NavMeshAgent agent;

    [Header("Animation Curve")]
    [SerializeField] private AnimationCurve Curve;

    [Header("Dialog")]
    [SerializeField] private Transform dialoguePanel;
    [SerializeField] private TMP_Text nameNPC;
    [SerializeField] private TMP_Text dialogueText;
    private Queue<string> lines;

    [Range(0, 1)]
    public float Time;
    public float Duraction = 15f;

    private void Start()
    {
        phrases = Resources.Load<NPCObject>("NPC/NPC");
        animator = GetComponent<Animator>();

        nameNPC.text = phrases.name;
        dialogueText.text = string.Empty;
        dialogueText.gameObject.SetActive(false);
        lines = new Queue<string>();
    }

    public void StartDialog()
    {
        lines.Clear();
        dialogueText.gameObject.SetActive(true);

        foreach(string line in phrases.lines)
        {
            lines.Enqueue(line);
        }

        animator.SetFloat("TalkMotion", Time);
        StartCoroutine(WaitNextLine(3f));
    }

    public void DisplayNextLine()
    {
        animator.SetTrigger("Hello");
        animator.SetBool("Talk", true);

        if (lines.Count == 0)
        {
            EndDialogue();
            return;
        }
        else { StartCoroutine(WaitNextLine(3f)); }

        string line = lines.Dequeue();
        dialogueText.text = line;
    }

    public void EndDialogue()
    {
        animator.SetBool("Talk", false);
        animator.SetTrigger("GoodBye");
        dialogueText.text = string.Empty; ;
    }

    private void Update()
    {
        Time = Curve.Evaluate(UnityEngine.Time.deltaTime / Duraction);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            DisplayNextLine();
        }
    }

    IEnumerator WaitNextLine(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        DisplayNextLine();
    }

    public void Movement()
    {
        //agent.SetDestination();
    }
}
