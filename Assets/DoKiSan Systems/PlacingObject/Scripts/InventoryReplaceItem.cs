using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.Controls.AxisControl;
using static UnityEngine.Rendering.DebugUI;

public class InventoryReplaceItem : MonoBehaviour
{
    [Header("Объект установки и настройки руки")]
    [SerializeField] GameObject replacedObjects;
    [SerializeField] float distance;
    [SerializeField] float projectionSpeed;
    [SerializeField] float rotationSpeed;
    [SerializeField] LayerMask layerMask;
    [SerializeField] Camera cameraPlayer;

    private Vector3 objectPosition;
    private Vector3 rotation = new Vector3(0f, 0f, 0f);
    [SerializeField] GameObject createdObj;
    [SerializeField] private bool isActiveReplace;


    private Vector3 currentSurface = new Vector3(0f,0f,0f);
    private GameObject hitObject;

    [Header("Рачсчет лимитов")]
    [SerializeField] List<GameObject> lampList;
    [SerializeField] List<GameObject> avtonomLampList;
    [SerializeField] LimmitDiveces lampLimmit;
    [SerializeField] LimmitDiveces avtonomLampLimmit;

    [Header("Таблички")]
    [SerializeField]List<GameObject> tableList;

    [SerializeField] LuxmetrCalculate luxmetrCalculate;
    

    //==================================
    //Public methods
    //==================================
    public List<GameObject> GetLampList()
    {
        return lampList;
    }
    public List<GameObject> GetAvtonomLampList()
    {
        return avtonomLampList;
    }
    public List<GameObject> GetTableList()
    {
        return tableList;
    }
    public void ActiveSystem(bool isActive)
    {
        isActiveReplace = isActive;
    }
    public bool IsSystemActive()
    {
        return isActiveReplace;
    }
    public void SetReplacedObjects(GameObject objects, GameObject createdObject = null)
    {
        if (createdObj)
            Destroy(createdObj);
        replacedObjects = objects;
        createdObj = createdObject;
    }
    public GameObject GetHitObject()
    {
        if (hitObject)
        {
            if (hitObject.GetComponent<ItemParent>())
            {
                GameObject tmpHitObject = hitObject;
                hitObject = null;
                return tmpHitObject;
            }
        }         
        return null;
    }
    public void ClearPrefab()
    {
        if (isActiveReplace)
        {
            if (createdObj.GetComponent<ItemsForReplace>().HaventCurrentTransform())
            { 
                Destroy(createdObj); 
            }
            else
            {
                createdObj.GetComponent<ItemsForReplace>().ReturnInCurrentTransform();
            }
            replacedObjects = null;
            isActiveReplace = false;
        }
    }

    public void PlacementPrefab()
    {
        if (isActiveReplace)
        {
            if (createdObj.GetComponent<ItemsForReplace>().Place(objectPosition, createdObj.transform.rotation.eulerAngles, replacedObjects.name))
            {
                createdObj.GetComponent<ItemsForReplace>().ControlIsPlace(true);
                GetEnumValue();
                createdObj = null;
                currentSurface = Vector3.zero;
                isActiveReplace = false;
                return;
            }
        }

    }

    public void GetEnumValue()
    {
        ItemsForReplace itemReplace = createdObj.GetComponent<ItemsForReplace>();

        switch (itemReplace.ReturnDivecesType())
        {
            case ItemsForReplace.TypeDiveces.None:
                if (tableList.Count == 0)
                {
                    tableList.Add(createdObj);
                }
                else
                {
                    if (tableList.Contains(createdObj))
                        Debug.Log("lol");
                    else
                    {
                        tableList.Add(createdObj);
                    }
                }
                break;

            case ItemsForReplace.TypeDiveces.MainLamp:
                if (lampList.Count == 0)
                {
                    lampList.Add(createdObj);
                    lampLimmit.CalculationCount(lampList.Count);
                    luxmetrCalculate.AddItemForList(createdObj);
                }
                else 
                {
                    if (lampList.Contains(createdObj))
                        Debug.Log("есть такой");
                    else
                    {
                        lampList.Add(createdObj);
                        lampLimmit.CalculationCount(lampList.Count);
                        luxmetrCalculate.AddItemForList(createdObj);
                    }
                        
                }
                break;

            case ItemsForReplace.TypeDiveces.AvtonomLamp:
                if (avtonomLampList.Count == 0)
                {
                    avtonomLampList.Add(createdObj);
                    avtonomLampLimmit.CalculationCount(avtonomLampList.Count);
                    luxmetrCalculate.AddItemForList(createdObj);
                }
                else
                {
                    if (avtonomLampList.Contains(createdObj))
                        Debug.Log("есть такой");
                    else
                    {
                        avtonomLampList.Add(createdObj);
                        avtonomLampLimmit.CalculationCount(avtonomLampList.Count);
                        luxmetrCalculate.AddItemForList(createdObj);
                    }
                        
                }
                break;
        }
    }

