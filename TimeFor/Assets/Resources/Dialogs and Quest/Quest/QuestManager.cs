using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{ 
    [Header("Задание")]
    public Quest currentQuest;

    public static Action<Quest> onQuestCompleted;
    public bool isQuestCompleted = false;

    // Для задания "убить врагов"
    public List<GameObject> enemiesToKill;
    public int enemiesToKillCount;

    // Для задания "собрать предметы"
    public List<GameObject> itemsToCollect;
    public int itemsToCollectCount;

    public int progressQuestCount;

    public void GetQuest(Quest quest)
    {
        this.currentQuest = quest;

        switch (currentQuest.questType)
        {
            case Quest.QuestType.KillEnemies:
                enemiesToKill = currentQuest.enemiesToKill;
                enemiesToKillCount = currentQuest.itemsToCollectCount;
                break;

            case Quest.QuestType.CollectItems:
                itemsToCollect = currentQuest.itemsToCollect;
                itemsToCollectCount = currentQuest.itemsToCollectCount;
                break;
        }

        progressQuestCount = currentQuest.progressQuestCount;
    }

    private void Update()
    {
        if (currentQuest != null)
        {
            if (currentQuest.questType == Quest.QuestType.KillEnemies && progressQuestCount >= enemiesToKillCount)
            {
                CompleteQuest();
            }
            else if (currentQuest.questType == Quest.QuestType.CollectItems && progressQuestCount >= itemsToCollectCount)
            {
                CompleteQuest();
            }
        }
    }

    public void KillEnemy(GameObject currentEnemy)
    {
        foreach(GameObject enemy in enemiesToKill)
        {
            if(enemy == currentEnemy)
            {
                progressQuestCount++;
            }
        }
    }

    public void CollectItems(GameObject currentItem)
    {
        foreach (GameObject item in itemsToCollect)
        {
            if (item == currentItem)
            {
                progressQuestCount++;
            }
        }
    }

    public void CompleteQuest()
    {
        // Выполняем действия для завершения задания
        isQuestCompleted = true;
        onQuestCompleted(currentQuest);
    }
}
