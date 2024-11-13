/*using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CheckpointManager : MonoBehaviour
{
    public GameObject portal; // Referensi ke portal
    public GameObject mandiPanel; // Panel UI untuk mandi
    public GameObject makanPanel; // Panel UI untuk makan
    public GameObject scenemandi;
    public GameObject scenemakan;
    public float panelDuration = 10.0f; // Durasi panel default, bisa diatur di Inspector

    private bool isAtBathroom = false; // Apakah player di kamar mandi
    private bool isAtKitchen = false;  // Apakah player di dapur
    private bool hasBathed = false;    // Status mandi
    private bool hasEaten = false;     // Status makan

public class TimerWithDigitalClock : MonoBehaviour {
    public void UpdateDigitalClock()
    {

    }
}

    void Start()
    {
        portal.SetActive(false);  // Pastikan portal tidak aktif saat game dimulai
    }

    void Update()
    {
        // Timer global, mengurangi waktu secara real-time
        if (panelDuration > 0)
        {
            panelDuration -= Time.deltaTime; // Kurangi waktu berdasarkan deltaTime
        }

        // Jika player berada di kamar mandi dan menekan "E"
        if (isAtBathroom && Input.GetKeyDown(KeyCode.E) && !hasBathed)
        {
            hasBathed = true; // Player telah mandi
            mandiPanel.SetActive(false); // Menyembunyikan panel mandi
            StartCoroutine(ShowPanelForTimeMandi(10.0f)); Debug.Log("mandi kurangi 2 menit"); // Tampilkan panel selama 10 detik
            CheckTasks(); // Cek apakah semua tugas selesai
            Debug.Log("sudah mandi");
        }

        // Jika player berada di dapur dan menekan "E"
        if (isAtKitchen && Input.GetKeyDown(KeyCode.E) && !hasEaten)
        {
            hasEaten = true; // Player telah makan
            makanPanel.SetActive(false); // Menyembunyikan panel makan
            StartCoroutine(ShowPanelForTimeMakan(10.0f)); Debug.Log("makan kurangi 2 menit"); // Tampilkan panel selama 10 detik
            CheckTasks(); // Cek apakah semua tugas selesai
            Debug.Log("sudah makan");
        }
        

    }

    private IEnumerator ShowPanelForTimeMandi(float panelTime)
    {
        scenemandi.SetActive(true); 
        Time.timeScale = 0; // Pause game
        yield return new WaitForSecondsRealtime(panelTime); // Menunggu 10 detik sebelum menyembunyikan panel
        scenemandi.SetActive(false); 
        Time.timeScale = 1; // Resume game
        
        // Setelah panel mandi ditutup, kurangi 120 detik dari timer
        panelDuration -= 120f;
    }

    private IEnumerator ShowPanelForTimeMakan(float panelTime)
    {
        scenemakan.SetActive(true);
        Time.timeScale = 0; // Pause game
        yield return new WaitForSecondsRealtime(panelTime); // Menunggu 10 detik sebelum menyembunyikan panel
        scenemakan.SetActive(false); 
        Time.timeScale = 1; // Resume game
        
        // Setelah panel makan ditutup, kurangi 120 detik dari timer
        panelDuration -= 120f;
    }

    // Cek apakah semua tugas telah diselesaikan
    void CheckTasks()
    {
        if ((hasBathed = true) && (hasEaten = true))
        {
            Debug.Log("Semua tugas selesai, portal aktif!");
            // Aktifkan portal jika semua tugas selesai
            portal.SetActive(true);
        }
        else 
        {
            Debug.Log("Tugas belum selesai, portal belum terbuka");
            portal.SetActive(false);
        }
    }

    // Ketika player memasuki kamar mandi atau dapur (Trigger masuk)
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

    // Ketika player keluar dari kamar mandi atau dapur (Trigger keluar)
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
}*/

using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CheckpointManager : MonoBehaviour
{
    public GameObject portal; // Referensi ke portal
    public GameObject mandiPanel; // Panel UI untuk mandi
    public GameObject makanPanel; // Panel UI untuk makan
    public Button mandiButton; // Tombol UI untuk mandi
    public Button makanButton; // Tombol UI untuk makan
    public GameObject scenemandi;
    public GameObject scenemakan;
    public float panelDuration = 10.0f; // Durasi panel default, bisa diatur di Inspector

    private bool isAtBathroom = false; // Apakah player di kamar mandi
    private bool isAtKitchen = false;  // Apakah player di dapur
    private bool hasBathed = false;    // Status mandi
    private bool hasEaten = false;     // Status makan

    void Start()
    {
        portal.SetActive(false);  // Pastikan portal tidak aktif saat game dimulai

        // Menyembunyikan panel mandi dan makan saat game dimulai
        mandiPanel.SetActive(false);
        makanPanel.SetActive(false);

        // Menghubungkan event tombol UI dengan fungsi mandi dan makan
        mandiButton.onClick.AddListener(OnMandiButtonClicked);
        makanButton.onClick.AddListener(OnMakanButtonClicked);
    }

    void Update()
    {
        // Timer global, mengurangi waktu secara real-time
        if (panelDuration > 0)
        {
            panelDuration -= Time.deltaTime; // Kurangi waktu berdasarkan deltaTime
        }
    }

    private void OnMandiButtonClicked()
    {
        if (isAtBathroom && !hasBathed)
        {
            hasBathed = true; // Player telah mandi
            mandiPanel.SetActive(false); // Menyembunyikan panel mandi
            StartCoroutine(ShowPanelForTimeMandi(10.0f)); // Tampilkan panel selama 10 detik
            CheckTasks(); // Cek apakah semua tugas selesai
            Debug.Log("sudah mandi");
        }
    }

    private void OnMakanButtonClicked()
    {
        if (isAtKitchen && !hasEaten)
        {
            hasEaten = true; // Player telah makan
            makanPanel.SetActive(false); // Menyembunyikan panel makan
            StartCoroutine(ShowPanelForTimeMakan(10.0f)); // Tampilkan panel selama 10 detik
            CheckTasks(); // Cek apakah semua tugas selesai
            Debug.Log("sudah makan");
        }
    }

    private IEnumerator ShowPanelForTimeMandi(float panelTime)
    {
        scenemandi.SetActive(true); 
        Time.timeScale = 0; // Pause game
        yield return new WaitForSecondsRealtime(panelTime); // Menunggu 10 detik sebelum menyembunyikan panel
        scenemandi.SetActive(false); 
        Time.timeScale = 1; // Resume game
        
        // Setelah panel mandi ditutup, kurangi 120 detik dari timer
        panelDuration -= 120f;
    }

    private IEnumerator ShowPanelForTimeMakan(float panelTime)
    {
        scenemakan.SetActive(true);
        Time.timeScale = 0; // Pause game
        yield return new WaitForSecondsRealtime(panelTime); // Menunggu 10 detik sebelum menyembunyikan panel
        scenemakan.SetActive(false); 
        Time.timeScale = 1; // Resume game
        
        // Setelah panel makan ditutup, kurangi 120 detik dari timer
        panelDuration -= 120f;
    }

    // Cek apakah semua tugas telah diselesaikan
    void CheckTasks()
    {
        if ((hasBathed = true) && (hasEaten = true))
        {
            Debug.Log("Semua tugas selesai, portal aktif!");
            portal.SetActive(true);
        }
        else 
        {
            Debug.Log("Tugas belum selesai, portal belum terbuka");
            portal.SetActive(false);
        }
    }

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
