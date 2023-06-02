using Cinemachine;
using UnityEngine;
using UnityEngine.UI;

public class GloballSetting : MonoBehaviour
{
    [Header("EntryPoint")]
    public EntryPoint entryPoint;
    public PlayerEntryPoint playerEntry;
    public UIEntryPoint uIEntry;
    public GloballEntryPoint globallEntry;

    [Header("Панели")]
    [SerializeField] public PauseScript pauseScript;
    [SerializeField] public bookCharacter bookScript;
    [SerializeField] public DeathScript deathScript;

    [Header("Объекты")]
    public GameObject character;

    [Header("Управление курсором")]
    [SerializeField] bool isVisible = true;
    [SerializeField] public CinemachineFreeLook freeLook;

    public void EntryPoint(EntryPoint entryPoint)
    {
        this.entryPoint = entryPoint;
        playerEntry = entryPoint.player;
        uIEntry = entryPoint.UI;
        globallEntry = entryPoint.globallSetting;

        pauseScript = GetComponent<PauseScript>();
        deathScript = GetComponent<DeathScript>();

        freeLook = uIEntry.freeLook;
        deathScript.SetComponent(uIEntry.dealthPanel.gameObject);
    }

    private void Update()
    {
        if (character != null)
        {
            if (Input.GetKeyDown(KeyCode.Escape) && pauseScript.isOpenPanel == false)
            {
                OpenMenu(pauseScript.PausePanel, pauseScript.isOpenPanel); Visible(); pauseScript.OpenPanel();
            }
            else if (Input.GetKeyDown(KeyCode.Tab) && pauseScript.isOpenPanel == false)
            {
                bookScript.OpenInventory(); Visible();
            }
            else if ((Input.GetKeyDown(KeyCode.M) && pauseScript.isOpenPanel == false) || (Input.GetKeyDown(KeyCode.M) && pauseScript.isOpenPanel == true))
            {
                bookScript.OpenMap(); notVisible();
            }
            else if (Input.GetKeyDown(KeyCode.LeftAlt))
            {
                Visible();
            }
            else if (Input.GetKeyUp(KeyCode.LeftAlt))
            {
                notVisible();
            }
        }
    }

    public void Visible()
    {
        if (isVisible == false && freeLook != null)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            isVisible = true;
            freeLook.m_XAxis.m_InputAxisName = "";
            freeLook.m_YAxis.m_InputAxisName = "";
            freeLook.m_YAxis.m_InputAxisValue = 0;
            freeLook.m_XAxis.m_InputAxisValue = 0;
        }
    }

    public void notVisible()
    {
        if (isVisible && freeLook != null)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            isVisible = false;
            freeLook.m_XAxis.m_InputAxisName = "Mouse X";
            freeLook.m_YAxis.m_InputAxisName = "Mouse Y";
        }
    }

    public void OpenMenu(Transform panel, bool isOpenPanel)
    {
        if (isOpenPanel == false)
        {
            panel.gameObject.SetActive(true);
            isOpenPanel = true;
        }
        else if (isOpenPanel == true)
        {
            panel.gameObject.SetActive(false);
            isOpenPanel = false;
        }
    }
}
