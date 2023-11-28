using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SelectableRoomSpawn : MonoBehaviour
{
    [Header("Основные компоненты и настройка")]
    [SerializeField] Camera thirdPersonCamera;
    [SerializeField] float distance;
    [SerializeField] LayerMask usebleLayerMask;

    [Header("Выделенный объект")]
    [SerializeField] GameObject selectableObject;
    [SerializeField] GameObject previousSelectableObj;

    [Header("Проверка на открытый интерфейс")]
    [SerializeField] OpenUIAndDisableCharacter openUI;

    [SerializeField] GameObject acceptTeleportWindow;
    [SerializeField] AcceptRoomStartManual acceptRoomStartManual;

    private Ray ray;

    private void Update()
    {
        SelectRoomMainFunc();
    }

    private void SelectRoomMainFunc()//под выделение выбранной комнаты
    {
        ray = thirdPersonCamera.ScreenPointToRay(Mouse.current.position.ReadValue());

        if (Physics.Raycast(ray, out RaycastHit hit, distance, usebleLayerMask))
        {
            selectableObject = hit.transform.gameObject;
            if (previousSelectableObj == null)
            {
                previousSelectableObj = selectableObject;
            }
            if(selectableObject.TryGetComponent<PointSelect>(out PointSelect point))
            {
                point.SelectBorder();
            }
            else if(previousSelectableObj.TryGetComponent<PointSelect>(out PointSelect prevPoint))
            {
                prevPoint.UnselectBorder();
            }

            if(selectableObject != previousSelectableObj)
            {
                if (previousSelectableObj.TryGetComponent<PointSelect>(out PointSelect prevPoint))
                {
                    prevPoint.UnselectBorder();
                }
                previousSelectableObj= selectableObject;
            }
        }
    }

    public void RoomTeleport()
    {
        if (Physics.Raycast(ray,out RaycastHit hit, distance, usebleLayerMask))
        {
            GameObject hitObject = hit.transform.gameObject;
            if (hitObject.GetComponent<PointSelect>())
            {
                PointSelect point = hitObject.GetComponent<PointSelect>();
                acceptRoomStartManual.GetPoint(point);
                acceptTeleportWindow.SetActive(true);
                enabled = false;
            }
        }
    }
}
