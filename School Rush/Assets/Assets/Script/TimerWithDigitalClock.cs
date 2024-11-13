using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class TimerWithDigitalClock : MonoBehaviour
{
    public Text digitalClockText; // Referensi ke UI Text untuk menampilkan jam
    public GameObject warningPanel; // Panel yang muncul 30 detik sebelum waktu habis
    public GameObject gameOverPanel; // Panel yang muncul saat waktu habis
    public GameObject makanPanel; // Panel Makan
    public GameObject mandiPanel; // Panel Mandi
    private float totalTime = 5 * 60f; // Waktu total 5 menit dalam detik
    private float startHour = 5f; // Jam mulai (5:00)
    private float endHour = 7f; // Jam akhir (7:00)
    private bool timerRunning = true;
    private bool warningShown = false; // Mengecek apakah panel warning sudah muncul
    private float elapsedTime = 0f; // Waktu yang telah berlalu
    private static TimerWithDigitalClock instance; // Untuk menyimpan instance

    void Awake()
    {
        // Pastikan hanya ada satu instance dari script ini dan jangan dihancurkan saat pindah scene
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Jangan hancurkan GameObject saat scene berganti
        }
        else
        {
            Destroy(gameObject); // Hancurkan duplikatnya
        }
    }

    void Start()
    {
        warningPanel.SetActive(false); // Awalnya panel warning tidak aktif
        gameOverPanel.SetActive(false); // Panel gameover juga tidak aktif
        makanPanel.SetActive(false); // Panel makan tidak aktif
        mandiPanel.SetActive(false); // Panel mandi tidak aktif
        StartCoroutine(StartTimer()); // Mulai timer

        // Berlangganan event saat scene baru dimuat
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    IEnumerator StartTimer()
    {
        while (elapsedTime < totalTime && timerRunning)
        {
            elapsedTime += Time.deltaTime;

            // Hitung persentase waktu dari total 15 menit
            float timePercentage = elapsedTime / totalTime;

            // Hitung jam dari 5:00 hingga 7:00
            float currentHour = Mathf.Lerp(startHour, endHour, timePercentage);

            // Update tampilan jam digital
            UpdateDigitalClock(currentHour);

            // Cek apakah waktu yang tersisa kurang dari 30 detik
            if (totalTime - elapsedTime <= 30f && !warningShown)
            {
                ShowWarningPanel(); // Tampilkan panel warning
                warningShown = true;
            }

            yield return null;
        }

        // Saat waktu habis, tampilkan panel gameover
        if (elapsedTime >= totalTime)
        {
            ShowGameOverPanel();
        }
    }

    void UpdateDigitalClock(float currentHour)
    {
        // Pisahkan jam dan menit dari currentHour
        int hours = Mathf.FloorToInt(currentHour);
        int minutes = Mathf.FloorToInt((currentHour - hours) * 60);

        // Format jam digital dalam UI (HH:MM)
        digitalClockText.text = string.Format("{0:00}:{1:00}", hours, minutes);
    }

    void ShowWarningPanel()
    {
        warningPanel.SetActive(true); // Tampilkan panel warning
        StartCoroutine(BlinkWarningPanel());
    }

    void ShowGameOverPanel()
    {
        gameOverPanel.SetActive(true); // Tampilkan panel gameover
        timerRunning = false; // Hentikan timer
        Time.timeScale = 0;
    }

    // Fungsi untuk reset timer saat scene menu dibuka
    public void ResetTimer()
    {
        elapsedTime = 0f; // Reset waktu
        timerRunning = true;
        warningShown = false;
        warningPanel.SetActive(false);
        gameOverPanel.SetActive(false);
        makanPanel.SetActive(false); // Reset panel makan
        mandiPanel.SetActive(false); // Reset panel mandi
        StartCoroutine(StartTimer());
    }

    // Fungsi ini dipanggil saat sebuah scene dimuat
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Cek jika scene yang dimuat adalah Menu
        if (scene.name == "menu 3d")
        {
            ResetTimer(); // Reset timer saat kembali ke menu
        }
    }

    void OnDestroy()
    {
        // Unsubscribe dari event ketika script ini dihancurkan
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    IEnumerator BlinkWarningPanel()
    {
        float blinkDuration = 0.5f; // Durasi berkedip (0.5 detik on/off)
        
        while (elapsedTime < totalTime)
        {
            // Tampilkan panel
            warningPanel.SetActive(true);
            yield return new WaitForSeconds(blinkDuration); // Tunggu durasi blink

            // Sembunyikan panel
            warningPanel.SetActive(false);
            yield return new WaitForSeconds(blinkDuration); // Tunggu durasi blink
        }

        // Jika waktu habis, panel akan tetap menyala
        warningPanel.SetActive(true);
    }

    // Fungsi untuk mengurangi waktu ketika panel makan diakses
    public void AccessMakanPanel()
    {
        makanPanel.SetActive(true); // Tampilkan panel makan
        totalTime -= 120f; // Kurangi waktu 2 menit (120 detik)
        if (totalTime < 0f) totalTime = 0f; // Pastikan totalTime tidak negatif
    }

    // Fungsi untuk mengurangi waktu ketika panel mandi diakses
    public void AccessMandiPanel()
    {
        mandiPanel.SetActive(true); // Tampilkan panel mandi
        totalTime -= 120f; // Kurangi waktu 2 menit (120 detik)
        if (totalTime < 0f) totalTime = 0f; // Pastikan totalTime tidak negatif
    }
}

