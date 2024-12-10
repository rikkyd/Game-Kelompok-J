using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileKecilBossHarimau : MonoBehaviour
{
    [SerializeField] private int damage;
    [SerializeField] private float timetoLife = 2;
    [SerializeField] private float timesinceSpawned = 0;

    [Header("Freeze Setting")]
    public float freezeChance = 25f;
    public float freezeDuration = 1f;

    private void Update()
    {

        timesinceSpawned += Time.deltaTime;
        if (timesinceSpawned >= timetoLife)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            collision.GetComponent<PlayerController>().TakeDamage(damage);
            Freezing(collision);
            Destroy(gameObject);
        }
    }

    void Freezing(Collider2D player)
    {
        float randomvalue = Random.Range(0f, 100f);
        if (randomvalue < freezeChance)
        {
            Debuff playerController = player.GetComponent<Debuff>();
            if (playerController != null)
            {
                playerController.Freeze(freezeDuration);
            }
        }
    }
}
