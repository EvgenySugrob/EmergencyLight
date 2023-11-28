using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayNightStartSetting : MonoBehaviour
{
    [SerializeField] Light sunLight;
    [SerializeField] Material dayShaderGraph;
    [SerializeField] Material reserv;
    private bool isStart;

    private void Start()
    {
        Debug.Log(sunLight.transform.eulerAngles);
        DefaultStation();
    }

    public void DefaultStation()
    {
        RenderSettings.skybox = dayShaderGraph;
        sunLight.transform.localRotation = Quaternion.Euler(30f, 300f, 0);
        isStart= true;

        Debug.Log(sunLight.transform.eulerAngles);

        StartCoroutine(WaitToStopUpdate());
    }
    private void Update()
    {
        if (isStart)
        {
            DynamicGI.UpdateEnvironment();
        }
    }

    public void DefaultSkyBoxSet()
    {
        RenderSettings.skybox = reserv;
    }

    IEnumerator WaitToStopUpdate()
    {
        yield return new WaitForSeconds(0.6f);
        isStart = false;
    }

    public void SetNightTime()
    {
        DefaultSkyBoxSet();
        sunLight.transform.localRotation = Quaternion.Euler(-90f, 300f, 0);
        isStart = true;
        StartCoroutine(WaitToStopUpdate());
    }
}
