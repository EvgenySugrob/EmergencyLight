using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainSettingCustomDevices : MonoBehaviour
{
    [Header("»сточник света устройства")]
    [SerializeField] Light spotLight;

    [Space(5)]
    [Header("«амена на свет€щийс€ материал")]
    [SerializeField] MeshRenderer svetilnicGlass;
    [SerializeField] Material defaultMaterial;
    [SerializeField] Material emissionMaterial;

    [Space(5)]
    [SerializeField] string countIntensity = "250";
    private int intensity = 250;
    [SerializeField] bool spotLightIsActive;
    [SerializeField] float currentPower;
    [SerializeField] float ampere = 0.083f;
    [SerializeField] float powerAkb = 24f;
    [SerializeField] float hoursLeft; //это только дл€ автономного источника
    bool isCheck = false;
    private int sliderValuer = 0;

    [Space(5)]
    [SerializeField] float power;
    [SerializeField] ScenarioSetting scenarioSetting;

    private void Start()
    {
        scenarioSetting = FindObjectOfType<ScenarioSetting>();
        power = ampere * powerAkb;
    }
    public int GetCurrentSliderValue()
    {
        return sliderValuer;
    }

    public float GetIntensityLight()
    {
        return spotLight.intensity;
    }
    public float GetAngleLight()
    {
        return spotLight.spotAngle;
    }
    public string GetIntensityText()
    {
        return countIntensity;
    }
    public int GetIntensityInt()
    {
        return intensity;
    }
    public string GetPower()
    {
        power = (float)Math.Round(power,2);
        return power.ToString();
    }
    public float GetAmpere()
    {
        return ampere;
    }

    public void SetIntensityLight(float intensity,int textIntensity, int currentSliderValue)
    {
        spotLight.intensity = intensity;
        intensity = textIntensity;
        countIntensity = textIntensity.ToString();
        sliderValuer = currentSliderValue;
    }
    public void SetAngleLight(float angle)
    {
        spotLight.spotAngle = angle;
        spotLight.innerSpotAngle = spotLight.spotAngle - 50f;
    }
    public void SetAmpere(float ampereSet)
    {
        ampere = ampereSet;
        float roundPower = ampere * powerAkb;
        power = (float)Math.Round(roundPower,2);
    }

    public void CheckJobDevices()
    {
        if(isCheck)
        {
            spotLight.enabled= false;

            svetilnicGlass.material = defaultMaterial;
            isCheck=false;
        }
        else
        {
            spotLight.enabled = true;

            svetilnicGlass.material = emissionMaterial;
            isCheck=true;
        }
    }

    public void LastLampDisableCheck()
    {
        if(isCheck)
        {
            spotLight.enabled = false;

            svetilnicGlass.material = defaultMaterial;
            isCheck = false;
        }
    }

    public bool IsCheckOrNo()
    {
        return isCheck;
    }

    public void LampOn(bool isOn)
    {
        if (isOn)
        {
            spotLight.enabled = true;

            svetilnicGlass.material = emissionMaterial;
        }
        else 
        {
            spotLight.enabled = false;

            svetilnicGlass.material = defaultMaterial;
        }
    }

}
