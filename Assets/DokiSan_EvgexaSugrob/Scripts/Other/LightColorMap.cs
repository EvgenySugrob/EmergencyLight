using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class LightColorMap : MonoBehaviour
{
    [SerializeField] ScriptableRendererFeature scriptableRenderer;
    private void OnDisable()
    {
        scriptableRenderer.SetActive(false);
    }
    public void ColorMapActive(bool isOn)
    {
        scriptableRenderer.SetActive(isOn);
    }
}
