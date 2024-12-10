using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDropItem : MonoBehaviour
{
    [Header("Drop Settings")]
    public GameObject[] itemPrefabs; // Array untuk menyimpan semua prefab item
    public int dropIndex = 0; // Indeks item yang akan dijatuhkan

    public void DropItem()
    {
        // Periksa apakah indeks valid dan array tidak kosong
        if (itemPrefabs != null && itemPrefabs.Length > 0)
        {
            if (dropIndex >= 0 && dropIndex < itemPrefabs.Length)
            {
                Instantiate(itemPrefabs[dropIndex], transform.position, Quaternion.identity);
                Debug.Log($"Item dengan indeks {dropIndex} dijatuhkan!");
            }
            else
            {
                Debug.LogWarning("Indeks drop item di luar jangkauan!");
            }
        }
        else
        {
            Debug.LogWarning("Tidak ada item prefabs yang diatur!");
        }
    }

    private void OnDestroy()
    {
        // Pastikan item hanya dijatuhkan jika musuh dihancurkan dalam kondisi permainan
        if (gameObject.scene.isLoaded) // Hindari drop saat keluar dari Play Mode
        {
            DropItem();
        }
    }
}