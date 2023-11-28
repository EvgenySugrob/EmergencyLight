using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerOpenDoor : MonoBehaviour
{
    [SerializeField] List<DoorOpenInMenu> doorOpenInMenuList;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "MainCamera")
        {
            foreach(DoorOpenInMenu door in doorOpenInMenuList)
            {
                door.OpenCloseDoor();
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "MainCamera")
        {
            if (other.tag == "MainCamera")
            {
                foreach (DoorOpenInMenu door in doorOpenInMenuList)
                {
                    door.OpenCloseDoor();
                }
            }
        }
    }
}
