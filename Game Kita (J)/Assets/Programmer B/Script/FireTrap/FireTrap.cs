using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireTrap : MonoBehaviour
{
    [Header("Firetrap Timer")]
    [SerializeField] private float activationDelay;
    [SerializeField] private float activeTime;
    [SerializeField] private int damage;

    private Animator anim;
    private SpriteRenderer spriteRenderer;

    private bool trigger;
    private bool active;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            //trigger fire trap
        }
        if (active)
        {
            collision.GetComponent<PlayerController>().TakeDamage(damage);
        }
    }

    private IEnumerator ActiveFiretrap ()
    {
        trigger = true;
        yield return new WaitForSeconds(activationDelay);
        active = true;
    }
}
