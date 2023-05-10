using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[Serializable]
public class DialogText
{
    [Header("Фразы")]
    [TextArea(3, 10)]
    public string[] text;
}
