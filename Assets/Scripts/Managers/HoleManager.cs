using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoleManager : MonoBehaviour
{
    public GameObject prefabToSpawnHole;
    private List<Portal> holes;
    public int maxHoles;
    public float portalCooldown;
    [HideInInspector] public bool canPort;
    // Start is called before the first frame update
    void Start()
    {
        canPort = true;
        portalCooldown = 2.0f;
        holes = new List<Portal>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void addHole()
    {
        
        if(holes.Count >= maxHoles)
        {
            // remove oldest hole
            holes.ToArray()[0].gameObject.SetActive(false);
            Destroy(holes.ToArray()[0].gameObject);
            holes.RemoveAt(0);
            // decrement id in each hole
            foreach(Portal port in holes){
                port.decrementHoleIdentity();
            }
        }
        GameObject hole = Instantiate(prefabToSpawnHole, transform.position, Quaternion.identity);
        Portal hole_port = hole.GetComponent<Portal>();
        hole_port.setHoleIdentity(holes.Count);
        holes.Add(hole_port);
        StartCoroutine(stopPortalUsage());

    }

    public Portal getPreviousHole(int id)
    {
        if (id == 0) return holes.ToArray()[holes.Count - 1];
        else return holes.ToArray()[id - 1];
    }

    public IEnumerator stopPortalUsage()
    {
        canPort = false;
        yield return new WaitForSeconds(portalCooldown);
        canPort = true;
    }


}
