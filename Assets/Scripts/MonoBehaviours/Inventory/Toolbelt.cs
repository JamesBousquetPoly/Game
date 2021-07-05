using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 *  This class will be used to handle managing all the different "tool" behaviors available to the player. 
 */
public class Toolbelt : MonoBehaviour
{
    public GameObject prefabToSpawnHoleManager;
    [HideInInspector] public Tools currentTool;
    private HoleManager holeManager;
    private bool isCreatingPort;
    // Start is called before the first frame update
    void Start()
    {
        currentTool = Tools.Pickaxe; // TODO: default to pickaxe, should be removed later

        // Getting Pickaxe logic 
        GameObject holeManagerSpawn = Instantiate(prefabToSpawnHoleManager, transform.position, Quaternion.identity);
        holeManager = holeManagerSpawn.GetComponent<HoleManager>();

    }

    // Update is called once per frame
    void Update()
    {
        holeManager.transform.position = this.transform.position;
        if (Input.GetMouseButtonDown(1) && !isCreatingPort)
        {
            StartCoroutine(CreateHole());
        }
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

    [HideInInspector] 
    public enum Tools
    {
        Pickaxe,
        Axe,
        FishingPole
    }


}
