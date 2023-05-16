using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "New Answer", menuName = "Dialog System/Answer")]
public class Answer : ScriptableObject
{
    public string name;
    public string answerText;
    public Dialog nextDialog;
    public Quest quest;
}
