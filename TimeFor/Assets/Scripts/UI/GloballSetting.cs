using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GloballSetting : MonoCache
{
    InventoryPanel inventoryPanel;
    PauseMenu pauseMenu;

    private void Start()
    {
        inventoryPanel = GetComponent<InventoryPanel>();
        pauseMenu = GetComponent<PauseMenu>();
    }

    public override void OnTick()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) OpenPause();
        else if (Input.GetKeyDown(KeyCode.Tab)) OpenInventory(); 
    }

    void OpenPause()
    {
        pauseMenu.OpenMenu();
    }

    void OpenInventory()
    {
        if (pauseMenu.isOpenPanel == false) inventoryPanel.OpenInventory();
    }
}
