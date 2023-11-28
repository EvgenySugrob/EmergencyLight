using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.EventSystems;

public class PointSelect : MonoBehaviour
{
    [SerializeField] Transform playerPoint;
    [SerializeField] GameObject border;
    [SerializeField] Transform fpsControler;

    [SerializeField] Material defualtBorder;
    [SerializeField] Material selectBorder;

    private Vector3 startPositionPoint;
    private Quaternion startRotationPoint;

    private void Start()
    {
        //playerPoint = transform.GetChild(0).transform;

        startPositionPoint= playerPoint.position;
        startRotationPoint= playerPoint.rotation;
    }

    public void TeleportPlayerOnPoint()
    {
        fpsControler.position = playerPoint.position;
        fpsControler.rotation = playerPoint.rotation;
    }

    public void BorderSelect(bool isActive)
    {
        border.SetActive(isActive);
    }
    public void SelectBorder()
    {
        border.GetComponent<MeshRenderer>().material = selectBorder;
    }
    public void UnselectBorder()
    {
        border.GetComponent<MeshRenderer>().material = defualtBorder;
    }
    public void AllBorderOn(bool isOn)
    {

    }
}
