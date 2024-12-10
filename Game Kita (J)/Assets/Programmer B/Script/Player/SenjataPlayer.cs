using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SenjataPlayer : MonoBehaviour
{
    public float offset;

    public GameObject projectile;
    public Transform shotPoint;

    private void Update()
    {
        // Update the rotation to follow the crosshair
        Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        float rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rotZ + offset);
    }

    public void Shoot()
    {
        if (projectile != null && shotPoint != null)
        {
            Instantiate(projectile, shotPoint.position, transform.rotation);
        }
        else
        {
            Debug.LogWarning("Projectile or ShotPoint is not assigned.");
        }
    }
}