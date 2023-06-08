using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AnswerButton : MonoBehaviour
{
    public Text answerText;
    private NPCBehaviour npc;
    [SerializeField] private Answer answer;
    [SerializeField] private Quest quest;

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
        quest = currentAnswer.quest;    
    }

    public void OnButtonClick()
    {
        npc.SetNextDialog(answer);

        if(answer.quest != null)
        {
            quest = answer.quest;
            npc.SetQuest(quest);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(key) && isSelected)
        {
            OnButtonClick();
        }
    }

    public void isSelect()
    {
        isSelected = !isSelected;
    }
}

