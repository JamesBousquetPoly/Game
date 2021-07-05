using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Character : MonoBehaviour
{

    
    public float maxHitPoints;
    public float startingHitPoints;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

     public virtual void KillCharacter()
    {
        Destroy(gameObject);
    }

    public virtual IEnumerator FlickerCharacter()
    {
        GetComponent<SpriteRenderer>().color = Color.red;
        yield return new WaitForSeconds(0.1f);
        GetComponent<SpriteRenderer>().color = Color.white;
    }

    public abstract void ResetCharacter();

    public abstract IEnumerator DamageCharacter(int damage, float interval);
}
