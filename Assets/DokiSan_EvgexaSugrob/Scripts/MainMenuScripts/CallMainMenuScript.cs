using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CallMainMenuScript : MonoBehaviour
{
    [SerializeField] MainMenuAnimationBackground mainMenuAnimationBackground;

    public void ReturnDefaultView()
    {
        mainMenuAnimationBackground.NightOnOff(false);
    }

    public void FadeOff()
    {
        mainMenuAnimationBackground.FadeOff();
    }

}
