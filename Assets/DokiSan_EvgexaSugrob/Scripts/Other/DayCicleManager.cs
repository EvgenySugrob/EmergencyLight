using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayCicleManager : MonoBehaviour
{
    [Range(0, 1)]
    [SerializeField] float timeOfDay;
    [SerializeField] float dayDuration = 30f;

    [SerializeField] AnimationCurve sunCurve;


    [SerializeField] Material dayMaterial;
    [SerializeField] Material nightMaterial;
    [SerializeField] Material reservMaterial;
    [SerializeField] Color dayColorGround;
    [SerializeField] Color nightColorGround;
    [SerializeField] Color dayColorHorizont;
    [SerializeField] Color nightColorHorizont;

    [SerializeField] private Color currentColorGround;
    [SerializeField] private Color currentColotHorizont;

    [SerializeField] Light sunLight;
    private float sunIntensity;
    [SerializeField] private Material skyboxMaterial;

    [Header("Связанно персонажем и проверкой")]
    [SerializeField] StartCheck floorSelect;
    [SerializeField] ScenarioResultCheck scenarioResultCheck;

    private Vector3 nightPoint = new Vector3(-90f, 150f, 0f);
    private bool isStart;

    private bool isAutomatic;
   [SerializeField] private bool inRightPosition;
   [SerializeField] private bool isDay;
    private float startTimeOfDay = 0.4f;

    private void Start()
    {

        skyboxMaterial = RenderSettings.skybox;

        sunIntensity = sunLight.intensity;

        RenderSettings.skybox = skyboxMaterial;
    }

    private void Update()
    {
        timeOfDay += Time.deltaTime / dayDuration;

        if (timeOfDay >= 1)
            timeOfDay -= 1;

        currentColorGround = Color.Lerp(nightColorGround, dayColorGround, sunCurve.Evaluate(timeOfDay));
        currentColotHorizont = Color.Lerp(nightColorHorizont, dayColorHorizont, sunCurve.Evaluate(timeOfDay));

        skyboxMaterial.Lerp(nightMaterial, dayMaterial, sunCurve.Evaluate(timeOfDay));

        DynamicGI.UpdateEnvironment();

        sunLight.transform.localRotation = Quaternion.Euler(timeOfDay * 360f, 300, 0);
        sunLight.intensity = sunIntensity * sunCurve.Evaluate(timeOfDay);

        if (isDay && !inRightPosition)
        {
            DayTimeAllready();
        }
        else
        {
            NightTimeAllready();
        }
        
    }
    private void DayTimeAllready()
    {
        if (timeOfDay < 0.41f && timeOfDay > 0.4)
        {
            enabled = false;
            isDay = false;
            scenarioResultCheck.FPSViewEditorReturn();
        }
    }

    private void NightTimeAllready()
    {
        if (timeOfDay > 0.749f && timeOfDay < 0.75f)
        {
            enabled = false;
            inRightPosition = true;

            floorSelect.TopDownViewEnabledInCheck();
            if (isAutomatic)
            {
                scenarioResultCheck.AutomaticEnabled(true);
            }
            else
            {
                scenarioResultCheck.AutomaticEnabled(false);

            }
            scenarioResultCheck.ManualCheckTopDownViewEnable();
            scenarioResultCheck.ManualCheckStart(true);

        }
    }
    public void DayTimeNeeds(bool isActive)
    {
        dayDuration = 30;
        isDay = isActive;
        inRightPosition = false;
        enabled = true;
    }
    public void SetTypeCheck(bool isAutoType)
    {
        dayDuration = 40;
        isAutomatic = isAutoType;
    }
    public void SetReservSkybox()
    {
        RenderSettings.skybox = reservMaterial;
    }
    public void TimeInRightPosition()
    {
        if (inRightPosition)
        {
            floorSelect.TopDownViewEnabledInCheck();
            if (isAutomatic)
            {
                scenarioResultCheck.AutomaticEnabled(true);
            }
            else
            {
                scenarioResultCheck.AutomaticEnabled(false);
            }
            scenarioResultCheck.ManualCheckTopDownViewEnable();
        }
        else
        {
            enabled = true;
        }
    }
}
