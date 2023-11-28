using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateGI : MonoBehaviour
{
    public void UdpateGi()
    {
        DynamicGI.UpdateEnvironment();
    }
}
