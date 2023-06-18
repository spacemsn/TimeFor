using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class NPCGameManager : MonoBehaviour
{
    [Header("??????? ??????????")]
    public PlayerEntryPoint player;
    public UIEntryPoint ui;

    [Header("????? ?? ?????")]
    public List<GameObject> NPC;

    private void Start()
    {
        player = GetComponent<PlayerEntryPoint>();
        ui = GetComponent<UIEntryPoint>();

        SpawnContoller.onPlayerSceneLoaded += GetNPCComponent;
    }

    private void OnDisable()
    {
        if (!SpawnContoller.isPlayerSceneLoaded)
        {
            SpawnContoller.onPlayerSceneLoaded -= GetNPCComponent;
        }
    }

    public void GetNPCComponent()
    {
        NPC = GameObject.FindGameObjectsWithTag("NPC").ToList();

        if (NPC.Count > 0)
        {
            foreach (GameObject npc in NPC)
            {
                npc.GetComponent<NPCBehaviour>().camera = player.currentCamera.transform;
                npc.GetComponent<NPCBehaviour>().buttonParent = ui.buttonParent;
            }
        }
    }
}