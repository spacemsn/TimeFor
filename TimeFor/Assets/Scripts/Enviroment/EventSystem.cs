using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EventSystem : MonoBehaviour
{
    private BoxCollider portal;

    private void Start()
    {
        // Подписываемся на событие OnQuestCompleted
        QuestManager.onQuestCompleted += Open;

        portal = gameObject.GetComponent<BoxCollider>();
        portal.enabled = false;
    }

    private void OnDestroy()
    {
        // Подписываемся на событие OnQuestCompleted
        QuestManager.onQuestCompleted -= Open;
    }

    private void Open(Quest quest)
    {
        Debug.Log("Дверь открытлась");
        portal.enabled = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}
