using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    private int ID;
    HoleManager parent;

    void Start()
    {

    }

    void Update()
    {
        
    }

    void portalPlayer()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Player player = collision.gameObject.GetComponent<Player>();
            if(parent == null)
            {
                parent = player.GetComponent<Toolbelt>().GetHoleManager();
            }

            if ((ID >= 0) && (parent != null))
            {
                Portal linkedHole = parent.getPreviousHole(ID);
                if(linkedHole != null && parent.canPort)
                {
                    player.transform.position = linkedHole.transform.position;
                    StartCoroutine(parent.stopPortalUsage());
                }
            }
        }

    }

    public void setHoleIdentity(int id)
    {
        
        ID = id;
    }

    public void decrementHoleIdentity()
    {
        ID--;
    }

    
}
