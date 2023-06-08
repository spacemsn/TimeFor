using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using UnityEngine;

public class DayCycleManager : MonoBehaviour
{
    [Range(0, 1)]
    public float TimeOfDay;
    public float DayDuration = 120f;
    public float TimeScale = 1f;

    public AnimationCurve SunCurve;
    public AnimationCurve MoonCurve;
    public AnimationCurve SkyboxCurve;

    [Header("Skyboxes")]
    public List<Material> SkyBoxDay;
    public List<Material> SkyBoxNight;

    public Light Sun;
    public Light Moon;

    private float sunIntensity;
    private float moonIntensity;

    private int currentSkyboxIndex;

    public enum TimeDay
    {
        Day, 
        Night,
    }
    [Header("Режим дня")]
    public TimeDay timeDay;

    private void Start()
    {
        sunIntensity = Sun.intensity;
        moonIntensity = Moon.intensity;

        currentSkyboxIndex = 0;
        SetSkybox(currentSkyboxIndex);

        TimeOfDay = 0.5f; 
    }

    private void Update()
    {
        float timeIncrement = Time.deltaTime * TimeScale;
        TimeOfDay += timeIncrement;

        if (TimeOfDay >= 1f)
        {
            TimeOfDay -= 1f;
            currentSkyboxIndex = (currentSkyboxIndex + 1) % SkyBoxDay.Count;
            SetSkybox(currentSkyboxIndex);
        }

        RenderSettings.skybox = SkyboxCurve.Evaluate(TimeOfDay) > 0.5f ? SkyBoxDay[currentSkyboxIndex] : SkyBoxNight[currentSkyboxIndex];
        RenderSettings.sun = SkyboxCurve.Evaluate(TimeOfDay) > 0.5f ? Sun : Moon;
        DynamicGI.UpdateEnvironment();

        transform.rotation = Quaternion.Euler(TimeOfDay * 360f, 0, 0);
        Sun.intensity = sunIntensity * SunCurve.Evaluate(TimeOfDay);
        Moon.intensity = moonIntensity * MoonCurve.Evaluate(TimeOfDay);

        if(SkyboxCurve.Evaluate(TimeOfDay) > 0.5f) { timeDay = TimeDay.Day; }
        else if(SkyboxCurve.Evaluate(TimeOfDay) < 0.5f) { timeDay = TimeDay.Night; }
    }

    private void SetSkybox(int index)
    {
        RenderSettings.skybox = SkyboxCurve.Evaluate(TimeOfDay) > 0.5f ? SkyBoxDay[index] : SkyBoxNight[index];
        RenderSettings.sun = SkyboxCurve.Evaluate(TimeOfDay) > 0.5f ? Sun : Moon;
        DynamicGI.UpdateEnvironment();
    }
}
