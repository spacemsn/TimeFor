using Cinemachine;
using UnityEngine;

public class ScrollScript : MonoCache
{
    private CinemachineFreeLook freeLook;
    private GloballSetting globallSetting;

    private void Scrolling()
    {
        float mw = Input.GetAxis("Mouse ScrollWheel");
        if (mw > 0.1)
        {
            if (freeLook.m_Lens.FieldOfView >= 25) { freeLook.m_Lens.FieldOfView -= 5; }
        }
        else if (mw < -0.1)
        {
            if (freeLook.m_Lens.FieldOfView <= 45) { freeLook.m_Lens.FieldOfView += 5; }
        }

    }

    public void SetComponent(CinemachineFreeLook freeLook)
    {
        this.freeLook = freeLook;

        freeLook.m_XAxis.Value = 0.5f;
        freeLook.m_YAxis.Value = 0.5f;
    }

    private void Start()
    {
        globallSetting = GetComponent<GloballSetting>(); 
        freeLook = globallSetting.freeLook;
    }

    public override void OnUpdate()
    {
        Scrolling();
    }
}
