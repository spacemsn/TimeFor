using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Element", menuName = "Element")]
public class ElementObject : Item
{
    [Header("Тип стихии")]
    public Elements element;

    [Header("Тип стихии")]
    public float baseDamage;

    [Header("Тип стихии")]
    public float basePersent;

    public float Formule()
    {

        return 0;
    }
}

