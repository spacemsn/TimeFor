using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "New Quest", menuName = "Quest System/Quest")]
public class Quest : ScriptableObject
{
    public string questTitle;
    public string questDescription;

    public enum QuestType
    {
        KillEnemies,
        CollectItems
    }

    public QuestType questType;

    // Для задания "убить врагов"
    public List<GameObject> enemiesToKill;
    public int enemiesToKillCount;

    // Для задания "собрать предметы"
    public List<GameObject> itemsToCollect;
    public int itemsToCollectCount;

    // отслеживание програссе игрока по выполнению задания
    public int progressQuestCount;

    [Header("Награда за выполнения задания")]
    public int experiencePoints;
    public GameObject rewardItem;
}
