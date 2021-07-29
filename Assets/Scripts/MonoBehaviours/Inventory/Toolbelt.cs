using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 *  This class will be used to handle managing all the different "tool" behaviors available to the player. 
 */
public class Toolbelt : MonoBehaviour
{
    public GameObject prefabToSpawnHoleManager;
    private HoleManager holeManager;
    private bool isCreatingPort;
    private bool isUsingTool;
    private GameObject tool;
    private Tool toolScript;
    // Start is called before the first frame update
    void Start()
    {
        
        isUsingTool = false;
        tool = this.transform.GetChild(0).gameObject;
        tool.SetActive(false);
        toolScript = tool.GetComponent<Tool>();
        toolScript.SetTool(Tools.Pickaxe);// TODO: default to pickaxe, should be removed later

        // Getting Pickaxe logic 
        GameObject holeManagerSpawn = Instantiate(prefabToSpawnHoleManager, transform.position, Quaternion.identity);
        holeManager = holeManagerSpawn.GetComponent<HoleManager>();

    }

    // Update is called once per frame
    void Update()
    {
        holeManager.transform.position = this.transform.position;
        Tools currentTool = toolScript.GetTool();
        if (Input.GetKeyDown("e"))  // Manage special ability
        {
            
            if ((currentTool == Tools.Pickaxe) && !isCreatingPort)
            {
                StartCoroutine(CreateHole());
            } else if(currentTool == Tools.Axe)
            {

            } else if(currentTool == Tools.FishingPole)
            {

            }
            
        }

        if (Input.GetMouseButtonDown(1) && !isUsingTool) // activate resouce gathering, bad programming design but is fastest to implement
        {
            StartCoroutine(ActivateTool());
        }

        if (Input.GetKeyDown("q")) // Cycle to next tool
        {
            StartCoroutine(SwitchTool());
        }
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
    }
    private IEnumerator CreateHole()
    {
        isCreatingPort = true;
        holeManager.addHole();
        yield return new WaitForSeconds(holeManager.portalCooldown);
        isCreatingPort = false;
    }

    public HoleManager GetHoleManager()
    {
        return holeManager;
    }

    private IEnumerator ActivateTool()
    {
        LockState();
        tool.SetActive(true);
        yield return new WaitForSeconds(.5f);
        tool.SetActive(false);
        toolScript.setGathering(false);
        unLockState();
    }

    private IEnumerator SwitchTool()
    {
        LockState();
        Tools currentTool = toolScript.GetTool();
        currentTool = currentTool + 1;
        if (currentTool >= Tools.END)
        {
            currentTool = 0;
        }
        print("Tool set: " + currentTool);
        toolScript.SetTool(currentTool);
        yield return new WaitForSeconds(0.5f);
        unLockState();
    }

    public void LockState()
    {
        isUsingTool = true;
    }

    public void unLockState()
    {
        isUsingTool = false;
    }

    public bool ToolInUse()
    {
        return isUsingTool;
    }

    [HideInInspector] 
    public enum Tools
    {
        Pickaxe,
        Axe,
        FishingPole,


        END // should always be last
    }


}
