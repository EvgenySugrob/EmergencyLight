using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemSpawn : MonoBehaviour
{
    [SerializeField] GameObject _item;
    [SerializeField] InventoryReplaceItem _inventoryReplaceItem;

    [SerializeField] bool isMultiMaterial;
    [SerializeField] List<Material> materialList;
    enum MaterialType { left, right, stairsLeft, stairsRight, exitTable}
    [SerializeField] MaterialType typeMaterialForTable;

    [SerializeField] GameObject dialogsCustomMenuWindow;
    [SerializeField] GameObject dialogsNonCustomMenuWindow;
    [SerializeField] GameObject settingCustomDevicesWindow;

    public void EnablePlaceObject()
    {
        if(dialogsNonCustomMenuWindow.activeSelf == false && dialogsCustomMenuWindow.activeSelf == false && settingCustomDevicesWindow.activeSelf == false)
        {
            Debug.Log("Закрытое окно");
            if (isMultiMaterial)
            {
                CheckEnumMaterialType();
            }

            _inventoryReplaceItem.ActiveSystem(true);
            _inventoryReplaceItem.SetReplacedObjects(_item);
        }
        
    }

    private void CheckEnumMaterialType()
    {
        TableNewMaterialSet tableMaterial = _item.GetComponent<TableNewMaterialSet>();

        switch (typeMaterialForTable) 
        {
            case MaterialType.left:
                tableMaterial.GetMaterialForReplace(materialList[(int)typeMaterialForTable]);
                break;
            case MaterialType.right:
                tableMaterial.GetMaterialForReplace(materialList[(int)typeMaterialForTable]);
                break;
            case MaterialType.stairsLeft:
                tableMaterial.GetMaterialForReplace(materialList[(int)typeMaterialForTable]);
                break;
            case MaterialType.stairsRight:
                tableMaterial.GetMaterialForReplace(materialList[(int)typeMaterialForTable]);
                break;
            case MaterialType.exitTable:
                tableMaterial.GetMaterialForReplace(materialList[(int)typeMaterialForTable]);
                break;

        }
    }
}
