using System;
using TMPro;
using Unity.VisualScripting;
using UnityEditor.PackageManager.Requests;
using UnityEditor.Presets;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


[System.Serializable]
public class Item : MonoBehaviour
{

    [SerializeField]
    public Image itemIcon;

    [SerializeField]
    Image border;


    public event Action<Item> OnItemClicked;

    bool empty = false;

    public void Awake()
    {
        Reset();
        Deselect();
    }

    public void Deselect()
    {
        border.enabled = false;
    }
    public void Select()
    {
        border.enabled = true;
    }

    public void Reset()
    {
        this.itemIcon.gameObject.SetActive(false);
        empty = true;
    }

    public void SetItem(Sprite icon)
    {

        this.itemIcon.gameObject.SetActive(true);
        this.itemIcon.sprite=icon;
        empty = false;

    }

    public void OnPointerClick(BaseEventData data)
    {
        PointerEventData pData = (PointerEventData)data;
        if(pData.button== PointerEventData.InputButton.Left)
        {
            OnItemClicked?.Invoke(this);
        }
    }
}
