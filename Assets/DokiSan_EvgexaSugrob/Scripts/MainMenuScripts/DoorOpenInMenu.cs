using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;

public class DoorOpenInMenu : MonoBehaviour
{
    [Header("Если дверь двойная")]
    [SerializeField] bool isDoubleDoor;

    [Header("Параметры открытия/закрытия")]
    Quaternion startRotationDoor;
    Quaternion endRotationDoor = Quaternion.Euler(0f,160f,0f);
    Quaternion invertEndRotation = Quaternion.Euler(0f, -160f, 0f);
    [SerializeField] bool isOpen;
    [SerializeField] bool isStop;
    private bool isOpenNow;
    [SerializeField] float speed = 5f;

    private void Start()
    {
        startRotationDoor = transform.localRotation;
    }

    private void Update()
    {
        if(isOpen && isStop)
        {
            if (isDoubleDoor)
            {
                transform.localRotation = Quaternion.Lerp(transform.localRotation, invertEndRotation, speed * Time.deltaTime);
            }
            else
            {
                transform.localRotation = Quaternion.Lerp(transform.localRotation, endRotationDoor, speed * Time.deltaTime);
            }

            if (transform.localRotation == endRotationDoor || transform.localRotation == invertEndRotation)
            {

                isOpen = false;
                isStop = false;
                isOpenNow = true;
            }
        }
        else if (!isOpen && isStop) 
        {
            if (isDoubleDoor)
            {
                transform.localRotation = Quaternion.Lerp(transform.localRotation, startRotationDoor, speed * Time.deltaTime);
            }
            else
            {
                transform.localRotation = Quaternion.Lerp(transform.localRotation, startRotationDoor, speed * Time.deltaTime);
            }

            if (transform.localRotation == startRotationDoor)
            {
                isOpen = false;
                isStop = false;
                isOpenNow = false;
            }
        }
    }


    public void OpenCloseDoor()
    {
        if (isOpenNow)
        {
            isOpen = false;
            isStop = true;
        }
        else
        {
            isOpen = true;
            isStop = true;
        }
    }

}
