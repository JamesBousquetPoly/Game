using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    public GameObject prefabToSpawn;
    public float repeatInterval;


    void Start()
    {
        if (repeatInterval > 0)
        {
            InvokeRepeating("SpawnObject", 0.0f, repeatInterval);
        }
        
    }



    
    void Update()
    {
        
    }


    public GameObject SpawnObject()
    {
        if(prefabToSpawn != null)
        {
            return Instantiate(prefabToSpawn, transform.position, Quaternion.identity);
        }
        print("Spawn point probably not configured properly, returning null from SpawnPoint.SpawnObject()");
        return null;
    }
}
