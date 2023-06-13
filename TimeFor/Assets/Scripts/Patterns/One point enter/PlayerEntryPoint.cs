using Cinemachine;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class PlayerEntryPoint : MonoBehaviour
{
    public static Action<mainCharacter, indicatorCharacter, moveCharacter> onSavePlayer;
    public static Action<mainCharacter, indicatorCharacter, moveCharacter> onLoadPlayer;

    public GameObject playerPrefab;
    public GameObject currentPlayer;
    public SaveData saveData;

    [Header("Камера")]
    public GameObject cameraPrefab;
    public Camera currentCamera;

    [Header("Виртуальная камера")]
    public GameObject freeLookPrefab;
    public CinemachineFreeLook currentFreeLook;

    [Header("Скрипты")]
    public attackCharacter attack;
    public indicatorCharacter indicators;
    public mainCharacter main;
    public moveCharacter movement;
    public bookCharacter book;
    public DialogManager dialogManager;
    public QuestManager questManager;

    [Header("GameManagers")]
    public UIEntryPoint UIPoint;
    public GloballEntryPoint globallEntry;
    public EnemyGameManager enemyManager;

    private void Start()
    {
        UIPoint = GetComponent<UIEntryPoint>();
        globallEntry = GetComponent<GloballEntryPoint>();
        enemyManager = GetComponent<EnemyGameManager>();

        SpawnContoller.onPlayerSceneSaved += SavePlayerData;
        SpawnContoller.onPlayerSceneLoaded += LoadSaveData;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F5))
        {
            SavePlayerData();
        }
        if (Input.GetKeyDown(KeyCode.F6))
        {
            LoadSaveData();
        }
    }

    public void GetComponents()
    {
        attack = currentPlayer.GetComponent<attackCharacter>();
        indicators = currentPlayer.GetComponent<indicatorCharacter>();
        main = currentPlayer.GetComponent<mainCharacter>();
        movement = currentPlayer.GetComponent<moveCharacter>();
        dialogManager = currentPlayer.GetComponent<DialogManager>();
        questManager = currentPlayer.GetComponent<QuestManager>();


        attack.GetUI(this, UIPoint);
        indicators.GetUI(this, UIPoint);
        main.GetUI(this, UIPoint);
        movement.GetUI(this, UIPoint);
        dialogManager.GetUI(this, UIPoint);

        globallEntry.globall.character = currentPlayer;

        currentFreeLook.gameObject.transform.position = currentPlayer.transform.position;
        currentFreeLook.gameObject.transform.rotation = currentPlayer.transform.rotation;

        currentFreeLook.GetComponent<CinemachineFreeLook>().Follow = currentPlayer.transform; 
        currentFreeLook.GetComponent<CinemachineFreeLook>().LookAt = currentPlayer.transform;

        UIPoint.headPlayer = currentPlayer.transform.GetChild(0); currentFreeLook.GetComponent<CinemachineFreeLook>().GetRig(0).m_LookAt = UIPoint.headPlayer;
        UIPoint.centerPlayer = currentPlayer.transform.GetChild(1); currentFreeLook.GetComponent<CinemachineFreeLook>().GetRig(1).m_LookAt = UIPoint.centerPlayer;
        UIPoint.bottomPlayer = currentPlayer.transform.GetChild(2); currentFreeLook.GetComponent<CinemachineFreeLook>().GetRig(2).m_LookAt = UIPoint.bottomPlayer;
    }

    private void SavePlayerData()
    {
        if (currentPlayer != null)
        {
            onSavePlayer += saveData.SetSave;

            onSavePlayer.Invoke(main, indicators, movement);

            onSavePlayer -= saveData.SetSave;
        }
    }

    private void LoadSaveData()
    {
        if (currentPlayer != null && !SpawnContoller.firstLoadScene)
        {
            onLoadPlayer += saveData.LoadSave;

            onLoadPlayer.Invoke(main, indicators, movement);

            onLoadPlayer -= saveData.LoadSave;
        }
    }
}
