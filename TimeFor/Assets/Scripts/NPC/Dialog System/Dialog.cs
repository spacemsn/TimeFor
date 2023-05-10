using UnityEngine;

[CreateAssetMenu(fileName = "New Dialog", menuName = "Dialog System/Dialog")]
public class Dialog : ScriptableObject
{
    [TextArea(3, 10)]
    public string dialogText;

    public Answer[] answers;
}

