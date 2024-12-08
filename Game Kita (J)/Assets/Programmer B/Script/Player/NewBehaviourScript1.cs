using UnityEngine;

public class ScaleOnCollision2D : MonoBehaviour
{
    // Variabel untuk menyimpan skala awal
    private Vector2 originalScale;
    private bool hasScaled = false;

    private void Start()
    {
        // Menyimpan skala awal objek sebagai Vector2
        originalScale = transform.localScale;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Memeriksa apakah objek yang disentuh memiliki tag "Power" dan belum digandakan
        if (other.CompareTag("Power") && !hasScaled)
        {
            // Menggandakan skala objek ini
            transform.localScale = originalScale * 2;
            hasScaled = true; // Mencegah penggandaan lebih dari sekali
        }
    }
}
