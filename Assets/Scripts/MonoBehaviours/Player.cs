using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    public HealthBar healthBarPrefab;
    HealthBar healthBar;
    public Inventory inventoryPrefab;
    public HitPoints hitPoints;
    public HoleManager holeManager;

    Inventory inventory;
    public void Start()
    {
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("CanBePickedUp"))
        {
            RetrievableItem itemToPickUp = collision.gameObject.GetComponent<RetrievableItem>();
            Item hitObject = itemToPickUp.getItem();
            

            if((hitObject != null) && (!itemToPickUp.getIsPickedUp()))
            {
                itemToPickUp.setIsPickedUp(true);
                bool shouldDisappear = false;
                switch (hitObject.itemType) // should eventually remove switch, remenent from book code
                {
                    case Item.ItemType.COIN:
                        shouldDisappear = inventory.AddItem(hitObject, itemToPickUp.quantity); //TODO This is where I add an item
                        break;
                    case Item.ItemType.HEALTH:
                        shouldDisappear = AdjustHitPoints(hitObject.quantity);
                        break;
                    default:
                        shouldDisappear = inventory.AddItem(hitObject, itemToPickUp.quantity); //TODO This is where I add an item
                        break;
                }
                if (shouldDisappear)
                {
                    collision.gameObject.SetActive(false);
                } else
                {
                    itemToPickUp.setIsPickedUp(false);
                }

            }
            
        }
    }

    public bool AdjustHitPoints(int amount)
    {
        if(hitPoints.value < maxHitPoints)
        {
            hitPoints.value = hitPoints.value + amount;
            return true;
        }
        return false;
        
    }

    public override IEnumerator DamageCharacter(int damage, float interval)
    {
        while (true)
        {
            StartCoroutine(FlickerCharacter());
            hitPoints.value = hitPoints.value - damage;
            if(hitPoints.value <= float.Epsilon)
            {
                KillCharacter();
                break;
            }
            if(interval > float.Epsilon)
            {
                yield return new WaitForSeconds(interval);
            } else
            {
                break;
            }
        }
    }

    public override void KillCharacter()
    {
        base.KillCharacter();
        Destroy(healthBar.gameObject);
        Destroy(inventory.gameObject);
    }

    public override void ResetCharacter()
    {
        inventory = Instantiate(inventoryPrefab);
        healthBar = Instantiate(healthBarPrefab);
        healthBar.character = this;

        hitPoints.value = startingHitPoints;
    }

    private void OnEnable()
    {
        ResetCharacter();
    }

}
