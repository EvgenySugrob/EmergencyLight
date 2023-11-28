using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ObjectSelectebleOnCharacter : MonoBehaviour
{
    [Header("��������� ��������� ���������� ��������")]
    [SerializeField] Camera playerCamera;
    [SerializeField] LayerMask usebleLayerMask;
    [SerializeField] float distanceHand;
    [SerializeField] bool isActiveSelect;

    //������ ��� ��������� �������� �������������� � ����� ������������ ��������

    private void Update()
    {
        Ray ray = playerCamera.ScreenPointToRay(Mouse.current.position.ReadValue());
        if (isActiveSelect)
        {
            if(Physics.Raycast(ray, out RaycastHit hit, distanceHand, usebleLayerMask))
            {

            }
            else
            {

            }
        }
    }

}
