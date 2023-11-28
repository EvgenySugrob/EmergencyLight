using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using Vmaya.UI.Components;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] GameObject aboutProgrammWindow;
    [SerializeField] GameObject menuWarning;
    [SerializeField] TMP_Text textMessage;
    bool aboutWindowOpen;
    string typeWarning;

    public void MainMenuOpen()
    {
        SceneManager.LoadScene(0);
    }
    public void WarningWindowOpen(string action)
    {
        if (!menuWarning.activeSelf)
        {
            typeWarning= action;
            switch (action)
            {
                case "OpenMainMenu":
                    textMessage.text = "Перейти в главное меню?\nТекущий прогресс будет утерян.";
                    break;
                case "CloseApplication":
                    textMessage.text = "Выйти из программы?\nТекущий прогресс будет утерян.";
                    break;
            }
            menuWarning.SetActive(true);
        }
        
    }
    public void ResultWarningDialogs()
    {
        Debug.Log(typeWarning);
        if (typeWarning == "OpenMainMenu")
        {
            MainMenuOpen();
        }
        else 
        {
            ExitOnApplication();
        }
    }
    public void MainSceneOpen()
    {
        SceneManager.LoadScene(1);
    }


    public void ExitOnApplication()
    {
        Application.Quit();
    }
}
