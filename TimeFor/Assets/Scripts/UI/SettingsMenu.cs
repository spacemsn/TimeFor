using Cinemachine;
using UnityEngine;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    [Header("Настройки")]
    [SerializeField] private SettingsObject Default;
    [SerializeField] private SettingsObject Settings;
    [SerializeField] private Transform SettingsPanel;
    [SerializeField] private Transform PausePanel;
    public bool isOpenPanel = false;
    private float _SensitivityY;
    private float _SensitivityX;

    [Header("UI")]
    [SerializeField] private Slider SensitivityYSlider;
    [SerializeField] private Slider SensitivityXSlider;

    [Header("Компоненты")]
    [SerializeField] private PauseMenu pauseMenu;
    [SerializeField] private CinemachineFreeLook freeLook;

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
        pauseMenu.OpenMenu();
        OpenMenu();
    }

    private void Start()
    {
        SensitivityYSlider = GameObject.Find("SensitivityY").GetComponent<Slider>();
        SensitivityXSlider = GameObject.Find("SensitivityX").GetComponent<Slider>();
        freeLook = GameObject.FindGameObjectWithTag("FreeLook").GetComponent<CinemachineFreeLook>();

        pauseMenu = GetComponent<PauseMenu>();

        LoadSettings();
    }
}
