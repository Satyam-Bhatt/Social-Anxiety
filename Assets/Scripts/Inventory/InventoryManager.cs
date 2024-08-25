using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    [SerializeField]
    private GameObject[] containers;
    [SerializeField]
    private Item[] items;
    [SerializeField]
    private GameObject ItemInSlot;

    [SerializeField]
    private Slider happySlider;

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

    private void OnEnable()
    {
        for (int i = 0; i < containers.Length; i++)
        {
            inventorySlot = containers[i].GetComponent<InventorySlot>();
            inventorySlot.onUse += ItemUsed;
        }
    }
    private void OnDisable()
    {
        for (int i = 0; i < containers.Length; i++)
        {
            inventorySlot = containers[i].GetComponent<InventorySlot>();
            inventorySlot.onUse -= ItemUsed;
        }
    }

    public void ItemUsed(Item item)
    {
        Debug.Log("check" + item.type);
        if (item.type == Item.ItemType.Consumable)
        {
            happySlider.value += 1;
        }
    }
    

}
