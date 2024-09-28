using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class InventorySlot : MonoBehaviour
{
    public delegate void OnUse(Item item);
    public event OnUse onUse;

    public Item item;
    
    private GameObject item_PrefabScript;

    public void AddItem(GameObject item_Prefab, Item item_out)
    {
        item_Prefab.transform.SetParent(transform, false);
        item_Prefab.transform.localPosition = Vector3.zero;
        item_PrefabScript = item_Prefab;
        item = item_out;
    }

    public void ButtonClick()
    {
        if(item != null)
        {
            //item.Use();
            onUse?.Invoke(item);

            if (item.type == Item.ItemType.Consumable)
            { 
                Destroy(item_PrefabScript);
                item = null;            
            }
        }
    }
}
