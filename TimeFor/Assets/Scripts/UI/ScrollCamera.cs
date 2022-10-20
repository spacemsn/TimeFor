using Cinemachine;
using UnityEngine;

public class ScrollCamera : MonoCache
{
    public CinemachineFreeLook freeLook;

    private void Scroll()
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

    private void Start()
    {
        freeLook.m_XAxis.Value = 0.5f;
        freeLook.m_YAxis.Value = 0.5f;
    }

    public override void OnTick()
    {
        Scroll();
    }
}
