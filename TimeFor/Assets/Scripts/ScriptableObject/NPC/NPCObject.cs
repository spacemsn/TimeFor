using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NPC", menuName = "Object/ItemObject/NPC")]
public class NPCObject : ItemObject
{
    [Header("ֱאחמגûו פנאחû")]
    [TextArea(3, 10)]
    public string[] lines;
}
