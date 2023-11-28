using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenMontageInventory : MonoBehaviour
{
    [SerializeField] GameObject inventory;

    bool isInventoryOpen;

    public bool ReturnStateInventory()
    {
        return isInventoryOpen;
    }

    public void OpenCloseInventory()
    {
        if (isInventoryOpen) 
        {
            inventory.SetActive(false);
            isInventoryOpen = false;
        }
        else
        {
            inventory.SetActive(true);
            isInventoryOpen = true;
        }
    }
}
