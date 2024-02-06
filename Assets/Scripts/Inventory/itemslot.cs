using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class itemslot : MonoBehaviour
{
    public Image image;
    public Item item_mine; 

    private void Awake()
    {
        image = GetComponent<Image>();
    }

    public void InitializeItem(Item item)
    {
        item_mine = item; 
        image.sprite = item.icon;
    }
}
