using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class LuxmetrCalculate : MonoBehaviour
{
    [Header("Списки ламп")]
    [SerializeField] InventoryReplaceItem inventoryReplaceItem;

    [Header("Настройка параметров")]
    [SerializeField] Transform rayCheckPosition;
    [SerializeField] float rayDistance=10;
    [SerializeField] float waitSecond=1;
    [SerializeField] private List<GameObject> lampsList;
    [SerializeField] private List<GameObject> availabilityLamp;
    private Coroutine coroutine;

    [Header("Вывод значения")]
    [SerializeField] TMP_Text resultText;
    private float resultLux;

    public void AddItemForList(GameObject lampItem)
    {
        lampsList.Add(lampItem);
    }
    public void RemoveItemForList(GameObject lampItem)
    {
        lampsList.Remove(lampItem);
    }
    public void CreatingShareList()
    {
        //lampsList = inventoryReplaceItem.GetLampList();
        //lampsList.AddRange(inventoryReplaceItem.GetAvtonomLampList());
        foreach (GameObject lamp in lampsList)
        {
            lamp.GetComponent<LampLightPowerCalculate>().SetLuxmetr(transform.gameObject);
        }
        StartCorutineMetering();
    }

    public void StartCorutineMetering()
    {
        coroutine = StartCoroutine(StartMetering());
    }
    public void StopCorutineMetering()
    {
        //lampsList.Clear();
        if(coroutine!= null)
        {
            StopCoroutine(coroutine);
        }
    }
    IEnumerator StartMetering()
    {
        yield return new WaitForSeconds(waitSecond);
        AvailabilityСheck();
        coroutine = StartCoroutine(StartMetering());
    }
    private void AvailabilityСheck()
    {
        resultLux= 0;
        foreach(GameObject lamp in lampsList)
        {
            rayCheckPosition.LookAt(lamp.transform.position);

            RaycastHit hit;
            if (Physics.Raycast(rayCheckPosition.position, rayCheckPosition.forward, out hit, rayDistance))
            {
                if (hit.transform.GetComponent<ItemParent>())
                {
                    if (!availabilityLamp.Contains(lamp))
                    {
                        availabilityLamp.Add(lamp);
                    }
                }
                else
                {
                    if (availabilityLamp.Contains(lamp))
                    {
                        availabilityLamp.Remove(lamp);
                    }
                    Debug.Log(lamp.name + " перекрыт и не попадает в датчик");
                }
            }
        }
        foreach (GameObject lamp in availabilityLamp)
        {
            resultLux += lamp.GetComponent<LampLightPowerCalculate>().GetPowerLightFlowLamp();
        }
        Debug.Log(resultLux);
        float result = (float)Math.Round(resultLux,2);
        resultText.text = (result + " lux").ToString();
    }
}
