using Cinemachine;
using UnityEngine;

public class PlayerEntryPoint : MonoBehaviour
{
    public GameObject playerPrefab;
    public GameObject currentPlayer;
    public SaveData saveData;

    [Header("Скрипты")]
    public attackCharacter attack;
    public indicatorCharacter indicators;
    public moveCharacter movement;
    public interactionCharacter interaction;
    public bookCharacter book;
    public DialogManager dialogManager;

    [Header("GameManagers")]
    public UIEntryPoint UIPoint;
    public GloballEntryPoint globallEntry;
    public EnemyGameManager enemyManager;

    private void Start()
    {
        UIPoint = GetComponent<UIEntryPoint>();
        globallEntry = GetComponent<GloballEntryPoint>();
        enemyManager = GetComponent<EnemyGameManager>();

        if(currentPlayer != null && saveData.savedPlayers.Count > 0)
        {
            LoadSaveData();
        }
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

        if (currentPlayer == null)
        {
            SpawnPlayer();
        }
    }

    public void SpawnPlayer()
    {
        GameObject spawn = GameObject.FindGameObjectWithTag("Spawn Point");
        if(spawn != null)
        {
            currentPlayer = Instantiate(playerPrefab, spawn.transform.position, spawn.transform.rotation);
            GetComponents();
        }
    }

    public void GetComponents()
    {
        attack = currentPlayer.GetComponent<attackCharacter>();
        indicators = currentPlayer.GetComponent<indicatorCharacter>();
        movement = currentPlayer.GetComponent<moveCharacter>();
        interaction = currentPlayer.GetComponent<interactionCharacter>();
        book = globallEntry.globall.bookScript.GetComponent<bookCharacter>();
        dialogManager = currentPlayer.GetComponent<DialogManager>();

        attack.GetUI(this, UIPoint);
        indicators.GetUI(this, UIPoint);
        interaction.GetUI(this, UIPoint);
        movement.GetUI(this, UIPoint);
        dialogManager.GetUI(this, UIPoint);

        globallEntry.globall.character = currentPlayer;

        UIPoint.freeLook.Follow = currentPlayer.transform;
        UIPoint.freeLook.LookAt = currentPlayer.transform;

        UIPoint.headPlayer = currentPlayer.transform.GetChild(0); UIPoint.freeLook.GetRig(0).m_LookAt = UIPoint.headPlayer;
        UIPoint.centerPlayer = currentPlayer.transform.GetChild(1); UIPoint.freeLook.GetRig(1).m_LookAt = UIPoint.centerPlayer;
        UIPoint.bottomPlayer = currentPlayer.transform.GetChild(2); UIPoint.freeLook.GetRig(2).m_LookAt = UIPoint.bottomPlayer;

        enemyManager.GetEnemyComponent();
    }

    private void SavePlayerData()
    {
        var player = currentPlayer.GetComponent<mainCharacter>();
        saveData.SetSave(player, player.indicators, player.movement);
    }

    private void LoadSaveData()
    {
        var player = currentPlayer.GetComponent<mainCharacter>();
        saveData.LoadSave(player, player.indicators, player.movement);
    }
}
