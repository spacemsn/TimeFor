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

        notVisible();

        freeLook = uIEntry.freeLook;
        deathScript.SetComponent(uIEntry.dealthPanel.gameObject);
        pauseScript.SetComponent(uIEntry.pausePanel.gameObject);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && pauseScript.isOpenPanel == false)
        {
            pauseScript.OpenMenu();
        }
        else if (Input.GetKeyDown(KeyCode.Tab) && pauseScript.isOpenPanel == false)
        {
            bookScript.OpenInventory();
        }
        else if ((Input.GetKeyDown(KeyCode.M) && pauseScript.isOpenPanel == false) || (Input.GetKeyDown(KeyCode.M) && pauseScript.isOpenPanel == true))
        {
            bookScript.OpenMap();
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
}
