using UnityEngine;

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
    public GameObject[] enemiesToKill;
    public int enemiesToKillCount;

    // Для задания "собрать предметы"
    public GameObject[] itemsToCollect;
    public int itemsToCollectCount;
}
