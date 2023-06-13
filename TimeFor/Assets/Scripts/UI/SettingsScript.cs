using Cinemachine;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using System;

[Serializable]
public class Controlls
{
    [Header("Настройка мыши")]
    [SerializeField] private float SensitivityY;
    [SerializeField] private float SensitivityX;

    [Header("Настройка звука")]
    [SerializeField] private float MusicValue;
    [SerializeField] private float SoundValue;
    [SerializeField] private bool allSound;

    public Controlls(SettingsObject settings)
    {
        SensitivityY = settings.SensitivityY;
        SensitivityX = settings.SensitivityX;

        MusicValue = settings.MusicValue;
        SoundValue = settings.SoundValue;
        allSound = settings.isMute;
    }

    public void LoadControlls(SettingsObject settings)
    {
        settings.SensitivityY = SensitivityY;
        settings.SensitivityX = SensitivityX;

        settings.MusicValue = MusicValue;
        settings.SoundValue = SoundValue;
        settings.isMute = allSound;
    }
}

public class SettingsScript : MonoBehaviour
{
    public static Action onSaveSetting;
    public static Action onLoadSetting;

    public EntryPoint entryPoint;

    [Header("Компоненты")]
    [SerializeField] private GloballSetting setting;
    [SerializeField] private PauseScript pauseScript;
    [SerializeField] private CinemachineFreeLook freeLook;

    [Header("Настройки")]
    [SerializeField] private SettingsObject Default;
    [SerializeField] private SettingsObject Settings;
    [SerializeField] private GameObject SettingsPanel;
    public bool isOpenPanel = true;

    [Header("Настройка мыши")]
    public Slider SensitivityYSlider;
    public Slider SensitivityXSlider;

    [Header("Настройка звука")]
    public AudioMixerGroup MusicMixed;
    public AudioMixerGroup SoundMixed;
    public Slider MusicValue;
    public Slider SoundValue;
    public bool isMute;

    private void Start()
    {
        #region Get components

        setting = GetComponent<GloballSetting>(); entryPoint = setting.entryPoint; SettingsPanel = entryPoint.UI.SettingPanel.gameObject;
        pauseScript = GetComponent<PauseScript>();

        SpawnContoller.onPlayerSceneLoaded += UpdateStatus;

        #endregion
    }

    private void OnDisable()
    {
        SpawnContoller.onPlayerSceneLoaded -= UpdateStatus;
    }

    public void UpdateStatus()
    {
        if (SpawnContoller.isPlayerSceneLoaded)
        {
            freeLook = entryPoint.player.currentFreeLook;
            freeLook.m_YAxis.m_MaxSpeed = Settings.SensitivityY;
            freeLook.m_XAxis.m_MaxSpeed = Settings.SensitivityX;

            SensitivityYSlider = entryPoint.UI.SensitivityYSlider;
            SensitivityXSlider = entryPoint.UI.SensitivityXSlider;

            SensitivityYSlider.value = Settings.SensitivityY;
            SensitivityXSlider.value = Settings.SensitivityX;

            Settings.LoadSave(this); Debug.Log("Настройки загрузились");
        }
        else
        {
            SettingsPanel.gameObject.SetActive(false);
            isOpenPanel = false;
        }
    }

    public void SaveSettings()
    {
        Settings.SensitivityY = SensitivityYSlider.value;
        Settings.SensitivityX = SensitivityXSlider.value;

        if (freeLook != null)
        {
            freeLook.m_YAxis.m_MaxSpeed = Settings.SensitivityY;
            freeLook.m_XAxis.m_MaxSpeed = Settings.SensitivityX;
        }

        Settings.SetSave(this);
    }

    public void SetDefault()
    {
        if (freeLook != null)
        {
            freeLook.m_YAxis.m_MaxSpeed = Default.SensitivityY;
            freeLook.m_XAxis.m_MaxSpeed = Default.SensitivityX;
        }

        SensitivityYSlider.value = Default.SensitivityY;
        SensitivityXSlider.value = Default.SensitivityX;
    }    

    public void OpenMenu()
    {
        if (isOpenPanel == false)
        {
            SettingsPanel.gameObject.SetActive(true);
            isOpenPanel = true;
        }
        else if (isOpenPanel == true)
        {
            SettingsPanel.gameObject.SetActive(false);
            isOpenPanel = false;
        }
    }

    public void Back()
    {
        setting.OpenMenu(pauseScript.PausePanel, pauseScript.isOpenPanel); pauseScript.OpenPanel();
        OpenMenu();
    }

    public void OnMuteMusic(bool mute)
    {
        isMute = mute;
        if (isMute) 
        { 
            MusicMixed.audioMixer.SetFloat("Mute", 0);
            SoundMixed.audioMixer.SetFloat("Mute", 0);
        }
        else if(!isMute)
        { 
            MusicMixed.audioMixer.SetFloat("Mute", -80);
            SoundMixed.audioMixer.SetFloat("Mute", -80); 
        }
    }

    public void ChangeMusic(float volume)
    {
        MusicMixed.audioMixer.SetFloat("Music", Mathf.Lerp(-80, 0, volume));
    }

    public void ChangeSound(float volume)
    {
        SoundMixed.audioMixer.SetFloat("Effect", Mathf.Lerp(-80, 0, volume));
    }
}
