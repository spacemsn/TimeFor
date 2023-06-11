using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIEntryPoint : MonoBehaviour
{
    public GameObject CanvasPrefab;
    public GameObject currentCanvas;

    public Transform headPlayer;
    public Transform centerPlayer;
    public Transform bottomPlayer;

    [Header("UI компоненты")]
    public Transform IndecatorsPanel;
    public Transform SkillsPanel;
    public Transform MiniMapPanel;

    public Transform QuickslotPanel;

    public Slider healthBar;
    public Slider staminaBar;

    public SkillSlot WaterSlot;
    public SkillSlot FireSlot;
    public SkillSlot AirSlot;
    public SkillSlot TerraSlot;

    public Image reactionImage;
    public TMP_Text damageText;

    [Header("Menu")]
    public Transform pausePanel;
    public Transform SettingPanel;
    public Transform dealthPanel;

    [Header("Настройки")]
    [Header("Mouse")]
    public Slider SensitivityYSlider;
    public Slider SensitivityXSlider;

    [Header("Book")]
    public Transform bookPanel;
    public Transform playerPage;
    public Transform mapPage;
    public Transform inventoryPage;

    public Transform inventoryPanel;
    public Transform mapPanel;

    [Header("DialogPanel")]
    public Transform DialogPanel;
    public TMP_Text dialogText;
    public TMP_Text nameDialogText;
    public Transform answerButtonParent;
    public Transform BackgroundDialogUp;
    public Transform BackgroundDialogDown;

    [Header("ItemPanel")]
    public Transform ItemPanel;
    public Transform buttonParent;

    private void Start()
    {
        SpawnContoller.onPlayerSceneLoaded += ActiveCanvas;
    }

    private void OnDisable()
    {
        SpawnContoller.onPlayerSceneLoaded -= ActiveCanvas;
    }

    public void ActiveCanvas()
    {
        if (SpawnContoller.isPlayerSceneLoaded)
        {
            PlayerEntryPoint playerEntry = GetComponent<PlayerEntryPoint>(); 
            currentCanvas.GetComponent<Canvas>().renderMode = RenderMode.ScreenSpaceCamera;
            currentCanvas.GetComponent<Canvas>().worldCamera = playerEntry.currentCamera;
            currentCanvas.gameObject.SetActive(true);
        }
        else
        {
            currentCanvas.gameObject.SetActive(false);
        }
    }
}
