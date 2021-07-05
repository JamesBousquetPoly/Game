using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraLogic : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void LateUpdate()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");  // Get player object
        if (player != null)
        {
            Vector3 pos = player.transform.position;  // Get player position

            Camera.main.transform.position = new Vector3(pos.x, pos.y, Camera.main.transform.position.z);  // Translate main camera to players position (follow)
        }
    }
}
