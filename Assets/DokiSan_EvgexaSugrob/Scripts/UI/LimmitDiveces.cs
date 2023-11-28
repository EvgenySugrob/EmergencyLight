using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LimmitDiveces : MonoBehaviour
{
    [SerializeField] Button button;
    [SerializeField] TMP_Text captionCount;

    private int allCount;
    [SerializeField] int countDevices;

    public void GetLimmit(int count)
    {
        Debug.Log(count);
        allCount = count;
        countDevices= count;
        if(allCount==0)
        {
            button.interactable = false;
        }
        captionCount.text = countDevices.ToString();
    }

    public void CalculationCount(int countObject)
    {
        countDevices = allCount - countObject;
        if (countDevices == 0)
        {
            button.interactable = false;
        }
        else
        {
            button.interactable = true;
        }
        captionCount.text = countDevices.ToString();
    }

}
