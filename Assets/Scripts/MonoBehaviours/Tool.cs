using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tool : MonoBehaviour
{

    private Toolbelt.Tools toolType;
    private Character player;

    private int axePower;
    private int pickaxePower;
    private int fishingPolePower;
    private bool isGathering;


    private int animationState; // use to control animation state of tool
    GameObject parent;
    // Start is called before the first frame update
    void Start()
    {
        parent = transform.parent.gameObject;
        player = parent.GetComponent<Character>();
        axePower = 1;
        pickaxePower = 1;
        fishingPolePower = 1;
        isGathering = false;
    }

    // Update is called once per frame
    void Update()
    {
        this.gameObject.transform.position = transform.parent.position;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Resource"))
        {
            Weapon.Quadrant facing = player.GetFacing();
            Weapon.Quadrant relativeQuadrant;
            if (facing == Weapon.Quadrant.North || facing == Weapon.Quadrant.South)
            {
                relativeQuadrant = getRelativePosition(collision.transform.position)[1];
            } else
            {
                relativeQuadrant = getRelativePosition(collision.transform.position)[0];
            }

            if((facing == relativeQuadrant) && (collision.gameObject.GetComponent<Resource>().GetCurrentResource().correspondingTool == toolType) && (!isGathering))
            {
                setGathering(true);
                collision.gameObject.GetComponent<Resource>().getHit(getToolPower(toolType));
            }
        }
    }

    public void SetTool(Toolbelt.Tools toolToSwitch)
    {
        toolType = toolToSwitch;
    }

    public Toolbelt.Tools GetTool()
    {
        return toolType;
    }

    public void setToolPower(int power, Toolbelt.Tools toolToChange)
    {
        switch (toolToChange)
        {
            case Toolbelt.Tools.Axe:
                axePower = power;
                break;
            case Toolbelt.Tools.Pickaxe:
                pickaxePower = power;
                break;
            case Toolbelt.Tools.FishingPole:
                fishingPolePower = power;
                break;
        }
    }

    public int getToolPower(Toolbelt.Tools toolToGet)
    {
        switch (toolToGet)
        {
            case Toolbelt.Tools.Axe:
                return axePower;
            case Toolbelt.Tools.Pickaxe:
                return pickaxePower;
            case Toolbelt.Tools.FishingPole:
                return fishingPolePower;
            default:
                return 0;
        }
    }

    public void setGathering(bool gathering)
    {
        isGathering = gathering;
    }

    private Weapon.Quadrant[] getRelativePosition(Vector2 position) //  2 large, [West or East, North or South]
    {
        Vector2 toolPosition = this.gameObject.transform.position;
        Weapon.Quadrant[] retval = new Weapon.Quadrant[2];

        Vector2 diffVector = toolPosition - position;
        if(diffVector.x >= 0)
        {
            retval[0] = Weapon.Quadrant.West;
        } else
        {
            retval[0] = Weapon.Quadrant.East;
        }

        if(diffVector.y >= 0)
        {
            retval[1] = Weapon.Quadrant.South;
        } else
        {
            retval[1] = Weapon.Quadrant.North;
        }
        return retval;


        
    }


    
}
