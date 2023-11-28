using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuAnimationBackground : MonoBehaviour
{
    [SerializeField] Animator animator;

    [Header("DisableLightAll")]
    [SerializeField] List<GameObject> lightList;
    [SerializeField] GameObject globalLight;
    [SerializeField] DayNightStartSetting dayNightStartSetting;
    [SerializeField] Material materialDefault;
    [SerializeField] Material materialLight;
    [SerializeField] List<MeshRenderer> officeLampList;
    [SerializeField] List<GameObject> fakeLamps;
    [Header("PCScreenDisable")]
    [SerializeField] MeshRenderer pcScreenRender;
    [SerializeField] Material pcMaterilaDefault;
    [SerializeField] Material pcMaterialActive;
    [Header("DisableOther")]
    [SerializeField] GameObject notUseObj;
    [SerializeField] GameObject rotationObject;
    [SerializeField] GameObject cameraAnimation;

    private Coroutine coroutine;
    private Coroutine coroutineDisableLight;
    [SerializeField] private bool isNightActive;

    private Quaternion startRotationCameraAnim;
    private Vector3 startPosition;

    private void Start()
    {
        startRotationCameraAnim = cameraAnimation.transform.rotation;
        startPosition = cameraAnimation.transform.position;
    }
    public void NightOnOff(bool status)
    {
        if (status)
        {
            animator.SetBool("IsFade", true);
        }
        else
        {
            animator.SetBool("IsFade",false);
            ReturnToRotation();
        }
        NightTimeAndEnableLamps(status);
    }
    
    public void NightTimeAndEnableLamps(bool status)
    {

        isNightActive= status;
        

        if (status)
        {
            coroutineDisableLight = StartCoroutine(WaitForDisableLamp());

            dayNightStartSetting.SetNightTime();

        }
        else
        {
            foreach (GameObject lamp in fakeLamps)
            {
                lamp.SetActive(!isNightActive);
            }
            foreach (GameObject light in lightList)
            {
                light.SetActive(isNightActive);
            }
            foreach (MeshRenderer mesh in officeLampList)
            {
                mesh.material = materialLight;
            }
            pcScreenRender.material = pcMaterialActive;
            dayNightStartSetting.DefaultStation();

            transform.GetComponent<RotationCameraMenu>().ReturnStartPosition();
            rotationObject.SetActive(true);
            cameraAnimation.SetActive(false);
            cameraAnimation.transform.position = startPosition;
            cameraAnimation.transform.rotation = startRotationCameraAnim;
        }

    }
    IEnumerator WaitLampsOn()
    {
        yield return new WaitForSeconds(2.5f);
        foreach (GameObject light in lightList)
        {
            light.SetActive(isNightActive);
        }
    }
    IEnumerator WaitForDisableLamp()
    {
        yield return new WaitForSeconds(4f);


        if (isNightActive)
        {
            pcScreenRender.material = pcMaterilaDefault;
            foreach (MeshRenderer mesh in officeLampList)
            {
                mesh.material = materialDefault;
            }
            foreach (GameObject lamp in fakeLamps)
            {
                lamp.SetActive(!isNightActive);
            }
            StartCoroutine(WaitLampsOn());
            //foreach (GameObject light in lightList)
            //{
            //    light.SetActive(isNightActive);
            //}

        }

        coroutine = StartCoroutine(WaitToAnimationStart());
    }
    IEnumerator WaitToAnimationStart()
    {
        yield return new WaitForSeconds(1.5f);
        Debug.Log("Старт анимации основной");
        animator.SetBool("isPlay",true);
    }
    public void ReturnToRotation()
    {
        animator.SetBool("isPlay",false);
    }

    public void FadeOff()
    {
        Debug.Log("Ивент затухания");
        animator.SetBool("isFade",false);
    }
}
