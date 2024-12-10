using UnityEngine;

public class CameraController : MonoBehaviour
{
    // Room camera
    [SerializeField] private float speed;
    private float currentPosX;
    private Vector3 velocity = Vector3.zero;

    // Follow player
    [SerializeField] private Transform player;
    [SerializeField] private float aheadDistance; // Jarak kamera di depan pemain
    [SerializeField] private float cameraSpeed;   // Kecepatan kamera mengikuti
    private Vector3 lookAheadPosition;

    private void Update()
    {
        //Room camera (pindah ke posisi ruangan baru)
        //Vector3 targetRoomPosition = new Vector3(currentPosX, transform.position.y, transform.position.z);
        //transform.position = Vector3.SmoothDamp(transform.position, targetRoomPosition, ref velocity, speed);

        // Follow player
        Vector3 playerDirection = player.GetComponent<Rigidbody2D>().velocity.normalized; // Arah pergerakan pemain
        lookAheadPosition = player.position + (Vector3)playerDirection * aheadDistance;

        // Lerping kamera ke posisi baru
        transform.position = Vector3.Lerp(transform.position, new Vector3(lookAheadPosition.x, lookAheadPosition.y, transform.position.z), Time.deltaTime * cameraSpeed);
    }

    public void MoveToNewRoom(Transform _newRoom)
    {
        currentPosX = _newRoom.position.x;
    }
}
