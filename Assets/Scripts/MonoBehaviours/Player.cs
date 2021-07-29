using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character, IDamagable
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
                        //shouldDisappear = AdjustHitPoints(hitObject.quantity);
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

    public void HealDamage(float amount)
    {
        if((hitPoints.value + amount) < maxHitPoints)
        {
            hitPoints.value = hitPoints.value + amount;
        } else
        {
            hitPoints.value = maxHitPoints;
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

    public void DealDamage(float damage)
    {
        StartCoroutine(FlickerCharacter());
        if (hitPoints.value - damage > float.Epsilon)
        {
            hitPoints.value -= damage;
        } else
        {
            KillCharacter();
        }

    }



}
