using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resource : MonoBehaviour
{
    [SerializeField] ResourceSO TreeSO;
    [SerializeField] ResourceSO RockSO;
    private ResourceSO currentResource;
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
                currentResource = RockSO;
                break;
            case RESOURCE_TYPE.WOOD:
                currentResource = TreeSO;
                break;
        }
        LoadResource();
        this.gameObject.GetComponent<SpriteRenderer>().sprite = currentResource.sprite;


    }

    // Update is called once per frame
    void Update()
    {

    }

    private void getHit()
    {
        hitPoints--;
    }

    private void getHit(int hit)
    {
        hitPoints -= hit;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && currentResource.shouldFadeWhenClose)
        {
            Color color = this.gameObject.GetComponent<SpriteRenderer>().color;
            color.a = 0.5f;
            this.gameObject.GetComponent<SpriteRenderer>().color = color;
        }

        if (collision.gameObject.CompareTag("Tool"))
        {
            if(collision.gameObject.GetComponent<Tool>().toolType == currentResource.correspondingTool) // TODO: should this logic be here? I don't think so but I will examine later
            {
                getHit();
                if(hitPoints <= 0)
                {
                    DropResource();
                }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && currentResource.shouldFadeWhenClose)
        {
            Color color = this.gameObject.GetComponent<SpriteRenderer>().color;
            color.a = 1f;
            this.gameObject.GetComponent<SpriteRenderer>().color = color;
        }
    }

    private void LoadWood()
    {
        currentResource = TreeSO;
        this.gameObject.transform.localScale = currentResource.scale;
    }

    private void LoadStone()
    {
        currentResource = RockSO;
    }

    // loads resource once scriptable object has been assigned
    private void LoadResource()
    {
        this.gameObject.transform.localScale = currentResource.scale;
    }

    private void DropResource()
    {

    }
    



    enum RESOURCE_TYPE
    {
        WOOD,
        STONE
    }
}
