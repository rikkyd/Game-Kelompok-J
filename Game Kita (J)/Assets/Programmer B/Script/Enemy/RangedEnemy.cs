using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedEnemy : MonoBehaviour
{
    [Header("Stats")]
    public float speed;
    public float stopDistance;
    public float retreatDistance;
    public float startTimebetweenShot;
    private float timebetweenShot;

    [Header("Reference")]
    public GameObject projectile;
    private Transform player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector2.Distance(transform.position, player.position) > stopDistance) //kalo playernya jauh, musuhnya mendekat
        {
            transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
        }
        else if (Vector2.Distance(transform.position, player.position) < stopDistance && Vector2.Distance(transform.position, player.position) > retreatDistance) //kalo player gak jauh dan gak terlalu dekat, musuh bakal diam
        {
            transform.position = this.transform.position;
        }
        else if(Vector2.Distance(transform.position, player.position) < retreatDistance) // kalo player dekat, musuh akan menjauh
        {
            transform.position = Vector2.MoveTowards(transform.position, player.position, -speed * Time.deltaTime);
        }

        if (timebetweenShot <= 0)
        {
            Instantiate(projectile, transform.position, Quaternion.identity);
            timebetweenShot = startTimebetweenShot;
        }
        else
        {
            timebetweenShot -= Time.deltaTime;
        }
    }


}
