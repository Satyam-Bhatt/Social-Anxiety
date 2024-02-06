using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
public class Item : ScriptableObject
{
    public enum ItemType
    {
        Consumable,
        Killable
    }

    public Sprite icon = null;
    public ItemType type;

    public void Use()
    {
        if(type == ItemType.Consumable)
        {
            Debug.Log("Using Food");
        }
        else
        {
            Debug.Log("Killing Knife");
        }
    }
}