    public void RotationObject(float stepScroll)
    {
        if (isActiveReplace)
        {
            rotation = new Vector3(0f, 0f, stepScroll * rotationSpeed);
        }
    }

    public void DestroyObject(GameObject hitObject)
    {
        GameObject destoyedObject = hitObject.GetComponent<ItemParent>().GetParent();
        Debug.Log("DestroyObject");
        ItemsForReplace itemReplace = destoyedObject.GetComponent<ItemsForReplace>();

        switch (itemReplace.ReturnDivecesType())
        {
            case ItemsForReplace.TypeDiveces.None:
                if (tableList.Contains(destoyedObject))
                {
                    tableList.Remove(destoyedObject);
                    Destroy(destoyedObject);
                }
                break;

            case ItemsForReplace.TypeDiveces.MainLamp:
                if (lampList.Contains(destoyedObject))
                {
                    lampList.Remove(destoyedObject);
                    luxmetrCalculate.RemoveItemForList(destoyedObject);
                    lampLimmit.CalculationCount(lampList.Count);
                    Destroy(destoyedObject);
                }
                break;

            case ItemsForReplace.TypeDiveces.AvtonomLamp:
                if (avtonomLampList.Contains(destoyedObject))
                {
                    avtonomLampList.Remove(destoyedObject);
                    luxmetrCalculate.RemoveItemForList(destoyedObject);
                    avtonomLampLimmit.CalculationCount(avtonomLampList.Count);
                    Destroy(destoyedObject);
                }
                break;

            default:
                break;
        }
    }
    //Включение ламп и свечение табличек
    public void LampOnOff(bool isOn)
    {
        foreach (GameObject lamp in lampList)
        {
            lamp.GetComponent<MainSettingCustomDevices>().LampOn(isOn);
        }

        foreach (GameObject avtLamp in avtonomLampList)
        {
            avtLamp.GetComponent<MainSettingCustomDevices>().LampOn(isOn);
        }

        foreach (GameObject table in tableList)
        {
            table.GetComponent<TableNewMaterialSet>().EmmisionTableActivation(isOn);
        }
    }

    public int ReturnInstalledLamp()
    {
        return lampList.Count;
    }
    public float TotalLoadOfInstalledLamps()
    {
        float totalLoad = 0;

        foreach (GameObject lamp in lampList)
        {
            totalLoad+= lamp.GetComponent<MainSettingCustomDevices>().GetAmpere();
        }
        return totalLoad;
    }
    //==================================
    //Private methods
    //==================================

    private void Update()
    {
        Ray ray = cameraPlayer.ScreenPointToRay(Mouse.current.position.ReadValue());
        if (isActiveReplace) 
        {

            if (Physics.Raycast(ray,out RaycastHit hit, distance,layerMask))
            {
                CreateObject(objectPosition);
                AllignToSurface(hit.normal);
                objectPosition = new Vector3(hit.point.x, hit.point.y, hit.point.z);
            }
            else
            {               
                objectPosition = ray.GetPoint(distance);
                CreateObject(objectPosition);
            }
        }
        else
        {
            if (Physics.Raycast(ray, out RaycastHit hit, distance, layerMask))
            {
                hitObject = hit.transform.gameObject;
            }
        }
    }

    private void CreateObject(Vector3 position)
    {
        if (replacedObjects != null)
        {
            if (createdObj == null)
            {
                GameObject tempObject = Instantiate(replacedObjects, position, replacedObjects.transform.rotation);
                tempObject.name = replacedObjects.name;
                createdObj = tempObject;
            }
            else
            {
                createdObj.transform.rotation *= Quaternion.Euler(rotation);
                rotation = Vector3.zero;
                createdObj.transform.position = Vector3.Lerp(createdObj.transform.position, position, projectionSpeed * Time.fixedDeltaTime);
            }
        }
    }

    
    private void AllignToSurface(Vector3 surface)
    {
        if (currentSurface != surface)
        {
            createdObj.transform.rotation = Quaternion.LookRotation(surface.normalized);
            currentSurface = surface;
        }
    }
}