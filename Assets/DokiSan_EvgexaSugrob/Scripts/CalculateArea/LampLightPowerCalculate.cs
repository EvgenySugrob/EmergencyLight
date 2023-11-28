using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LampLightPowerCalculate : MonoBehaviour
{
    [Header("Люксометр")]
    [SerializeField] GameObject luxmetr;

    [Header("Параметры источника света")]
    [SerializeField] MainSettingCustomDevices mainSettingCustomDevices;

    [Header("Переменые для расчета формул")]
    [SerializeField] GameObject pointForDistance;
    private float hightOverWorkPoint;
    private float distanceOfWorkPoint;
    private float tgAlpha;
    private float alphaAngle;
    private float powerLightFlowLamp1000;
    private float powerLightFlowLamp;
    [SerializeField] private float intencityIlluminationLamp;
    private const float COEF_K = 1.5f;
    private const float COEF_M = 1.05f;

    [SerializeField]private float[] angleArray = new float[21] {0,5,15,25,35,45,55,65,75,85,90,95,105,115,125,135,145,155,165,175,180};
    [SerializeField]private float[] powerLightArray = new float[21] {284,280,277,258,228,181,106,56,26,6,2,4,4,4,5,5,5,4,4,3,3};

    private Vector3 pointPosition;


    private void Start()
    {
        mainSettingCustomDevices = GetComponent<MainSettingCustomDevices>();
    }

    public void SetLuxmetr(GameObject luxmetrOrigin)
    {
        luxmetr = luxmetrOrigin;
    }

    public void CalculationLampIllumination()
    {
        hightOverWorkPoint = transform.position.y - luxmetr.transform.position.y;

        pointPosition = new Vector3(transform.position.x,hightOverWorkPoint,transform.position.z);

        distanceOfWorkPoint = Vector3.Distance(luxmetr.transform.position,pointPosition);
        Debug.Log(distanceOfWorkPoint + " растояние до лампы");
        tgAlpha = hightOverWorkPoint / distanceOfWorkPoint;
        alphaAngle = (float)(Math.Atan(tgAlpha)*180/Math.PI);

        for (int i = 0; i < angleArray.Length; i++)
        {
            if (alphaAngle > angleArray[i])
            {
                powerLightFlowLamp1000 = Mathf.Lerp(powerLightArray[i], powerLightArray[i+1],0.65f)*1000;
                break;
            }
        }

        powerLightFlowLamp = (powerLightFlowLamp1000 * mainSettingCustomDevices.GetIntensityInt())/1000;

        intencityIlluminationLamp = (float)((powerLightFlowLamp * Math.Pow(Math.Cos(30),3)*COEF_M) / (COEF_K * hightOverWorkPoint));
    }

    public float GetPowerLightFlowLamp()
    {
        CalculationLampIllumination();
        return intencityIlluminationLamp;
    }
}
