using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    [SerializeField]
    private GameObject[] containers;
    [SerializeField]
    private Item[] items;
    [SerializeField]
    private GameObject ItemInSlot;

    private InventorySlot inventorySlot;

    public void CheckList(int itemIndex)
    {
        for (int i = 0; i < containers.Length; i++)
        {
            inventorySlot = containers[i].GetComponent<InventorySlot>();
            if(inventorySlot.item == null)
            {
                GameObject newItem = Instantiate(ItemInSlot, containers[i].transform.position, Quaternion.identity);
                containers[i].GetComponent<InventorySlot>().AddItem(newItem, items[itemIndex]);
                itemslot inventoryItem = newItem.GetComponent<itemslot>();
                inventoryItem.InitializeItem(items[itemIndex]);
                return;
            }
        }
    }
    

}
