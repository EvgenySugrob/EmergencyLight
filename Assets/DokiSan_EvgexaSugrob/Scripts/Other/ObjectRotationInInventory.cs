using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectRotationInInventory : MonoBehaviour
{
    [SerializeField] Transform objectRotation;
    private Vector3 speedRotation = new Vector3(0, 50f, 0);
    private Quaternion startRotation;
    private bool isRot;

    private void Start()
    {
        startRotation = objectRotation.rotation;
    }

    private void FixedUpdate()
    {
        if (isRot)
        {
            objectRotation.Rotate(speedRotation * Time.fixedDeltaTime, Space.World);
        }
        else
        {
            objectRotation.rotation = startRotation;
        }
    }

    public void RotationObject(bool isRotate)
    {
        if (isRotate)
        {
            isRot = true;
        }
        else
        {
            isRot = false;
        }
    }
}
