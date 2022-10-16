using UnityEngine;

public class DayCycleManager : MonoCache
{
    [Range(0, 1)]
    public float TimeOfDay;
    public float DayDuraction = 120f;

    public AnimationCurve SunCurve;
    public AnimationCurve MoonCurve;
    public AnimationCurve SkyboxCurve;

    public Material DaySkybox;
    public Material NightSkybox;

    public Light Sun;
    public Light Moon;

    public ParticleSystem Stars;

    private float sunIntensity;
    private float moonIntensity;

    private void Start()
    {
        sunIntensity = Sun.intensity;
        moonIntensity = Moon.intensity;
    }

    void FixedUpdate()
    {
        TimeOfDay += Time.deltaTime / DayDuraction;

        if(TimeOfDay >= 1)
        {
            TimeOfDay -= 1;
        }

        RenderSettings.skybox = SkyboxCurve.Evaluate(TimeOfDay) > 0.5f ? DaySkybox : NightSkybox;
        RenderSettings.sun = SkyboxCurve.Evaluate(TimeOfDay) > 0.5f ? Sun : Moon;
        DynamicGI.UpdateEnvironment();

        var mainModule = Stars.main;
        mainModule.startColor = new Color(1, 1, 1, 1 - MoonCurve.Evaluate(TimeOfDay));

        transform.rotation = Quaternion.Euler(TimeOfDay * 360f, 0, 0);
        Sun.intensity = sunIntensity * SunCurve.Evaluate(TimeOfDay);
        Moon.intensity = moonIntensity * MoonCurve.Evaluate(TimeOfDay);
    }
}
