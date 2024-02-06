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

    private InventorySlot InventorySlot;

    public void CheckList(int itemIndex)
    {
        for (int i = 0; i < containers.Length; i++)
        {
            InventorySlot = containers[i].GetComponent<InventorySlot>();
            if(InventorySlot.item == null)
            {
                GameObject newItem = Instantiate(ItemInSlot, containers[i].transform.position, Quaternion.identity);
                InventorySlot.AddItem(newItem, items[itemIndex]);
                itemslot inventoryItem = newItem.GetComponent<itemslot>();
                inventoryItem.InitializeItem(items[itemIndex]);
                return;
            }
        }
    }
    

}
