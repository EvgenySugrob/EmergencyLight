using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SelectableObjectInteract : MonoBehaviour
{
    [Header("Основные компоненты и настройка")]
    [SerializeField] Camera cameraFps;
    [SerializeField] float distance;
    [SerializeField] LayerMask usebleLayerMask;

    [Header("Выделенный объект")]
    [SerializeField] GameObject selectableObject;
    [SerializeField] GameObject previousSelectableObj;

    [Header("Проверка на открытый интерфейс")]
    [SerializeField] OpenUIAndDisableCharacter openUI;

    private float defaultDistance = 4f;
    private float minDistance = 0f;

    private void Update()
    {
        RaycastMainFunc();
    }

    private void RaycastMainFunc()
    {
        Ray ray = cameraFps.ScreenPointToRay(Mouse.current.position.ReadValue());

        if (Physics.Raycast(ray, out RaycastHit hit, distance, usebleLayerMask))
        {
            selectableObject = hit.transform.gameObject;
            if(previousSelectableObj == null)
            {
                previousSelectableObj= selectableObject;
            }
            if(selectableObject.TryGetComponent<OutlineActivation>(out OutlineActivation outline))
            {
                outline.ActivationOutline();
            }
            else if(previousSelectableObj.TryGetComponent<OutlineActivation>(out OutlineActivation prevOutline))
            {
                prevOutline.DisableOutline();
            }


            if(selectableObject != previousSelectableObj)
            {
                if(previousSelectableObj.TryGetComponent<OutlineActivation>(out OutlineActivation prevOutline))
                {
                    prevOutline.DisableOutline();
                }
                previousSelectableObj = selectableObject;
            }
        }
    }

    public void UserInterectWithObject()
    {
        if (selectableObject.GetComponent<DoorOpen>())
        {
            DoorOpen thisDoor = selectableObject.GetComponent<DoorOpen>();
            thisDoor.OpenCloseDoor();
        }
        else
        {
            Debug.Log("else");
        }
    }

    public void SetMinDistance(bool isMinDistanceSet)
    {
        if (isMinDistanceSet)
        {
            distance = minDistance;
        }
        else
        {
            distance = defaultDistance;
        }
    }
}
