using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AnswerButton : MonoBehaviour
{
    public Text answerText;
    private NPCBehaviour npc;
    private Answer answer;

    public KeyCode key;

    void Start()
    {
        npc = FindObjectOfType<NPCBehaviour>();
    }

    public void Setup(Answer currentAnswer)
    {
        answer = currentAnswer;
        answerText.text = currentAnswer.answerText;
    }

    public void OnButtonClick()
    {
        npc.SetNextDialog(answer);
    }

    private void Update()
    {
        if (Input.GetKeyDown(key))
        {
            npc.SetNextDialog(answer);
        }
    }
}

