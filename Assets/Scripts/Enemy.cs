using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character
{
    float hitPoints;

    public int damageStrength;
    Coroutine damageCoroutine;

    // Damage done TO the enemy
    public override IEnumerator DamageCharacter(int damage, float interval)
    {
        while (true)
        {
            StartCoroutine(FlickerCharacter());
            hitPoints = hitPoints - damage;
            if(hitPoints <= float.Epsilon) // see below comment
            {
                KillCharacter();
                break;
            }
            if(interval > float.Epsilon) // float.Epsilon is smallest possible value greater than 0, do this because floats are prone to errors
            {
                yield return new WaitForSeconds(interval);
            } else
            {
                break;
            }
        }
    }

    public override void ResetCharacter()
    {
        hitPoints = startingHitPoints;
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
            if(damageCoroutine == null)
            {
                damageCoroutine = StartCoroutine(player.DamageCharacter(damageStrength, 1.0f));
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
}