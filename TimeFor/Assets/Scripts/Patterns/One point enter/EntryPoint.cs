using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EntryPoint : MonoBehaviour
{
    private static EntryPoint instance;

    [Header("Игровые компоненты")]
    public PlayerEntryPoint player;
    public EnemyGameManager enemies;

    [Header("Компоненты")]
    public GloballEntryPoint globallSetting;

    [Header("UI")]
    public UIEntryPoint UI;


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            Initialize();
        }
        else
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }

    private void Initialize()
    {
        player = GetComponent<PlayerEntryPoint>();
        globallSetting = GetComponent<GloballEntryPoint>();
        UI = GetComponent<UIEntryPoint>();
        enemies = GetComponent<EnemyGameManager>();
    }
}

