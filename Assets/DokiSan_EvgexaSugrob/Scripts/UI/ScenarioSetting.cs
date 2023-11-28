using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;
using DoKiSan.Controls;
using System;
using Unity.Mathematics;
using MyBox;
using Vmaya.UI.Components;

public class ScenarioSetting : MonoBehaviour
{
    [Header("Принимаемые лимиты сценария")]
    [SerializeField] TMP_InputField emergencyLightCountInput;
    [SerializeField] TMP_InputField avtonomEmergencyLightCountInput;
    [SerializeField] TMP_InputField batteryCountInput;
    [SerializeField] TMP_InputField additionalBatteryInput;
    private int emergencyLightCount;
    private int avtonomEmergencyLightCount;

    [Header("Параметры для расчета\nначального времени работы")]
    [Tooltip("Средняя нагрузка одной лампы")]
    [SerializeField] float avgAmperOneLamp = 0.5f;
    [Tooltip("КПД")]
    [Range(0.1f,1f)]
    [SerializeField] float efficiency;
    [Tooltip("Емкость одного АКБ")]
    [SerializeField] private float capacityOfOneBattery = 7;
    [Tooltip("Напряжение одного АКБ")]
    [SerializeField] private float batteryVoltage = 24;
    [Tooltip("Минимальное среднее время работы от АКБ в часах")]
    [SerializeField] private float avgMinHoursWork = 2f;
    [SerializeField] private float timeLeft;
    private float allAmpere;
    private float powerConsumption;
    private int batteryCount = 1;
    private int additionalBattery = 0;
    private float avgHoursWork = 0;
    


    [Header("Передача лимитов")]
    [SerializeField] List<LimmitDiveces> limmitDivecesList;

    [Header("Включается после подтверждения лимитов")]
    [SerializeField] CharacterController characterController;
    [SerializeField] FirstPlayerControl firstPlayerControl;
    [SerializeField] SelectableObjectInteract selectableObjectInteract;
    [SerializeField] InventoryReplaceItem inventoryReplaceItem;

    [SerializeField] GameObject scenarioSettingWindow;

    [Header("для анимации полей")]
    [SerializeField] PlaceholderAnim countBatteryInputAnim;

    public void SetSettingScenario()
    {
        if (CheckInputForCorrect())
        {
            emergencyLightCount = int.Parse(emergencyLightCountInput.text);
            avtonomEmergencyLightCount = int.Parse(avtonomEmergencyLightCountInput.text);
            additionalBattery = int.Parse(additionalBatteryInput.text);

            CalculationForInputValues();

            limmitDivecesList[0].GetLimmit(emergencyLightCount);
            limmitDivecesList[1].GetLimmit(avtonomEmergencyLightCount);
            scenarioSettingWindow.SetActive(false);

            characterController.enabled= true;
            firstPlayerControl.enabled= true;
            inventoryReplaceItem.enabled = true;
            selectableObjectInteract.enabled = true;
        }
        else
        {
            Debug.Log("Поля заполни");//выводить сообщение о том, что небходимо заполнить поля
        }
       
    }
    public void EndEditCountLamp()
    {
        if(!emergencyLightCountInput.text.IsNullOrEmpty())
        {
            emergencyLightCount = int.Parse(emergencyLightCountInput.text);
            if (emergencyLightCount != 0)
            {
                countBatteryInputAnim.PlayAnimIfNotNull(true);
                CalculationForInputValues();
            }
        }
        
    }
    private void CalculationForInputValues()
    {
        avgHoursWork = 0;
        batteryCount = 0;

        allAmpere = emergencyLightCount * avgAmperOneLamp;
        powerConsumption = allAmpere * batteryVoltage;

        while (avgHoursWork<avgMinHoursWork)
        {
            batteryCount++;
            avgHoursWork = (capacityOfOneBattery * batteryCount * batteryVoltage) / (powerConsumption * efficiency);
        }

        float printTime = (float)Math.Round(avgHoursWork,2);
        avgHoursWork = (float)Math.Round(avgHoursWork, 2);
        timeLeft = avgHoursWork;

        Debug.Log("Часы: " + printTime + "\nМинуты: " + Mathf.Round(printTime * 60) + "\nКол-во АКБ: "+batteryCount);

        batteryCountInput.text = batteryCount.ToString();
    }
    public float RecalculationTime()
    {
        int numberInstaledLamps = inventoryReplaceItem.ReturnInstalledLamp();
        Debug.Log(numberInstaledLamps);

        if (numberInstaledLamps !=0)
        {
            float allLampAmper = inventoryReplaceItem.TotalLoadOfInstalledLamps();

            Debug.Log(allLampAmper);

            allAmpere = numberInstaledLamps * allLampAmper;

            Debug.Log(allAmpere);
            powerConsumption = allLampAmper * batteryVoltage;

            timeLeft = (capacityOfOneBattery * batteryCount * batteryVoltage) / (powerConsumption * efficiency);

            timeLeft = (float)Math.Round(timeLeft, 2);
            Debug.Log("Время работы в часах будет: " + timeLeft);
            Debug.Log("Время работы в минутах будет: " + Mathf.Round(timeLeft * 60));
        }
        return timeLeft;
    }
    private bool CheckInputForCorrect()
    {
        bool isCorrect= false;

        if(emergencyLightCountInput.text=="" && avtonomEmergencyLightCountInput.text=="")
        {
            isCorrect = false;
        }
        else
        {
            isCorrect= true;
        }

        return isCorrect;
    }
}
