using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogManager : MonoBehaviour
{
    [Header("EntryPoint")]
    public EntryPoint entryPoint;
    public PlayerEntryPoint playerEntry;
    public UIEntryPoint uIEntry;

    [Header("Quest Manager")]
    public QuestManager questManager;
    public Quest currentQuest;


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

    public void GetUI(PlayerEntryPoint player, UIEntryPoint uI)
    {
        this.playerEntry = player;
        this.uIEntry = uI;

        dialogText = uI.dialogText;
        nameDialogText = uI.nameDialogText;
        answerButtonParent = uI.answerButtonParent;
}

    public void StartDialog(NPCBehaviour currentNpc)
    {
        npc = currentNpc;
        UpdateDialogUI();

        uIEntry.BackgroundDialogUp.gameObject.SetActive(true);
        uIEntry.BackgroundDialogDown.gameObject.SetActive(true);
        uIEntry.ItemPanel.gameObject.SetActive(false);

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
        uIEntry.BackgroundDialogUp.gameObject.SetActive(false);
        uIEntry.BackgroundDialogDown.gameObject.SetActive(false);
        uIEntry.ItemPanel.gameObject.SetActive(true);
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
            answerButton.Setup(answers[i]); answerButton.key = KeyCode.Space;
        }
    }
}
