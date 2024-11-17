using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SenjataPlayer : MonoBehaviour
{
    public float offset;

    public GameObject projectile;
    public Transform shotPoint;

    public float startTimeBtwShots;
    private float TimeBtwShots;
    // Start is called before the first frame update
    private void Update()
    {
        Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        float rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rotZ + offset);

        if (TimeBtwShots <= 0)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Instantiate(projectile, shotPoint.position, transform.rotation);
                TimeBtwShots = startTimeBtwShots;
            }
        }
        else
        {
            TimeBtwShots -= Time.deltaTime;
        }


    }


}
