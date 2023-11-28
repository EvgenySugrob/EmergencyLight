using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutlineActivation : MonoBehaviour
{
    [SerializeField] List<Outline> outlineObject;
    [SerializeField] ScenarioResultCheck scenarioResultCheck;
    [SerializeField] bool devices;

    private void Start()
    {
        if (devices)
            scenarioResultCheck = FindObjectOfType<ScenarioResultCheck>();
    }

    public void ActivationOutline()
    {
        if (devices)
        {
            if (!scenarioResultCheck.ReturnIsCheckProgress())
            {
                OutListOn();
            }
        }
        else
        {
            OutListOn();
        }
       
    }
    public void DisableOutline()
    {
        if(devices)
        {
            if(!scenarioResultCheck.ReturnIsCheckProgress())
            {
                OutListOff();
            }
        }
        else
        {
            OutListOff();
        }
    }
    public void OutListOn()
    {
        foreach (Outline outline in outlineObject)
        {
            outline.enabled = true;
        }
    }
    public void OutListOff()
    {
        foreach (Outline outline in outlineObject)
        {
            outline.enabled = false;
        }
    }
   
}
