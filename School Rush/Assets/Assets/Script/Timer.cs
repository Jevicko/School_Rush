/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour
{
    public static Timer instance; // Singleton instance

    public Slider sliderTimer;
    public GameObject gameOverPanel; // Panel Game Over
    public GameObject warningPanel; // Panel merah transparan untuk peringatan
    public float warningTime = 30f; // Waktu sebelum timer habis untuk mulai peringatan
    public float blinkSpeed = 0.5f; // Kecepatan kedip (dalam detik)
    private bool isWarning = false; // Status apakah peringatan sedang aktif
    bool isFinish = false;

    public float initialTimerValue = 900f; // Nilai awal timer

    void Awake()
    {
        // Singleton pattern: only one instance of Timer should exist
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Prevent destruction when scene changes
        }
        else
        {
            Destroy(gameObject); // Destroy duplicate Timer objects
        }
    }

    void Start()
    {
        // Mengambil nilai slider dari scene sebelumnya (jika ada) atau reset jika tidak ada
        if (PlayerPrefs.HasKey("timerValue"))
        {
            sliderTimer.value = PlayerPrefs.GetFloat("timerValue");
        }
        else
        {
            sliderTimer.value = initialTimerValue; // Atur timer ke nilai awal
        }

        // Pastikan panel game over dan warning tersembunyi di awal
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(false);
        }

        if (warningPanel != null)
        {
            warningPanel.SetActive(false); // Sembunyikan panel peringatan di awal
        }

        // Reset time scale to normal if starting the game
        Time.timeScale = 1;
    }

    void Update()
    {
        timer();
    }

    void timer()
    {
        if (sliderTimer.value > sliderTimer.minValue)
        {
            sliderTimer.value -= Time.deltaTime;

            // Simpan nilai timer ke PlayerPrefs agar bisa diakses di scene lain
            PlayerPrefs.SetFloat("timerValue", sliderTimer.value);

            // Jika waktu tersisa kurang dari warningTime dan peringatan belum aktif
            if (sliderTimer.value <= warningTime && !isWarning)
            {
                isWarning = true;
                StartCoroutine(BlinkWarning()); // Mulai peringatan kedip
            }
        }
        else
        {
            if (!isFinish)
            {
                Debug.Log("Time Out!!!");

                isFinish = true;
                GameOver(); // Panggil fungsi game over
            }
        }
    }

    // Coroutine untuk membuat panel peringatan berkedip
    IEnumerator BlinkWarning()
    {
        if (warningPanel != null)
        {
            while (sliderTimer.value > 0 && !isFinish)
            {
                warningPanel.SetActive(true); // Tampilkan panel
                yield return new WaitForSeconds(blinkSpeed); // Tunggu selama blinkSpeed detik
                warningPanel.SetActive(false); // Sembunyikan panel
                yield return new WaitForSeconds(blinkSpeed); // Tunggu lagi untuk kedipan berikutnya
            }
        }
    }

    void GameOver()
    {
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(true); // Tampilkan panel Game Over
        }

        // Hentikan permainan
        Time.timeScale = 0; // Bekukan seluruh aktivitas game
    }

    // Fungsi untuk memulai permainan baru
    public void NewGame()
    {
        // Reset nilai timer ke posisi awal
        sliderTimer.value = initialTimerValue;
        
        // Hapus data lama yang tersimpan
        PlayerPrefs.DeleteKey("timerValue");

        // Load scene permainan dari awal
        SceneManager.LoadScene("Home");
    }

    // Fungsi untuk memuat scene menu dan reset timer
    public void GoToMenu()
    {
        // Reset nilai timer ke posisi awal saat menuju menu
        sliderTimer.value = initialTimerValue;

        // Hapus nilai yang tersimpan di PlayerPrefs
        PlayerPrefs.DeleteKey("timerValue");

        // Load scene menu
        SceneManager.LoadScene("menu 3d");
    }

    // Fungsi untuk memuat scene baru sambil mempertahankan nilai timer
    public void LoadNewScene(string sceneName)
    {
        // Simpan nilai timer sebelum berganti scene
        PlayerPrefs.SetFloat("timerValue", sliderTimer.value);
        PlayerPrefs.Save();

        // Ganti scene
        SceneManager.LoadScene(sceneName);
    }
}*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour
{
    public static Timer instance; // Singleton instance

    public Slider sliderTimer;
    public GameObject gameOverPanel; // Panel Game Over
    public GameObject warningPanel; // Panel merah transparan untuk peringatan
    public float warningTime = 30f; // Waktu sebelum timer habis untuk mulai peringatan
    public float blinkSpeed = 0.5f; // Kecepatan kedip (dalam detik)
    private bool isWarning = false; // Status apakah peringatan sedang aktif
    bool isFinish = false;

    public float initialTimerValue = 100f; // Nilai awal timer

    void Awake()
    {
        // Singleton pattern: only one instance of Timer should exist
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject.transform.root.gameObject);
           // DontDestroyOnLoad(gameObject); // Prevent destruction when scene changes
        }
        else
        {
            Destroy(gameObject); // Destroy duplicate Timer objects
        }
    }

    void Start()
    {
        // Cek jika nilai timer tersimpan
        if (PlayerPrefs.HasKey("timerValue"))
        {
            sliderTimer.value = PlayerPrefs.GetFloat("timerValue");
        }
        else
        {
            sliderTimer.value = initialTimerValue; // Atur timer ke nilai awal
        }

        // Pastikan panel game over dan warning tersembunyi di awal
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(false);
        }

        if (warningPanel != null)
        {
            warningPanel.SetActive(false); // Sembunyikan panel peringatan di awal
        }

        // Reset time scale to normal if starting the game
        Time.timeScale = 1;
    }

    void Update()
    {
        timer();
    }

    void timer()
    {
        if (sliderTimer.value > sliderTimer.minValue)
        {
            sliderTimer.value -= Time.deltaTime;

            // Simpan nilai timer ke PlayerPrefs agar bisa diakses di scene lain
            PlayerPrefs.SetFloat("timerValue", sliderTimer.value);

            // Jika waktu tersisa kurang dari warningTime dan peringatan belum aktif
            if (sliderTimer.value <= warningTime && !isWarning)
            {
                isWarning = true;
                StartCoroutine(BlinkWarning()); // Mulai peringatan kedip
            }
        }
        else
        {
            if (!isFinish)
            {
                Debug.Log("Time Out!!!");

                isFinish = true;
                GameOver(); // Panggil fungsi game over
            }
        }
    }

    // Coroutine untuk membuat panel peringatan berkedip
    IEnumerator BlinkWarning()
    {
        if (warningPanel != null)
        {
            while (sliderTimer.value > 0 && !isFinish)
            {
                warningPanel.SetActive(true); // Tampilkan panel
                yield return new WaitForSeconds(blinkSpeed); // Tunggu selama blinkSpeed detik
                warningPanel.SetActive(false); // Sembunyikan panel
                yield return new WaitForSeconds(blinkSpeed); // Tunggu lagi untuk kedipan berikutnya
            }
        }
    }

    void GameOver()
    {
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(true); // Tampilkan panel Game Over
        }

        // Hentikan permainan
        Time.timeScale = 0; // Bekukan seluruh aktivitas game
    }

    // Fungsi untuk memulai permainan baru
    public void NewGame()
    {
        // Reset nilai timer ke posisi awal
        sliderTimer.value = initialTimerValue;
        
        // Hapus data lama yang tersimpan
        PlayerPrefs.DeleteKey("timerValue");

        // Load scene permainan dari awal
        SceneManager.LoadScene("Home");
    }

    // Fungsi untuk memuat scene menu dan reset timer
    public void GoToMenu()
    {
        // Reset nilai timer ke posisi awal saat menuju menu
        sliderTimer.value = initialTimerValue;

        // Hapus nilai yang tersimpan di PlayerPrefs
        PlayerPrefs.DeleteKey("timerValue");

        // Load scene menu
        SceneManager.LoadScene("menu 3d");
    }

    // Fungsi untuk memuat scene baru sambil mempertahankan nilai timer
    public void LoadNewScene(string sceneName)
    {
        // Simpan nilai timer sebelum berganti scene
        PlayerPrefs.SetFloat("timerValue", sliderTimer.value);
        PlayerPrefs.Save();

        // Ganti scene
        SceneManager.LoadScene(sceneName);
    }

}
