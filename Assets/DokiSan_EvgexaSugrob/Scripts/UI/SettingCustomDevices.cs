using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Vmaya.UI.Components;

public class SettingCustomDevices : MonoBehaviour
{
    [Header("Заголовок окна")]
    [SerializeField] GameObject settingLampWindow;
    [SerializeField] TMP_Text titleWindow;

    [Space(10)]
    [Header("Настройка устройства")]
    [SerializeField] Slider intensitySlider;
    [SerializeField] TMP_Text intensityValueText;
    [SerializeField] Slider angleSlider;
    [SerializeField] TMP_Text angleValueText;

    [Space(10)]
    [Header("Информация о времени работы и потребляемой мощности")]
    [SerializeField] TMP_Text currentPowerText;
    [SerializeField] TMP_Text hoursLeftText;

    [Space(10)]
    [Header("Принимаемый объект")]
    [SerializeField] GameObject recevidObject;

    [SerializeField, HideInInspector]
    MainSettingCustomDevices mainSettingCustomDevices;

    [Space(5)]
    [SerializeField] DialogsMenuReplace dialogsMenuReplace;
    [SerializeField] ScenarioSetting scenarioSetting;

    private int intens;

    public void GetDevices(GameObject gameObject)
    {
        recevidObject = gameObject.GetComponent<ItemParent>().GetParent();
        mainSettingCustomDevices = recevidObject.GetComponent<MainSettingCustomDevices>();
        OpenSettingWindow(true);
    }

    public void OpenSettingWindow(bool isOn)
    {
        if(isOn)
        {
            //установка свойст от объекта
            SetValuesInSettingWindow();
            settingLampWindow.SetActive(true);

        }
        else
        {
            settingLampWindow.GetComponent<Window>().hide();
            OffLastLamp();

            dialogsMenuReplace.OpenCloseDialogsWindow(isOn);
        }
    }

    public void SliderIntensityValue(float value)
    {
        switch (value)
        {
            case 0:
                intens = 250;
                intensityValueText.text = "250";
                mainSettingCustomDevices.SetIntensityLight(10, intens,0);
                mainSettingCustomDevices.SetAmpere(0.083f);

                break;

            case 1:
                intens = 300;
                intensityValueText.text = "300";
                mainSettingCustomDevices.SetIntensityLight(15, intens,1);
                mainSettingCustomDevices.SetAmpere(0.167f);
                break;

            case 2:
                intens = 450;
                intensityValueText.text = "450";
                mainSettingCustomDevices.SetIntensityLight(25, intens,2);
                mainSettingCustomDevices.SetAmpere(0.25f);
                break;

            case 3:
                intens = 600;
                intensityValueText.text = "600";
                mainSettingCustomDevices.SetIntensityLight(30, intens,3);
                mainSettingCustomDevices.SetAmpere(0.333f);
                break;

            case 4:
                intens = 900;
                intensityValueText.text = "900";
                mainSettingCustomDevices.SetIntensityLight(60, intens,4);
                mainSettingCustomDevices.SetAmpere(0.416f);
                break;

            case 5:
                intens = 1100;
                intensityValueText.text = "1100";
                mainSettingCustomDevices.SetIntensityLight(80, intens,5);
                mainSettingCustomDevices.SetAmpere(0.5f);
                break;

            case 6:
                intens = 1250;
                intensityValueText.text = "1250";
                mainSettingCustomDevices.SetIntensityLight(95, intens, 6);
                mainSettingCustomDevices.SetAmpere(0.583f);
                break;

            case 7:
                intens = 1400;
                intensityValueText.text = "1400";
                mainSettingCustomDevices.SetIntensityLight(110, intens, 7);
                mainSettingCustomDevices.SetAmpere(0.667f);
                break;
        }
        HoursLeftCalculation();
    }
    public void SliderAngleIntensity(float value)
    {
        mainSettingCustomDevices.SetAngleLight(value);
        angleValueText.text = Mathf.Round(value).ToString();
    }

    public void ButtonCheckClick()
    {
        if (recevidObject!=null)
        {
            mainSettingCustomDevices.CheckJobDevices();
        }
    }

    public void OffLastLamp()
    {
        if(recevidObject != null)
        {
            mainSettingCustomDevices.LastLampDisableCheck();
        }
    }

    private void SetValuesInSettingWindow()
    {
        
        titleWindow.text = recevidObject.name.ToString();
        intensitySlider.value = mainSettingCustomDevices.GetCurrentSliderValue();
        angleSlider.value = mainSettingCustomDevices.GetAngleLight();

        intensityValueText.text = mainSettingCustomDevices.GetIntensityText();
        angleValueText.text = mainSettingCustomDevices.GetAngleLight().ToString();

        currentPowerText.text = mainSettingCustomDevices.GetPower() + " Вт"; // брать из MainSettingCustomDevices

        HoursLeftCalculation();
        Debug.Log(titleWindow.text);
        Debug.Log(intensitySlider.value);
        Debug.Log(angleSlider.value);
        Debug.Log(intensityValueText.text);
        Debug.Log(angleValueText.text);
 
    }

    private void HoursLeftCalculation()
    {
        ItemsForReplace itemsForReplace = recevidObject.GetComponent<ItemsForReplace>();
        currentPowerText.text = mainSettingCustomDevices.GetPower() + " Вт";
        if (itemsForReplace.ReturnDivecesType() == ItemsForReplace.TypeDiveces.MainLamp)
        {
            hoursLeftText.text = (scenarioSetting.RecalculationTime() + " ч").ToString();// брать из MainSettingCustomDevices
        }
        else if (itemsForReplace.ReturnDivecesType() == ItemsForReplace.TypeDiveces.AvtonomLamp)
        {
            hoursLeftText.text = "2 ч";// брать из MainSettingCustomDevices
        }
    }

    public void ClsoeWindowSettingCustomDevices()
    {

    }
}
