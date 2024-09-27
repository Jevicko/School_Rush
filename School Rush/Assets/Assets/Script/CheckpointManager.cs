using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CheckpointManager : MonoBehaviour
{
    public GameObject portal; // Referensi ke portal
    public GameObject mandiPanel; // Panel UI untuk mandi
    public GameObject makanPanel; // Panel UI untuk makan
    public GameObject scenemandi;
    public GameObject scenemakan;
    public float panelDuration = 10.0f; // Bisa diatur di Inspector

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
            StartCoroutine(ShowPanelForTimeMandi(10.0f)); // Panel tetap tampil selama 10 detik
            hasBathed = true; // Player telah mandi
            CheckTasks(); // Cek apakah semua tugas selesai
        }

        // Jika player berada di dapur dan menekan "E"
        if (isAtKitchen && Input.GetKeyDown(KeyCode.E) && !hasEaten)
        {
            Debug.Log("sudah makan");
            makanPanel.SetActive(false); // Menyembunyikan panel makan
            StartCoroutine(ShowPanelForTimeMakan(10.0f)); // Panel tetap tampil selama 10 detik
            hasEaten = true; // Player telah makan
            CheckTasks(); // Cek apakah semua tugas selesai
        }
    }

    private IEnumerator ShowPanelForTimeMandi(float panelTime)
    {
        scenemandi.SetActive(true); 
        Time.timeScale = 0; // Pause game
        yield return new WaitForSecondsRealtime(panelTime); // Menunggu 10 detik sebelum menyembunyikan panel
        scenemandi.SetActive(false); 
        Time.timeScale = 1; // Resume game
        
        // Setelah panel mandi ditutup, kurangi 30 detik dari timer
        panelDuration = Mathf.Max(panelDuration - 30.0f, 0);
    }

    private IEnumerator ShowPanelForTimeMakan(float panelTime)
    {
        scenemakan.SetActive(true);
        Time.timeScale = 0; // Pause game
        yield return new WaitForSecondsRealtime(panelTime); // Menunggu 10 detik sebelum menyembunyikan panel
        scenemakan.SetActive(false); 
        Time.timeScale = 1; // Resume game
        
        // Setelah panel makan ditutup, kurangi 30 detik dari timer
        panelDuration = Mathf.Max(panelDuration - 30.0f, 0);
    }

    // Cek apakah semua tugas telah diselesaikan
    void CheckTasks()
    {
        if ((hasBathed = true) && (hasEaten = true))
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
