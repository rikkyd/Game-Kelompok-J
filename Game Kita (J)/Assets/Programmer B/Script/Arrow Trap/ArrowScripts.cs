using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowScripts : MonoBehaviour
{
    public float moveSpeed = 1f;
    public float timetoLife = 4f;
    public float timesinceSpawned = 0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += moveSpeed * transform.right * Time.deltaTime;
        timesinceSpawned += Time.deltaTime;

        if(timesinceSpawned >= timetoLife)
        {
            Destroy(gameObject);
        }
    }
}
