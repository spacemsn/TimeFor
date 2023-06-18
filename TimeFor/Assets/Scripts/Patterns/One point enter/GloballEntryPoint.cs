using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GloballEntryPoint : MonoBehaviour
{
    public GameObject GameObjectPrefab;
    public GameObject currentGameObject;

    [Header("Скрипт")]
    public GloballSetting globall;
    public PauseScript pause;
    public DeathScript death;
    public SettingsScript settings;

    [Header("GameManagers")]
    public EntryPoint entryPoint;

    private void Start()
    {
        entryPoint = GetComponent<EntryPoint>();
        if(currentGameObject == null)
        {
            InstantiateObject();
        }
        else
        {
            globall = currentGameObject.GetComponent<GloballSetting>();
            pause = currentGameObject.GetComponent<PauseScript>();
            death = currentGameObject.GetComponent<DeathScript>();
            settings = currentGameObject.GetComponent<SettingsScript>();
        }
    }

    public void InstantiateObject()
    {
        currentGameObject = Instantiate(GameObjectPrefab) as GameObject;
        globall = currentGameObject.GetComponent<GloballSetting>();
        pause = currentGameObject.GetComponent<PauseScript>();
        death = currentGameObject.GetComponent<DeathScript>();
        settings = currentGameObject.GetComponent<SettingsScript>();
    }
}
