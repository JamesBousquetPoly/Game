using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resource : MonoBehaviour
{
    [SerializeField] Sprite TreeSprite;
    [SerializeField] Sprite StoneSprite;
    RESOURCE_TYPE type;
    private int hitPoints = 4;
    // Start is called before the first frame update
    void Start()
    {
        int rand = Random.Range(0,2);
        type = (RESOURCE_TYPE)rand;
        print("type: " + type);
        switch (type)
        {
            case RESOURCE_TYPE.STONE:
                LoadStone();
                break;
            case RESOURCE_TYPE.WOOD:
                LoadWood();
                break;
        }

        
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

    private void LoadWood()
    {
        this.gameObject.GetComponent<SpriteRenderer>().sprite = TreeSprite;
    }

    private void LoadStone()
    {
        this.gameObject.GetComponent<SpriteRenderer>().sprite = StoneSprite;
    }
    



    enum RESOURCE_TYPE
    {
        WOOD,
        STONE
    }
}
