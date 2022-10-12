using UnityEngine;

public class PointLight : MonoBehaviour
{

    public Light pointLight;
    private float pointIntensity;

    DayCycleManager dayCycleManager;

    void Start()
    {
        pointLight = this.gameObject.GetComponent<Light>();
        pointIntensity = pointLight.intensity;
        dayCycleManager = GameObject.Find("DayCycleManager").GetComponent<DayCycleManager>();
    }
void Update()
{
        pointLight.intensity = pointIntensity * dayCycleManager.MoonCurve.Evaluate(dayCycleManager.TimeOfDay);
    }
}
