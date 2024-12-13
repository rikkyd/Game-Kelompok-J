using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Panah : MonoBehaviour
{
    public float speed;
    public float lifeTime;
    public float distance;
    public int damage; 
    public LayerMask WhatisSolid;
    // Start is called before the first frame update
    private void Start()
    {
        Invoke("DestroyProjectile", lifeTime);
    }

    // Update is called once per frame
    private void Update()
    {
        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, transform.up, distance, WhatisSolid);
        if (hitInfo.collider != null)
        {
            if (hitInfo.collider.CompareTag("Enemy"))
            {
                Debug.Log("Musuh Kena");
                BossBuaya bossBuaya = hitInfo.collider.GetComponent<BossBuaya>();
                if (bossBuaya != null) bossBuaya.TakeDamage(damage);

                BossHarimau bossHarimau = hitInfo.collider.GetComponent<BossHarimau>();
                if (bossHarimau != null) bossHarimau.TakeDamage(damage);

                BossKomodo bossKomodo = hitInfo.collider.GetComponent<BossKomodo>();
                if (bossKomodo != null) bossKomodo.TakeDamage(damage);

                BossBuriswara bossBuriswara = hitInfo.collider.GetComponent<BossBuriswara>();
                if (bossBuriswara != null) bossBuriswara.TakeDamage(damage);
            }
            DestroyProjectile();
        }
        transform.Translate(speed * Time.deltaTime * Vector2.up);
    }

    void DestroyProjectile()
    { 
        Destroy(gameObject);
    }
}
