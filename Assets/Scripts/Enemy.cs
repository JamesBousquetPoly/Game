using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character, IDamagable
{
    float hitPoints;
    float maxHealth;
    float primaryHitInterval = 1.5f;

    public float damageStrength;
    Coroutine damageCoroutine;


    public override void ResetCharacter()
    {
        hitPoints = startingHitPoints;
    }

    public override void KillCharacter()
    {
        this.gameObject.GetComponent<LootDrop>().DropLoot();
        Destroy(gameObject);
    }

    private void OnEnable()
    {
        ResetCharacter();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Player player = collision.gameObject.GetComponent<Player>();
            if (damageCoroutine == null) 
            {
                damageCoroutine = StartCoroutine(DealDamageToPlayer(primaryHitInterval, player)); // TODO: Need to parameterize the 1.5f interval. 
            }

        }

    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            
            if (damageCoroutine != null)
            {
                StopCoroutine(damageCoroutine);
                damageCoroutine = null;
            }
        }  
    }

    private IEnumerator DealDamageToPlayer(float interval, Player player)
    {
        while (true)
        {
            player.DealDamage(damageStrength);
            if (interval > float.Epsilon)
            {
                yield return new WaitForSeconds(interval);
            }
            else
            {
                break;
            }
        }
    }

    public void DealDamage(float damage)
    {
        StartCoroutine(FlickerCharacter());
        hitPoints -= damage;
        if (hitPoints <= float.Epsilon)
        {
            KillCharacter();
        }
    }
    public void HealDamage(float heal)
    {
        if((hitPoints + heal) <= maxHealth)
        {
            hitPoints += heal;
        } else
        {
            hitPoints = maxHealth;
        }
    }
}
