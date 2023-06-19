using UnityEngine;

[CreateAssetMenu(fileName = "Attack", menuName = "Object/ItemObject/Attack")]
public class skillItem : Item
{
    [Header("Характеристики скилла")]
    [Header("Стихия")]
    public Elements element;
    [Header("Прокачиваемая стихия")]
    public ElementObject elementObject;
    [Header("Откат способности")]
    public float attackRollback;
    [Header("Скорость")]
    public float speed;
}
