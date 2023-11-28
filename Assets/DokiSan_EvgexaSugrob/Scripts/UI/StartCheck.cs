using DoKiSan.Controls;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using Vmaya.UI.Components;

public class StartCheck : MonoBehaviour
{
    [Header("Контролеры персонажа")]
    [SerializeField] GameObject fpsControler;
    [SerializeField] GameObject thirdControler;
    [SerializeField] GameObject timeSkipCamera;
    [SerializeField] SelectableRoomSpawn selectableRoomSpawn;
    [SerializeField] DayCicleManager dayCicleManager;
    [SerializeField] OpenUIAndDisableCharacter openUIAndDisable;

    [Header("Прочее")]
    [SerializeField] ScenarioResultCheck scenarioResultCheck;
    [SerializeField] GameObject choiseTypeManualCheck;
    [SerializeField] GameObject warningWindow;
    [SerializeField] GameObject warningWindowAlready;
    [SerializeField] GameObject roof;
    [SerializeField] TMP_Text nameFloor;
    [SerializeField] Button nextBt;
    [SerializeField] Button prevBt;
    [SerializeField] GameObject swapCheckWindow;
    [SerializeField] GameObject fpsButton;
    [SerializeField] OpenMontageInventory openMontageInventory;
    [SerializeField] DialogsMenuReplace dialogsMenuReplace;
    [SerializeField] SettingCustomDevices settingCustomDevices;
    [SerializeField] GameObject horizontalMenu;
    [SerializeField] GameObject mainMenuParent;
    [SerializeField] InventoryReplaceItem inventoryReplaceItem;
    [SerializeField] GameObject endCheckWindow;
    [SerializeField] GameObject endCheckButton;
    [SerializeField] List<GameObject> devicesList;

    private bool typeCheckIsOpen;
    private bool checkBuffer;
    private bool isTopDown;
    private string itemMenuDisableName = "MenuItem-2";
    private string itemMenuCheckDisableName = "MenuItem-3";

    private bool windowEndOpen;

    public void OpenWindowTypeCheck()
    {
        if (typeCheckIsOpen)
        {
            warningWindow.SetActive(true);
        }
        else
        {
            choiseTypeManualCheck.SetActive(true);
            typeCheckIsOpen = true;
        }
    }
    public void CloseWindow()
    {
        typeCheckIsOpen= false;
        choiseTypeManualCheck.GetComponent<Window>().hide();
    }

    public void ActivationManualCheck(bool isAuto) 
    {
        if (!GetStatusWarningEndCheckWindow())
        {
            if (openMontageInventory.ReturnStateInventory())
            {
                openMontageInventory.OpenCloseInventory();
            }
            if (dialogsMenuReplace.IsDialogOpen())
            {
                dialogsMenuReplace.OpenCloseDialogsWindow(false);
            }
            settingCustomDevices.OpenSettingWindow(false);

            foreach (Transform transform in mainMenuParent.transform)
            {
                if (transform.name == itemMenuDisableName || transform.name == itemMenuCheckDisableName)
                {
                    transform.gameObject.SetActive(false);
                    
                }
            }

            if (scenarioResultCheck.ReturnIsCheckProgress())
            {
                selectableRoomSpawn.enabled = false;
                checkBuffer = isAuto;
                swapCheckWindow.SetActive(true);

            }
            else
            {
                checkBuffer = isAuto;
                CheckHasBegun();
            }
        }
    }


    public void CheckHasBegun()
    {
        if (swapCheckWindow.activeSelf)
            HideWindowSwap();

        horizontalMenu.SetActive(false);
        fpsControler.SetActive(false);
        timeSkipCamera.SetActive(true);
        endCheckButton.SetActive(true);
        fpsControler.GetComponent<FirstPlayerControl>().enabled = false;
        //OutlineCheckOff();
        //fpsButton.SetActive(true);

        TypeCheck(checkBuffer);
        OnlyTimeSkip();
    }
    public void HideWindowSwap()
    {
        selectableRoomSpawn.enabled = true;
        swapCheckWindow.GetComponent<Window>().hide();
    }

    private void TypeCheck(bool isAuto)
    {
        dayCicleManager.SetTypeCheck(isAuto);
    }
    public void OnlyTimeSkip()
    {
        dayCicleManager.SetReservSkybox();
        //openUIAndDisable.OpenCloseUI();
        dayCicleManager.TimeInRightPosition();
    }

    public void TopDownViewEnabledInCheck()
    {
        timeSkipCamera.SetActive(false);
    }

    //private void OutlineCheckOff()
    //{
        
    //    foreach (GameObject item in inventoryReplaceItem.GetLampList())
    //    {
    //        devicesList.Add(item);
    //    }
    //    devicesList.AddRange(inventoryReplaceItem.GetAvtonomLampList());
    //    devicesList.AddRange(inventoryReplaceItem.GetTableList());

    //    foreach (GameObject item in devicesList)
    //    {
    //        if(!devicesList.Contains(item))
    //        {
    //            item.GetComponent<AllOutlineCheckDevicesOff>().OutlineDeviceDisable();
    //        }
            
    //    }
    //}

    public void EndCheckWindowOpen()
    {
        if (!windowEndOpen)
        {
            if (selectableRoomSpawn.enabled)
            {
                selectableRoomSpawn.enabled = false;
                isTopDown = true;
            }
            else
            {
                fpsControler.GetComponent<FirstPlayerControl>().enabled = false;
                isTopDown= false;
            }
            
            endCheckWindow.SetActive(true);
            windowEndOpen = true;
        }
    }

    public void EndCheck()
    {
        CloseEndCheckWindow();
        Debug.Log("isTopDown - " + isTopDown);
        scenarioResultCheck.EndCheckTypeView(isTopDown);
        foreach (Transform transform in mainMenuParent.transform)
        {
            if (transform.name == itemMenuDisableName || transform.name == itemMenuCheckDisableName)
            {
                transform.gameObject.SetActive(true);
            }
        }
    }
    public void CloseEndCheckWindow()
    {
        endCheckWindow.GetComponent<Dialog>().hide();
        windowEndOpen = false;
        if(!scenarioResultCheck.AutomaticCheck())
        {
            thirdControler.GetComponent<ThirdPlayerControl>().EnableLeftMouseClick(true);
        }
        
    }

    public bool GetStatusWarningEndCheckWindow()
    {
        return windowEndOpen;
    }
}
