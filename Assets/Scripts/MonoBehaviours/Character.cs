using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Character : MonoBehaviour
{
    /*
     * Almost purely a management script, little real functionality in here. Just used to grab data from other scripts of the character and manage a couple high level things
     */
    
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


    // Getter functions from other classes with the character
    public Weapon.Quadrant GetFacing()
    {
        return this.gameObject.GetComponent<MovementController>().GetFacing();
    }

    public Weapon.Quadrant GetRelativeQuadrant(Transform inputTransform)
    {
        return this.gameObject.GetComponent<Weapon>().GetRelativeQuadrant(inputTransform);
    }

    public abstract void ResetCharacter();
}
