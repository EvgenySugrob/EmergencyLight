using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vmaya.UI.Components;

public class AcceptRoomStartManual : MonoBehaviour
{
    [SerializeField] Window window;
    [SerializeField] PointSelect pointSelect;
    [SerializeField] SelectableRoomSpawn selectableRoomSpawn;

    [SerializeField] StartCheck floorSelectScript;
    [SerializeField] ScenarioResultCheck scenarioResultCheck;


    public void GetPoint(PointSelect point)
    {
        pointSelect = point;
    }

    public void TeleportOnPoint() //телепорт и промотка времени 
    {
        //pointSelect.TeleportPlayerOnPoint();
        scenarioResultCheck.TeleportToPoint(pointSelect);


        window.hide();
    }

    public void CancelAndCloseWindow()
    {
        selectableRoomSpawn.enabled = true;
        window.hide();
    }
}
