using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AnswerButton : MonoBehaviour
{
    public Text answerText;
    private NPCBehaviour npc;
    private Answer answer;

    public KeyCode key;
    public bool isSelected = false;

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
        if (Input.GetKeyDown(key) && isSelected)
        {
            npc.SetNextDialog(answer);
        }
    }

    public void isSelect()
    {
        isSelected = !isSelected;
    }
}

