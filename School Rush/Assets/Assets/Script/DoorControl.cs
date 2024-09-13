using System.Collections;
using UnityEngine;

public class DoorControl : MonoBehaviour
{
    public Transform door;      // Objek pintu yang ingin dibuka/tutup
    public float openAngle = 90f; // Sudut terbuka
    public float closeAngle = 0f; // Sudut tertutup
    public float speed = 2f;     // Kecepatan buka/tutup pintu
    private bool isOpen = false; // Status pintu terbuka atau tertutup
    private bool playerNearby = false; // Apakah player dekat dengan pintu

    void Update()
    {
        // Cek jika player dekat dan menekan tombol "E"
        if (playerNearby && Input.GetKeyDown(KeyCode.E))
        {
            // Toggle status pintu
            isOpen = !isOpen;
            StopAllCoroutines(); // Hentikan animasi sebelumnya
            StartCoroutine(MoveDoor(isOpen)); // Jalankan animasi baru
        }
    }

    // Fungsi untuk menggerakkan pintu
    IEnumerator MoveDoor(bool open)
    {
        float targetAngle = open ? openAngle : closeAngle;
        Quaternion targetRotation = Quaternion.Euler(0, targetAngle, 0);

        while (Quaternion.Angle(door.localRotation, targetRotation) > 0.01f)
        {
            door.localRotation = Quaternion.Slerp(door.localRotation, targetRotation, Time.deltaTime * speed);
            yield return null;
        }

        door.localRotation = targetRotation; // Pastikan posisi pintu tepat
    }

    // Deteksi apakah pemain berada di dekat pintu (menggunakan Collider sebagai Trigger)
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerNearby = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerNearby = false;
        }
    }
}
