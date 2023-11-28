using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemsForReplace : MonoBehaviour
{
    [HideInInspector]public enum TypeDiveces
    {
        None,
        MainLamp,
        AvtonomLamp

    }
    [Header("Тип объекта")]
    [SerializeField] TypeDiveces typeDiveces;

    [Header("Настройка поведения объекта")]
    [SerializeField] InventoryReplaceItem inventoryReplace;
    [SerializeField] Transform raycastObj;
    [SerializeField] Material canReplace;
    [SerializeField] Material cantReplace;
    [SerializeField] float rayDistance = 0.1f;
    [SerializeField] LayerMask defaultLayer;
    [TagSelector] 
    public string tagFilter = "";
    [SerializeField] bool isMultiTag;
    [TagSelector]
    public string[] tagFilterArray = new string[] { };

    private bool can;
    [Header("Листы с колайдерами и материалами\nобъекта")]
    [SerializeField,HideInInspector] Collider[] colliders;
    [SerializeField,HideInInspector] MeshFilter[] meshes;
    [SerializeField, HideInInspector] List<Material> materialsList;

    [SerializeField] PointerEventData.InputButton _button;
    private bool isPlace = false;
    private Vector3? _currentPosition = null;
    private Quaternion? _currentRotation = null;
    private bool isCollision = false;
    private string currentTag;
    [SerializeField] bool isReplace;
    [SerializeField] Transform parent;

    private void OnEnable()
    {
        gameObject.layer = 2;
        colliders = GetComponentsInChildren<Collider>();
        meshes = GetComponentsInChildren<MeshFilter>();
        RigidbodyComponentWork(true);
        for (int i = 0; i < meshes.Length; i++)
        {
            materialsList.Add(meshes[i].GetComponent<Renderer>().material);
        }
        for (int i = 0; i < colliders.Length; i++)
        {
            colliders[i].enabled = false;
        }
        GetComponent<BoxCollider>().enabled = true;
    }
    private void OnDisable()
    {
        _currentPosition = transform.position;
        _currentRotation = transform.rotation;
    }
    private void Start()
    {
        inventoryReplace = FindObjectOfType<InventoryReplaceItem>();
    }
    private void Update()
    {
        if(isMultiTag)
        {
            MultiTagCheck();
        }
        else
        {
            SingleTagCheck();
        }

        for (int i = 0; i < meshes.Length; i++)
        {
            meshes[i].GetComponent<Renderer>().material = can ? canReplace : cantReplace;
        }
    }

    private void SingleTagCheck()
    {
        RaycastHit hit;

        if (Physics.Raycast(raycastObj.position, raycastObj.forward, out hit, rayDistance) && !isCollision)
        {
            if (hit.transform.tag != tagFilter)
            {
                can = false;
            }
            else
            {
                can = true;
                parent = hit.transform;
            }
        }
        else
        {
            can = false;
        }
    }

    private void MultiTagCheck()
    {
        RaycastHit hit;

        if(Physics.Raycast(raycastObj.position,raycastObj.forward, out hit, rayDistance)&& !isCollision)
        {
            if (hit.transform.tag != tagFilterArray[0]/* && hit.transform.tag != tagFilterArray[1]*/)
            {
                can = false;
            }
            else
            {
                can = true;
                parent = hit.transform;
            }
        }
        else
        {
            can= false;
        }
    }

    private void ReturnToNormal()
    {
        for (int i = 0; i < meshes.Length; i++)
        {
            meshes[i].GetComponent<Renderer>().material = materialsList[i];
        }
        for (int i = 0; i < colliders.Length; i++)
        {
            colliders[i].enabled = true;
        }
        gameObject.layer = defaultLayer;
        GetComponent<BoxCollider>().enabled = false;
        RigidbodyComponentWork(false);
        transform.SetParent(parent);
        isReplace = true;
        enabled = false;
    }

    public bool Place(Vector3 pos, Vector3 local,string name)
    {
        transform.position = pos;
        transform.localEulerAngles = local;
        transform.name= name;
        if (can)
        {
            ReturnToNormal();
        }
        else
        {
            return can;
        }
        return can;
    }

    public void EnableReplace(InventoryReplaceItem inventoryReplaceItem)
    {
        if (isPlace)
        {
            isPlace = false;
            inventoryReplaceItem.ActiveSystem(true);
            inventoryReplaceItem.SetReplacedObjects(gameObject, gameObject);
        }
    }
    public void ReturnInCurrentTransform()
    {
        transform.position = (Vector3)_currentPosition;
        transform.rotation = (Quaternion)_currentRotation;
        isPlace = true;
        ReturnToNormal();
    }
    public bool HaventCurrentTransform()
    {
        return (_currentPosition == null || _currentRotation == null);
    }
    public void ControlIsPlace(bool state)
    {
        isPlace = state;
    }
    private void OnTriggerEnter(Collider other)
    {
        isCollision = true;
    }
    private void OnTriggerExit(Collider other)
    {
        isCollision = false;
    }
    private void RigidbodyComponentWork(bool isCreate)
    { 
        if (isCreate)
        {
            Rigidbody rigidbody = gameObject.AddComponent<Rigidbody>();
            rigidbody.isKinematic = true;
            rigidbody.useGravity = false;
        }
        else
            Destroy(GetComponent<Rigidbody>());
    }

    public TypeDiveces ReturnDivecesType()
    {
        return typeDiveces;
    }
}
