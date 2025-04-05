using System;
using TMPro;
using UnityEngine;

public class ItemTrigger : MonoBehaviour
{
    [SerializeField] Sprite itemIcon;
    [SerializeField] string title;
    [SerializeField] string desc;
    [SerializeField] InventoryController invController;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.CompareTag("Player"))
        {
            InventoryPage invPage = collision.GetComponent<InventoryController>().invUI;
            if (invPage != null)
            {
                AddItemToInv(invPage, itemIcon, title, desc);
            }
            else
            {
                Debug.Log("nullos");

            }
        }
        Destroy(gameObject);
    }

    private void AddItemToInv(InventoryPage inventoryPage, Sprite icon, string itemTitle, string itemDesc)
    {
        Debug.Log("Attempting to add item...");
        foreach (Item item in inventoryPage.items)
        {
            if (!item.itemIcon.gameObject.activeSelf)
            {
                item.SetItem(icon);
            }
        }
    }
}
