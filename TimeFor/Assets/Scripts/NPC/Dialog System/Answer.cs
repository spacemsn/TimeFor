using UnityEngine;

[CreateAssetMenu(fileName = "New Answer", menuName = "Dialog System/Answer")]
public class Answer : ScriptableObject
{
    public string answerText;
    public Dialog nextDialog;
    public Quest quest;
}
