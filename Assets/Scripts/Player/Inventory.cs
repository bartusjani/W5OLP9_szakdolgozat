using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public GameObject inventoryUI;
    bool isOpen = false;
    List<string> items = new List<string>(); 
    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            if (isOpen)
            {
                Close();
            }
            else
            {
                InventoryOpen();
            }
        }
    }

    void InventoryOpen()
    {
        isOpen = !isOpen;
        Time.timeScale = 0f;
        inventoryUI.SetActive(true);
        Debug.Log("inventory");
    }
    void Close()
    {
        isOpen = false;
        Time.timeScale = 1f;
        inventoryUI.SetActive(false);

    }
    void AddItem(string text)
    {
        items.Add(text);
        Debug.Log("Addolta az itemet");
    }
}
