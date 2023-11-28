using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TableNewMaterialSet : MonoBehaviour
{
    [SerializeField] MeshRenderer meshRenderer;
    [SerializeField] Material materialTable;
    [SerializeField] Color noEmissionColor;
    [SerializeField] Color emissionColor;

    public void GetMaterialForReplace(Material newMaterial)
    {
        meshRenderer.material= newMaterial;
    }

    public void EmmisionTableActivation(bool isOn)
    {
        if (isOn) 
        {
            materialTable = meshRenderer.material;
            materialTable.SetColor("_EmissionColor", emissionColor);
        }
        else
        {
            materialTable.SetColor("_EmissionColor", noEmissionColor);
        }
       
    }
}
