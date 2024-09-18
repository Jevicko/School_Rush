using UnityEngine;
using UnityEngine.UI;

public class CheckpointManager : MonoBehaviour
{
    public GameObject portal; // Referensi ke portal
    public GameObject mandiPanel; // Panel UI untuk mandi
    public GameObject makanPanel; // Panel UI untuk makan

    private bool isAtBathroom = false; // Apakah player di kamar mandi
    private bool isAtKitchen = false;  // Apakah player di dapur
    private bool hasBathed = false;    // Status mandi
    private bool hasEaten = false;     // Status makan

    void Start()
{
    portal.SetActive(false);  // Pastikan portal tidak aktif saat game dimulai
}


    void Update()
    {
        // Jika player berada di kamar mandi dan menekan "E"
        if (isAtBathroom && Input.GetKeyDown(KeyCode.E) && !hasBathed)
        {
            Debug.Log("sudah mandi");
            mandiPanel.SetActive(false); // Menyembunyikan panel mandi
            hasBathed = true; // Player telah mandi
            CheckTasks(); // Cek apakah semua tugas selesai
        }

        // Jika player berada di dapur dan menekan "E"
        if (isAtKitchen && Input.GetKeyDown(KeyCode.E) && !hasEaten)
        {
            Debug.Log("sudah makan");
            makanPanel.SetActive(false); // Menyembunyikan panel makan
            hasEaten = true; // Player telah makan
            CheckTasks(); // Cek apakah semua tugas selesai
        }
    }

    // Cek apakah semua tugas telah diselesaikan
    void CheckTasks()
    {
        if (hasBathed && hasEaten)
        {
            Debug.Log("sudah diklik semua");
            // Aktifkan portal jika semua tugas selesai
            portal.SetActive(true);
        }
        else 
        {
            Debug.Log("portal belum terbuka");
            portal.SetActive(false);
        }
    }

    // Ketika player memasuki kamar mandi (Trigger kamar mandi)
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (gameObject.CompareTag("Bathroom"))
            {
                mandiPanel.SetActive(true); // Tampilkan panel mandi
                isAtBathroom = true;
            }
            else if (gameObject.CompareTag("Kitchen"))
            {
                makanPanel.SetActive(true); // Tampilkan panel makan
                isAtKitchen = true;
            }
        }
    }

    // Ketika player keluar dari kamar mandi atau dapur
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (gameObject.CompareTag("Bathroom"))
            {
                mandiPanel.SetActive(false); // Sembunyikan panel mandi
                isAtBathroom = false;
            }
            else if (gameObject.CompareTag("Kitchen"))
            {
                makanPanel.SetActive(false); // Sembunyikan panel makan
                isAtKitchen = false;
            }
        }
    }
}