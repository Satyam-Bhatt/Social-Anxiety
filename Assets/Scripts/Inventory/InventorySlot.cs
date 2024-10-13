using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public delegate void OnUse(Item item);
    public event OnUse onUse;

    public Item item;
    
    private GameObject item_PrefabScript;

    [SerializeField] private GameObject use_Panel;

    private void Start()
    {
        use_Panel.SetActive(false);
    }

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

    public void OnPointerEnter(PointerEventData eventData)
    {
        //RectTransformUtility.ScreenPointToLocalPointInRectangle(this.GetComponent<RectTransform>(), Input.mousePosition, null, out mousePosition);
        //use_Panel.GetComponent<RectTransform>().anchoredPosition = new Vector2(mousePosition.x, mousePosition.y + 100);

        if (item == null) return;
        Vector2 anchorPos = GetComponent<RectTransform>().anchoredPosition;
        use_Panel.GetComponent<RectTransform>().anchoredPosition = new Vector2(anchorPos.x, anchorPos.y + 800);
        use_Panel.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        use_Panel.SetActive(false);
    }
}
