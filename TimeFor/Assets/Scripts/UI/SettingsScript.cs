using Cinemachine;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Controlls
{
    [Header("Mouse")]
    public float SensitivityY;
    public float SensitivityX;

    public Controlls(float sensitivityY, float sensitivityX)
    {
        this.SensitivityY = sensitivityY;
        this.SensitivityX = sensitivityX;
    }
}

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
    public bool isOpenPanel = true;

    [Header("UI")]
    [SerializeField] private Slider SensitivityYSlider;
    [SerializeField] private Slider SensitivityXSlider;

    private void Start()
    {
        // Load saved settings data
        //Default = Resources.Load<SettingsObject>("Settings/Defaunt Settings");
        //Settings = Resources.Load<SettingsObject>("Settings/Settings");

        #region Get components

        settings = GetComponent<GloballSetting>();
        pauseScript = GetComponent<PauseScript>();

        if ((GameObject.FindGameObjectWithTag("FreeLook")) != null)
        {
            freeLook = GameObject.FindGameObjectWithTag("FreeLook").GetComponent<CinemachineFreeLook>();
            freeLook.m_YAxis.m_MaxSpeed = Settings.SensitivityY;
            freeLook.m_XAxis.m_MaxSpeed = Settings.SensitivityX;
        }

        #endregion

        SensitivityYSlider.value = Settings.SensitivityY;
        SensitivityXSlider.value = Settings.SensitivityX;
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
        pauseScript.OpenMenu();
        OpenMenu();
    }

}
