using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;
using TMPro;

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
    public float healthValue = 0.4f;

    [SerializeField]
    private TMP_Text healthText;

    [SerializeField]
    private GameObject killPanel;

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
                inventoryItem.GetComponent<RectTransform>().rotation = Quaternion.Euler(0, 0, 0f);
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
        killPanel.SetActive(false);
        healthText.text = "";
        healthBar.SetFloat("_Health", healthValue);
    }

    public void AfterSleep()
    {
        for (int i = 0; i < containers.Length; i++)
        {
            if (containers[i].GetComponent<InventorySlot>().item != null)
            {
                GameObject child_ = containers[i].transform.GetChild(0).gameObject;
                Destroy(child_);
                containers[i].GetComponent<InventorySlot>().item = null;
            }
        }

        CheckList(1);

        StopAllCoroutines();
    }

    public void ItemUsed(Item item)
    {
        Debug.Log("check" + item.type);
        if (item.type == Item.ItemType.Consumable && GameManager.Instance.isBW == false)
        {
            ConfidenceIncrease();
        }
        else if(item.type == Item.ItemType.Killable)
        {
            killPanel.SetActive(true);
        }
    }

    public float confidenceFallTime = 1.5f;
    public void BWTransition(PlayableDirector timeline)
    { 
        StartCoroutine(ConfidenceFall());
    }

    public void StopConfidenceCoroutine()
    {
        StopAllCoroutines();
    }

    public IEnumerator ConfidenceFall()
    { 
        while(healthValue > 0.02f)
        { 
            yield return new WaitForSeconds(confidenceFallTime);
            //healthValue = 0.02f;
            healthBar.SetFloat("_Health", healthValue -= 0.015f);
            healthText.text = "-CONFIDENCE";
            healthText.gameObject.GetComponent<Animator>().SetTrigger("Decrease");
        }
    }

    public void ConfidenceIncrease()
    {
        if(GameManager.Instance.isBW == false)
        {
            if (healthValue <= 1f)
            { 
                healthBar.SetFloat("_Health", healthValue += 0.075f);
            }
            healthText.text = "+CONFIDENCE";
            healthText.gameObject.GetComponent<Animator>().SetTrigger("Increase");
        }
    }

    public void ConfidenceIncreaseEndGame()
    {
        if (healthValue <= 1f)
        { 
            healthBar.SetFloat("_Health", healthValue += 0.1f);
        }
        healthText.text = "+CONFIDENCE";
        healthText.gameObject.GetComponent<Animator>().SetTrigger("Increase");
    }

    public void ConfidenceDecreaseEndGame()
    {
        if (healthValue >= 0f)
        {
            healthBar.SetFloat("_Health", healthValue -= 0.05f);
        }
        healthText.text = "-CONFIDENCE";
        healthText.gameObject.GetComponent<Animator>().SetTrigger("Decrease");
    }


}
