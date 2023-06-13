using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class mainCharacter : MonoCache
{
    [Header("EntryPoint")]
    public PlayerEntryPoint playerEntry;
    public UIEntryPoint uIEntry;

    [Header("Сохранение")]
    public SaveData saveData;

    [Header("Компоненты")]
    public attackCharacter attack;
    public indicatorCharacter indicators;
    public moveCharacter movement;
    public bookCharacter book;
    public DialogManager dialogManager;

    private void Start()
    {
        movement = this.GetComponent<moveCharacter>();
        attack = this.GetComponent<attackCharacter>();
        indicators = this.GetComponent<indicatorCharacter>();
        dialogManager = this.GetComponent<DialogManager>();
    }

    public void GetUI(PlayerEntryPoint player, UIEntryPoint uI)
    {
        this.playerEntry = player;
        this.uIEntry = uI;

        book = player.book;
    }
}
