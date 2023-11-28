using DoKiSan.Controls;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScenarioResultCheck : MonoBehaviour
{
    [Header("Лампы офиса")]
    [SerializeField] Material lampsMaterial;
    [SerializeField] Material lampsMaterialNoLight;
    [SerializeField] List<MeshRenderer> meshRenderersLampList;

    [Space(10)]
    [Header("Дневной цикл")]
    [SerializeField] DayCicleManager dayCicleManager;
    [SerializeField] GameObject sunLight;
    [SerializeField] Material dayShaderGraph;
    [SerializeField] Material nightShaderGrapgh;

    [Space(10)]
    [Header("Список ламп и табличек")]
    [SerializeField] InventoryReplaceItem inventoryReplaceItem;

    [Space(10)]
    [Header("Проверка освещености помещения")]
    [SerializeField] GameObject firstFloor;
    [SerializeField] GameObject secondFloor;
    [SerializeField] GameObject thirdFloor;
    [SerializeField] GameObject mainRoof;
    [SerializeField] List<MeshRenderer> roofsRenderList;
    [SerializeField] Material opacityMaterial;
    [SerializeField] Material defualtRoofMaterial;
    [SerializeField] List<GameObject> stairsList;

    [Space(10)]
    [SerializeField] GameObject fpsCharacter;
    [SerializeField] GameObject thirdCharacter;
    [SerializeField] GameObject timeSkipCamera;
    [SerializeField] SelectableRoomSpawn selectableRoomSpawn;

    [Space(10)]
    [Header("Точки спавна для ручной проверки")]
    [SerializeField] List<GameObject> pointsGroup;
    [SerializeField] List<PointSelect> pointSelectList;

    [Space(10)]
    [Header("Трайсер пути")]
    [SerializeField] List<TrackPlayer> trackPlayerList;

    [Header("Люксометр")]
    [SerializeField] GameObject luxmetr;

    private Animator secondFloorAnim;
    private Animator thirdFloorAnim;

    private PointSelect pointSelect;
    private bool isTeleportReady;
    [SerializeField] private bool isAutomatic;
    [SerializeField] private bool isCheckNow;

    [SerializeField] float waitTime;
    [SerializeField] LightColorMap lightColorMap;

    [Header("Прочее")]
    [SerializeField] Button fpsButton;
    [SerializeField] Button topDownButton;
    [SerializeField] GameObject endCheckButtonObject;
    [SerializeField] List<PointSelect> pointsSelectList;
    [SerializeField] GameObject horizontalMenu;
    [SerializeField] OpenUIAndDisableCharacter openUIAndDisableCharacter;
    [SerializeField] StartCheck startCheck;

    public void CheckActivation(bool isTypeCheck)
    {
        StartCoroutine(WaitActivationCheck(isTypeCheck));
    }
     IEnumerator WaitActivationCheck(bool isAuto)
    {
        yield return new WaitForSeconds(0.1f);
        startCheck.ActivationManualCheck(isAuto);
    }
    public void NightTest()
    {
        DisableLamps(true);
        sunLight.transform.localRotation = Quaternion.Euler(-90f, sunLight.transform.localRotation.y, sunLight.transform.localRotation.z);
        RenderSettings.skybox = nightShaderGrapgh;
        
    }

    public void DisableLamps(bool isOn)
    {
        if(isOn)
        {
            foreach (MeshRenderer render in meshRenderersLampList)
            {
                render.material = lampsMaterialNoLight;
            }
        }
        else
        {
            foreach (MeshRenderer render in meshRenderersLampList)
            {
                render.material = lampsMaterial;
            }
        }
        
    }
    public void AutomaticEnabled(bool isOn)
    {
        if (isOn)
        {
            foreach (TrackPlayer track in trackPlayerList)
            {
                track.VisibleHiddenTrack(false);
            }
        }
        else
        {
            lightColorMap.ColorMapActive(false);
            thirdCharacter.GetComponent<ThirdPlayerControl>().EnableLeftMouseClick(true);
        }
        isCheckNow = true;
        isAutomatic = isOn;
    }

    public void ManualCheckTopDownViewEnable() 
    {
        EnableLuxmetr(false);
        fpsCharacter.SetActive(false);
        fpsCharacter.GetComponent<FirstPlayerControl>().enabled = false;

        secondFloorAnim = secondFloor.GetComponent<Animator>();
        thirdFloorAnim = thirdFloor.GetComponent<Animator>();

        thirdCharacter.SetActive(true);

        mainRoof.SetActive(false);

        foreach (MeshRenderer renderFloor in roofsRenderList)
        {
            renderFloor.transform.GetComponent<MeshCollider>().enabled = false;
            renderFloor.enabled = false;
        }
        if (!isAutomatic && isCheckNow)
        {
            topDownButton.interactable = false;
        }
        else
        {
            selectableRoomSpawn.enabled=false;

            foreach(PointSelect point in pointSelectList)
            {
                point.UnselectBorder();
            }

            fpsButton.interactable = false;
            fpsButton.gameObject.SetActive(false);
            topDownButton.interactable = false;
            topDownButton.gameObject.SetActive(false);
        }
        secondFloorAnim.SetBool("isOpen", true);
        thirdFloorAnim.SetBool("isOpen", true);
        StartCoroutine(WaitOpenBuild());

        foreach (GameObject stairs in stairsList)
        {
            stairs.SetActive(true);
        }
    }

    public void ManualCheckFpsViewEnable()
    {
        thirdCharacter.GetComponent<ThirdPlayerControl>().enabled = false;
        selectableRoomSpawn.enabled = false;

        foreach (PointSelect point in pointSelectList)
        {
            point.UnselectBorder();
        }

        foreach (GameObject point in pointsGroup)
        {
            point.SetActive(false);
        }

        secondFloorAnim.SetBool("isOpen", false);
        thirdFloorAnim.SetBool("isOpen", false);

        EnableHorizontalMenu(false);

        if (!isAutomatic && isCheckNow)
        {
            foreach (TrackPlayer track in trackPlayerList)
            {
                track.VisibleHiddenTrack(false);
                track.GenerateNewTrace();
            }
            fpsButton.interactable = false;
        }
        else if(isAutomatic && isCheckNow)
        {
            lightColorMap.ColorMapActive(false);
        }
        
        StartCoroutine(WaitCloseBuild());
    }

    public void ManualCheckStart(bool statusCheck)
    {
        DisableLamps(statusCheck);
        inventoryReplaceItem.LampOnOff(statusCheck);

    }
    IEnumerator WaitOpenBuild()
    {
        yield return new WaitForSeconds(waitTime);

        thirdCharacter.GetComponent<ThirdPlayerControl>().enabled = true;

        EnableHorizontalMenu(true);

        foreach(GameObject point in pointsGroup)
        {
            point.SetActive(true);
        }
        if (!isAutomatic && isCheckNow)
        {
            Debug.Log("Включение выбора комнаты");
            selectableRoomSpawn.enabled = true;
            thirdCharacter.GetComponent<ThirdPlayerControl>().EnableLeftMouseClick(true);
            topDownButton.gameObject.SetActive(false);
            fpsButton.gameObject.SetActive(true);
            fpsButton.interactable = true;

            foreach (TrackPlayer track in trackPlayerList)
            {
                track.VisibleHiddenTrack(true);
                track.StartDrawTrace();
            }
        }
        else if(isAutomatic && isCheckNow)
        {
            selectableRoomSpawn.enabled = false;
            thirdCharacter.GetComponent<ThirdPlayerControl>().EnableLeftMouseClick(false);
            lightColorMap.ColorMapActive(true);
        }
        
    }
    IEnumerator WaitCloseBuild() 
    {
        yield return new WaitForSeconds(waitTime);

        foreach (MeshRenderer renderFloor in roofsRenderList)
        {
            renderFloor.transform.GetComponent<MeshCollider>().enabled = true;
            renderFloor.enabled = true;
        }
        mainRoof.SetActive(true);

        foreach (GameObject stairs in stairsList)
        {
            stairs.SetActive(false);
        }

        if (isTeleportReady)
        {
            pointSelect.TeleportPlayerOnPoint();
            isTeleportReady= false;
        }
        
        thirdCharacter.SetActive(false);

        fpsCharacter.SetActive(true);
        fpsCharacter.GetComponent<FirstPlayerControl>().enabled = true;
        EnableLuxmetr(true);
        openUIAndDisableCharacter.OpenCloseUI();

        if (!isAutomatic && isCheckNow)
        {
            fpsButton.gameObject.SetActive(false);
            topDownButton.gameObject.SetActive(true);
            topDownButton.interactable = true;
        }
    }

    private void EnableHorizontalMenu(bool on)
    {
        horizontalMenu.SetActive(on);
    }

    public void TeleportToPoint(PointSelect point)
    {
        pointSelect = point;
        isTeleportReady = true;
        ManualCheckFpsViewEnable();
    }
    public void EnableLuxmetr(bool active)
    {
        luxmetr.SetActive(active);
        if (active)
        {
            luxmetr.GetComponent<LuxmetrCalculate>().CreatingShareList();
        }
        else
        {
            luxmetr.GetComponent<LuxmetrCalculate>().StopCorutineMetering();
        }
    }

    public void CheckNowEnd()
    {
        isCheckNow = false;
    }

    public bool ReturnIsCheckProgress()
    {
        return isCheckNow;
    }

    public void EndCheckTypeView(bool isTopDown)
    {

        endCheckButtonObject.SetActive(false);
        CheckNowEnd();
        fpsButton.gameObject.SetActive(false);
        topDownButton.gameObject.SetActive(false);
        ManualCheckStart(false);
        lightColorMap.ColorMapActive(false);

        EnableHorizontalMenu(false);

        if (isTopDown || isAutomatic)
        {
            foreach (GameObject point in pointsGroup)
            {
                point.SetActive(false);
            }

            secondFloorAnim.SetBool("isOpen", false);
            thirdFloorAnim.SetBool("isOpen", false);

            StartCoroutine(EndCheckWaitBuildClose());
            isAutomatic= false;
        }
        else
        {
            
            EnableLuxmetr(false);

            fpsCharacter.GetComponent<FirstPlayerControl>().enabled = false;
            fpsCharacter.SetActive(false);

            timeSkipCamera.SetActive(true);
            dayCicleManager.DayTimeNeeds(true);

        }
    }

    IEnumerator EndCheckWaitBuildClose()
    {
        yield return new WaitForSeconds(waitTime);
       
        foreach (MeshRenderer renderFloor in roofsRenderList)
        {
            renderFloor.transform.GetComponent<MeshCollider>().enabled = true;
            renderFloor.enabled = true;
        }
        mainRoof.SetActive(true);

        foreach (GameObject stairs in stairsList)
        {
            stairs.SetActive(false);
        }

        thirdCharacter.SetActive(false);
        ManualCheckStart(false);

        timeSkipCamera.SetActive(true);
        dayCicleManager.DayTimeNeeds(true);
    }

    public void FPSViewEditorReturn()
    {
        selectableRoomSpawn.enabled = false;
        timeSkipCamera.SetActive(false);
        fpsCharacter.SetActive(true);
        openUIAndDisableCharacter.OpenCloseUI();
        fpsCharacter.GetComponent<FirstPlayerControl>().enabled = true;
    }

    public bool AutomaticCheck()
    {
        return isAutomatic;
    }
}
