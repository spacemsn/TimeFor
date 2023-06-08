using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static QuestManager;

public class QuestReward : MonoBehaviour
{ 
    [Header("Задание")]
    public Quest quest;

    [Header("Награды за выполнения задания")]
    public int experiencePoints = 100;
    public GameObject rewardItem;

    private void Start()
    {
        // Подписываемся на событие OnQuestCompleted
        QuestManager.onQuestCompleted += GiveReward;
    }

    private void OnDestroy()
    {
        // Отписываемся от события OnQuestCompleted при уничтожении объекта
        QuestManager.onQuestCompleted -= GiveReward;
    }

   
    private void GiveReward(Quest quest)
    {
        // Даем игроку очки опыта
        //other.GetComponent<Player>().AddExperience(experiencePoints);

        // Даем игроку новый предмет
        if (rewardItem != null)
        {
            // добавляем предметы в инвентарь или создаем предметы 
        }

        // Выполняем условия после выполнения задания
        //GameManager.instance.CompleteQuest();

        this.quest = quest;
        experiencePoints = quest.experiencePoints;
        rewardItem = quest.rewardItem;
    }
}
