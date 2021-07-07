using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resource : MonoBehaviour
{
    [SerializeField] Sprite TreeSprite;
    private int hitPoints = 4;
    // Start is called before the first frame update
    void Start()
    {
        this.gameObject.GetComponent<SpriteRenderer>().sprite = TreeSprite;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void getHit()
    {
        hitPoints--;
    }

    public void getHit(int hit)
    {
        hitPoints -= hit;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Color color = this.gameObject.GetComponent<SpriteRenderer>().color;
            color.a = 0.5f;
            this.gameObject.GetComponent<SpriteRenderer>().color = color;
        }

/*        if (collision.gameObject.CompareTag("Tool"))
        {
            collision.gameObject.GetComponent<Tool>();
        }*/
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Color color = this.gameObject.GetComponent<SpriteRenderer>().color;
            color.a = 1f;
            this.gameObject.GetComponent<SpriteRenderer>().color = color;
        }
    }



    enum RESOURCE_TYPE
    {
        WOOD
    }
}
