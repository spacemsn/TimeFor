using Cinemachine;
using UnityEngine;
using UnityEngine.UI;

public class SettingsScript : MonoBehaviour
{
    [Header("Компоненты")]
    [SerializeField] private GloballSetting settings;
    [SerializeField] private PauseScript pauseScript;
    [SerializeField] private CinemachineFreeLook freeLook;

    [Header("Настройки")]
    [SerializeField] private SettingsObject Default;
    [SerializeField] private SettingsObject Settings;
    [SerializeField] private GameObject SettingsPanel;
    [SerializeField] private GameObject PausePanel;
    public bool isOpenPanel = true;
    private float _SensitivityY;
    private float _SensitivityX;

    [Header("UI")]
    [SerializeField] private Slider SensitivityYSlider;
    [SerializeField] private Slider SensitivityXSlider;

    private void Start()
    {
        settings = GetComponent<GloballSetting>();
        pauseScript = GetComponent<PauseScript>();
    }

    public void SetComponent(GameObject SettingsPanel, GameObject PausePanel, CinemachineFreeLook freeLook, Slider _SensitivityY, Slider _SensitivityX)
    {
        this.SettingsPanel = SettingsPanel;
        this.PausePanel = PausePanel;
        this.freeLook = freeLook;
        this.SensitivityYSlider = _SensitivityY;
        this.SensitivityXSlider = _SensitivityX;

        LoadSettings();
        OpenMenu();
    }

    public void LoadSettings()
    {
        freeLook.m_YAxis.m_MaxSpeed = Settings.SensitivityY;
        freeLook.m_XAxis.m_MaxSpeed = Settings.SensitivityX;

        SensitivityYSlider.value = Settings.SensitivityY;
        SensitivityXSlider.value = Settings.SensitivityX;
    }

    public void SaveSettings()
    {
        Settings.SensitivityY = SensitivityYSlider.value;
        Settings.SensitivityX = SensitivityXSlider.value;

        freeLook.m_YAxis.m_MaxSpeed = Settings.SensitivityY;
        freeLook.m_XAxis.m_MaxSpeed = Settings.SensitivityX;
    }

    public void SetDefault()
    {
        freeLook.m_YAxis.m_MaxSpeed = Default.SensitivityY;
        freeLook.m_XAxis.m_MaxSpeed = Default.SensitivityX;

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
        pauseScript.OpenMenu();
        OpenMenu();
    }

}
