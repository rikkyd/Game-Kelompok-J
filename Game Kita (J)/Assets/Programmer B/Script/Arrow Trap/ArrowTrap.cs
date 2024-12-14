using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowTrap : MonoBehaviour
{
    public GameObject projectile;
    public Transform spawnLocation;
    public DetectionZone detectionZone;

    public float spawnTime = 0.5f;
    public float timesinceSpawned = 0.1f;
    private Quaternion spawnRotation;

    // Start is called before the first frame update
    void Start()
    {
        spawnRotation = Quaternion.identity;
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
                Debug.Log("Spawning Arrow with Rotation: " + spawnRotation);
                Instantiate(projectile, spawnLocation.position, spawnRotation * Quaternion.Euler(0, 0, 0));
                timesinceSpawned = 0;
            }
        }
        else
        {
            timesinceSpawned = 0.1f;
        }
    }
}
