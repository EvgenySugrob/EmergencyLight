using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class NameObjectOnCursor : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private string defaultName;
    [SerializeField] TMP_Text inventoryWindowTittle;
    [SerializeField] string nameTitle;

    private void Start()
    {
        defaultName = inventoryWindowTittle.text;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        inventoryWindowTittle.text = nameTitle;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        inventoryWindowTittle.text = defaultName;
    }
}
