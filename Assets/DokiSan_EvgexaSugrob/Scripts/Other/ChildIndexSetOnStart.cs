using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ChildIndexSetOnStart : MonoBehaviour
{
    [SerializeField] int needIndex;

    private void OnEnable()
    {
        transform.SetSiblingIndex(needIndex);
        Debug.Log("ÂÊË");
    }
    private void Start()
    {
        transform.SetSiblingIndex(needIndex);
    }


}
