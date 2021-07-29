using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ammo : MonoBehaviour
{
    public int damageInflicted;


    void Start()
    {
        
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision is BoxCollider2D)
        {
            Enemy enemy = collision.gameObject.GetComponent<Enemy>();
            enemy.DealDamage((float)damageInflicted);
            gameObject.SetActive(false);
        }
    }
}
