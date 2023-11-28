using DoKiSan.Controls;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Vmaya.UI.Components;

public class DialogsMenuReplace : MonoBehaviour
{
    [Header("Получаемый объект")]
    [SerializeField] GameObject hitGameObject;

    [Header("Взаимодействие")]
    [SerializeField] Inventory_MouseControlInput_Example mouseControlInput;
    [SerializeField] InventoryReplaceItem inventoryReplaceItem;
    [SerializeField] CharacterController characterController;
    [SerializeField] FirstPlayerControl firstPlayerControl;
    [SerializeField] OpenUIAndDisableCharacter openUIAndDisableCharacter;
    [SerializeField] SelectableObjectInteract selectableObjectInteract;

    [Header("Отображение/скрытие диалоговых окон")]
    [SerializeField] GameObject dialogsWindowForCustomObject;
    [SerializeField] Window dialogsWindowForCustom;
    [SerializeField] GameObject dialogsWindowForNonCustomObject;
    [SerializeField] Window dialogsWindowForNonCustom;
    [SerializeField] SettingCustomDevices settingCustomDevices;

    [SerializeField] bool isCustomObject;

    private bool dialogWindowIsOpen;

    public void DialogsMenuOpen(GameObject gameObject)
    {
        hitGameObject = gameObject;
        ItemsForReplace item = hitGameObject.GetComponent<ItemParent>().GetParent().GetComponent<ItemsForReplace>();

        if(item.ReturnDivecesType() == ItemsForReplace.TypeDiveces.None)
        {
            isCustomObject= false;
        }
        else
        {
            isCustomObject= true;
        }

        if (inventoryReplaceItem.IsSystemActive() == false)
        {
            OpenCloseDialogsWindow(true);
        }
    }

    public void InteractionWithDevices()
    {
        InteractableNextStep();
        settingCustomDevices.OffLastLamp();
        settingCustomDevices.GetDevices(hitGameObject);
    }

    public void ConfirmReplace()
    {
        OpenCloseDialogsWindow(false);
        mouseControlInput.ConfirmRepaerReplace(hitGameObject);
    }

    public void DeleteObject()
    {
        OpenCloseDialogsWindow(false);
        inventoryReplaceItem.DestroyObject(hitGameObject);
    }

    public void InteractableNextStep()
    {
        dialogsWindowForCustom.hide();
    }

    public void OpenCloseDialogsWindow(bool isOn)
    {
        
        if (isCustomObject) 
        {
            if(isOn)
            {
                dialogsWindowForCustomObject.SetActive(isOn);
            }
            else
            {
                dialogsWindowForCustom.hide();
            } 
        }
        else
        {
            if(isOn) 
            {
                dialogsWindowForNonCustomObject.SetActive(isOn);
            }
            else
            {
                dialogsWindowForNonCustom.hide();
            }
        }

        mouseControlInput.enabled = !isOn;
        dialogWindowIsOpen = isOn;
        characterController.enabled = !isOn;
        inventoryReplaceItem.enabled = !isOn;
        selectableObjectInteract.SetMinDistance(isOn); // Возможно переделать на отключение скрипта, и выключение выделения
    }

    public bool IsDialogOpen() 
    {
        return dialogWindowIsOpen;
    }
}
