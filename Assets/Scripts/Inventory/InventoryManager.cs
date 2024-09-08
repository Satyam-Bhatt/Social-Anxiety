using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
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
    private PlayableDirector timeline;

    private InventorySlot inventorySlot;

    [SerializeField]
    private Material healthBar;
    private float healthValue = 0.5f;

    private static InventoryManager _instance;
    public static InventoryManager Instance
    {
        get
        {
            _instance = FindObjectOfType<InventoryManager>();
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<InventoryManager>();
            }

            return _instance;
        }
    }

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

        timeline.stopped += BWTransition;
    }
    private void OnDisable()
    {
        for (int i = 0; i < containers.Length; i++)
        {
            inventorySlot = containers[i].GetComponent<InventorySlot>();
            inventorySlot.onUse -= ItemUsed;
        }

        timeline.stopped -= BWTransition;
    }

    private void Start()
    {
        healthBar.SetFloat("_Health", healthValue);
    }

    public void ItemUsed(Item item)
    {
        Debug.Log("check" + item.type);
        if (item.type == Item.ItemType.Consumable && GameManager.Instance.isBW == false)
        {
            healthBar.SetFloat("_Health", healthValue += 0.1f);
        }
    }

    public void BWTransition(PlayableDirector timeline)
    { 
        StartCoroutine(ConfidenceFall());
    }

    IEnumerator ConfidenceFall()
    { 
        while(healthValue > 0f)
        { 
            yield return new WaitForSeconds(0.5f);
            healthBar.SetFloat("_Health", healthValue -= 0.005f);
        }
    }

    public void ConfidenceIncrease()
    {
        if(GameManager.Instance.isBW == false)
        healthBar.SetFloat("_Health", healthValue += 0.1f);
    }


}
