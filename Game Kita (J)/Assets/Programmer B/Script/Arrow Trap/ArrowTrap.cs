using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowTrap : MonoBehaviour
{
    public GameObject projectile;
    public Transform spawnLocation;
    public Quaternion spawnRotation;
    public DetectionZone detectionZone;

    public float spawnTime = 0.5f;
    public float timesinceSpawned = 0.1f;
// Start is called before the first frame update
void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (detectionZone.detectedObj.Count > 0)
        {
            timesinceSpawned += Time.deltaTime;
            Debug.Log("Time Since Spawned: " + timesinceSpawned);

            if (timesinceSpawned >= spawnTime)
            {
                Debug.Log("Spawning Arrow");
                Instantiate(projectile, spawnLocation.position, spawnRotation);
                timesinceSpawned = 0;
            }
        }
        else
        {
            timesinceSpawned = 0.1f;
        }
    }
}
