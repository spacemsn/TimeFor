using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogManager : MonoBehaviour
{
    public TMP_Text dialogText;
    public TMP_Text nameDialogText;
    public GameObject answerButtonPrefab;
    public Transform answerButtonParent;

    [SerializeField] private NPCBehaviour npc;
    [SerializeField] private GameObject player;

    void Start()
    {
        player = this.gameObject;
    }

    public void StartDialog(NPCBehaviour currentNpc)
    {
        npc = currentNpc;
        UpdateDialogUI();


        player.GetComponent<moveCharacter>().isManagement = false;
    }

    public void NextDialog()
    {
        UpdateDialogUI();
    }

    public void EndDialog()
    {
        nameDialogText.text = "";
        dialogText.text = "";

        foreach (Transform child in answerButtonParent)
        {
            Destroy(child.gameObject);
        }

        player.GetComponent<moveCharacter>().isManagement = true;
    }

    void UpdateDialogUI()
    {
        nameDialogText.text = npc.GetCurrentNameDialogText();
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
