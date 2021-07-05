using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public GameObject slotPrefab;
    public const int numSlots = 5;
    Image[] itemImages = new Image[numSlots];
    Item[] items = new Item[numSlots];
    GameObject[] slots = new GameObject[numSlots];

    // Start is called before the first frame update
    void Start()
    {
        CreateSlots();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CreateSlots()
    {
        if(slotPrefab != null)
        {
            for(int i = 0; i < numSlots; i++)
            {
                GameObject newSlot = Instantiate(slotPrefab);
                newSlot.name = "ItemSlot_" + i;

                newSlot.transform.SetParent(gameObject.transform.GetChild(0).transform);
                slots[i] = newSlot;
                itemImages[i] = newSlot.transform.GetChild(1).GetComponent<Image>();
            }
        }
        else
        {
            print("SlotPrefab null!");
        }
    }

    public bool AddItem(Item itemToAdd)
    {
        if(itemToAdd.sprite == null)
        {
            print("item to add null!");
            return false;
        }

        for(int i = 0; i < items.Length; i++)
        {
            if(items[i] != null && items[i].itemType == itemToAdd.itemType && itemToAdd.stackable == true)
            {
                // add to existing slot

                // add 1 to current quantity
                items[i].quantity = items[i].quantity + 1;

                // Update text
                Slot slotScript = slots[i].gameObject.GetComponent<Slot>();
                Text quantityText = slotScript.qtyText;
                quantityText.enabled = true;
                quantityText.text = items[i].quantity.ToString();
                return true;
            }

            if(items[i] == null)
            {
                // add new item to inventory

                // add item and set picture (do not add text if just one quantity of item)
                items[i] = Instantiate(itemToAdd); // must instantiate copy because otherwise modifying base object
                items[i].quantity = 1;
                if(itemImages[i] == null)
                {
                    print("itemImages[i] null");
                }
                itemImages[i].sprite = itemToAdd.sprite;
                itemImages[i].enabled = true;
                return true;
            }

        }
        return false;
    }
}
