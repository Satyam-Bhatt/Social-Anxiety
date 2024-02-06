using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class InventorySlot : MonoBehaviour
{
    public Item item;

    public void AddItem(GameObject item_Prefab, Item item_out)
    {
        item_Prefab.transform.SetParent(transform, false);
        item_Prefab.transform.localPosition = Vector3.zero;
        item = item_out;
    }
}
