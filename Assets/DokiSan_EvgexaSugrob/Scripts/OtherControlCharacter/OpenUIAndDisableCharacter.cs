using DoKiSan.Controls;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenUIAndDisableCharacter : MonoBehaviour
{
    [Header("Компоненты FPS")]
    [SerializeField] CharacterController characterController;
    [SerializeField] FirstPlayerControl firstPlayerControl;

    [Header("Компоненты UI")]
    [SerializeField] GameObject allMainComponentUI;

    
    [SerializeField] bool uiIsOpen;

    public void OpenCloseUI()
    {
        if (uiIsOpen)
        {
            allMainComponentUI.SetActive(false);

            characterController.enabled = true;
            firstPlayerControl.CursoreLock();

            uiIsOpen = false;
        }
        else
        {
            characterController.enabled = false;

            allMainComponentUI.SetActive(true);

            uiIsOpen= true;
        }
    }

    public bool ReturnUIIsOpen()
    {
        return uiIsOpen;
    }
}
