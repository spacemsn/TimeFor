using Cinemachine;
using UnityEngine;

public class MouseVisible : MonoCache
{
    [Header("��������� ����")]
    [SerializeField] bool isVisible = true;

    public CinemachineFreeLook freeLook;

    private void Start()
    {
        Visible();
    }

    public override void OnTick()
    {
        OnEnterButton();
    }

    public void Visible()
    {
        if(isVisible)
        { 
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            isVisible = false;
            freeLook.m_XAxis.m_InputAxisName = "Mouse X";
            freeLook.m_YAxis.m_InputAxisName = "Mouse Y";
        }
        else if(!isVisible)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            isVisible = true;
            freeLook.m_XAxis.m_InputAxisName = "";
            freeLook.m_XAxis.m_InputAxisValue = 0;
            freeLook.m_YAxis.m_InputAxisName = "";
            freeLook.m_YAxis.m_InputAxisValue = 0;
        }

    }

    void OnEnterButton()
    {
        if (Input.GetKeyDown(KeyCode.LeftAlt)) { Visible(); }
        else if(Input.GetKeyUp(KeyCode.LeftAlt)) { Visible(); }
    }
}
