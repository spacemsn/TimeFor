using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogManager : MonoBehaviour
{
    public TMP_Text dialogText;
    public GameObject answerButtonPrefab;
    public Transform answerButtonParent;

    [SerializeField] private NPC npc;

    void Start()
    {
        npc = FindObjectOfType<NPC>();
    }

    public void StartDialog()
    {
        UpdateDialogUI();
    }

    public void NextDialog()
    {
        UpdateDialogUI();
    }

    void UpdateDialogUI()
    {
        dialogText.text = npc.GetCurrentDialogText();

        Answer[] answers = npc.GetAnswers();

        foreach (Transform child in answerButtonParent)
        {
            Destroy(child.gameObject);
        }

        for (int i = 0; i < answers.Length; i++)
        {
            AnswerButton answerButton = Instantiate(answerButtonPrefab, answerButtonParent).GetComponent<AnswerButton>();
            answerButton.Setup(answers[i]); answerButton.key = KeyCode.Alpha1 + i;
        }
    }
}
