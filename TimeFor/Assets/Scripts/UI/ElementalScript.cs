using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ElementalScript : GloballSetting
{
    [Header("Меню Респавна")]
    public Transform ElementalPanel;
    [SerializeField] public bool isOpenPanel = false;

    [SerializeField] private SettingsScript settingsMenu;
    public GloballSetting globall;

    [Header("Сохранение инвентаря")]
    public SaveData saveInventory;
    public SaveData defaultInventory;

    [Header("Элементы")]
    public ElementObject fire;
    public ElementObject water;
    public ElementObject air;
    public ElementObject terra;

    [Header("Выбранная стихия")]
    public ElementObject currentElement;

    [Header("Выбранная ячейка")]
    public CellObject currentCell;

    [Header("UI")]
    public TMP_Text AbilityPointsText;
    public TMP_Text AbilityNameText;
    public TMP_Text AbilityDescriptionText;

    public Transform AbilityPointsPanel;
    public Transform AbilityNamePanel;
    public Transform AbilityDescriptionPanel;

    public void OpenPanel()
    {
        isOpenPanel = !isOpenPanel;
    }

    private void OnEnable()
    {
        indicatorCharacter.onLevelUp += ViewPoint;
        SpawnContoller.onPlayerSceneLoaded += ViewPoint;
    }

    private void OnDisable()
    {
        indicatorCharacter.onLevelUp -= ViewPoint;
        SpawnContoller.onPlayerSceneLoaded -= ViewPoint;
    }

    private void ViewPoint()
    {
        if(globall.player != null)
        {
            AbilityPointsText.text = "Очки способности: " + globall.player.GetComponent<indicatorCharacter>().elementalPoints.ToString();
        }
    }

    public void ViewElement(ElementObject element)
    {
        currentElement = element;

        AbilityNameText.text = element.name;
        AbilityDescriptionText.text = element.aboutItem;
    }
    public void ViewCell(CellObject cell)
    {
        currentCell = cell;

        AbilityNameText.text = cell.name;
        AbilityDescriptionText.text = cell.aboutItem;
    }

    public void Upgrade()
    {
        if(globall.player.GetComponent<indicatorCharacter>().elementalPoints > 0)
        {
            currentCell.Upgrade();
            globall.player.GetComponent<indicatorCharacter>().elementalPoints--;
            ViewPoint();
        }
    }

    public void Back()
    {
        OpenMenu(elementalScript.ElementalPanel, elementalScript.isOpenPanel);

        if (elementalScript.isOpenPanel)
        {
            elementalScript.OpenPanel(); ResumeGame(); notVisible();
        }
        else if (!elementalScript.isOpenPanel)
        {
            elementalScript.OpenPanel(); PauseGame(); Visible();
        }
    }
}
