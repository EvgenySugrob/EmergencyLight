using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllOutlineCheckDevicesOff : MonoBehaviour
{
    [SerializeField] OutlineActivation outlineActivation;

    public void OutlineDeviceDisable()
    {
        outlineActivation.OutListOff();
    }
}
