using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class GetMainLightDirection : MonoBehaviour
{
    [SerializeField] Material skyboxMaterialNight;
    [SerializeField] Material skyboxMaterialDay;
    [SerializeField] Material reserv;

    private void Update()
    {

        skyboxMaterialDay.SetVector("_MainLightDirection", transform.forward);
        reserv.SetVector("_MainLightDirection", transform.forward);
    }
}
