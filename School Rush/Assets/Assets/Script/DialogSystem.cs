/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // Untuk mengakses UI
using TMPro;         // Jika menggunakan TextMeshPro

public class DialogSystem : MonoBehaviour
{
    public GameObject dialogPanel;  // Panel dialog
    public TextMeshProUGUI dialogText;  // Text dialog
    public Button continueButton;   // Tombol untuk melanjutkan

    private bool isDialogActive = true; // Status dialog

    void Start()
    {
        // Awal game, dialog aktif dan waktu dihentikan
        dialogPanel.SetActive(true); 
        //dialogText.text = "Selamat datang di Kota! Tekan tombol untuk melanjutkan.";
        Time.timeScale = 0f;  // Hentikan game
        continueButton.onClick.AddListener(ContinueGame);  // Tambahkan listener ke tombol
    }

    // Fungsi untuk melanjutkan game saat tombol ditekan
    void ContinueGame()
    {
        dialogPanel.SetActive(false);  // Sembunyikan dialog
        Time.timeScale = 1f;           // Kembali jalankan game
        isDialogActive = false;
    }
}

using UnityEngine;
using UnityEngine.UI; // Untuk UI
using System.Collections;

public class DialogSystem : MonoBehaviour
{
    public GameObject dialogPanel; // Panel dialog yang akan ditampilkan
    public Button continueButton;  // Tombol untuk melanjutkan permainan
    public float delayTime = 5f;   // Waktu tunda sebelum dialog muncul (5 detik)

    void Start()
    {
        // Memulai coroutine untuk menampilkan dialog setelah 5 detik
        StartCoroutine(ShowDialogAfterDelay());

        // Tambahkan listener pada tombol continue
        continueButton.onClick.AddListener(ContinueGame);
    }

    // Coroutine untuk menunggu beberapa detik sebelum menampilkan dialog
    IEnumerator ShowDialogAfterDelay()
    {
        // Menunggu 5 detik
        yield return new WaitForSeconds(delayTime);

        // Menampilkan dialog setelah waktu tunda
        ShowDialog();
    }

    // Fungsi untuk menampilkan dialog dan menghentikan game
    void ShowDialog()
    {
        dialogPanel.SetActive(true); // Menampilkan panel dialog
        Time.timeScale = 0f;         // Pause game
    }

    // Fungsi untuk melanjutkan game saat tombol ditekan
    void ContinueGame()
    {
        dialogPanel.SetActive(false); // Sembunyikan panel dialog
        Time.timeScale = 1f;          // Resume game
    }
}*/
/*
using UnityEngine;
using UnityEngine.UI; // Untuk UI
using System.Collections;

public class DialogSystem : MonoBehaviour
{
    public GameObject dialogPanel; // Panel dialog yang akan ditampilkan
    public Button continueButton;  // Tombol untuk melanjutkan permainan
    public float delayTime = 2f;   // Waktu tunda sebelum dialog muncul (5 detik)

    void Start()
    {
        // Sembunyikan dialog panel di awal permainan
        dialogPanel.SetActive(false);

        // Memulai coroutine untuk menampilkan dialog setelah 5 detik
        StartCoroutine(ShowDialogAfterDelay());

        // Tambahkan listener pada tombol continue
        continueButton.onClick.AddListener(ContinueGame);
    }

    // Coroutine untuk menunggu beberapa detik sebelum menampilkan dialog
    IEnumerator ShowDialogAfterDelay()
    {
        // Menunggu 5 detik
        yield return new WaitForSeconds(delayTime);

        // Menampilkan dialog setelah waktu tunda
        ShowDialog();
    }

    // Fungsi untuk menampilkan dialog dan menghentikan game
    void ShowDialog()
    {
        dialogPanel.SetActive(true); // Menampilkan panel dialog
        Time.timeScale = 0f;         // Pause game
    }

    // Fungsi untuk melanjutkan game saat tombol ditekan
    void ContinueGame()
    {
        dialogPanel.SetActive(false); // Sembunyikan panel dialog
        Time.timeScale = 1f;          // Resume game
    }
}*/
/*
using UnityEngine;
using UnityEngine.UI; // Untuk UI
using System.Collections;

public class DialogSystem : MonoBehaviour
{
    public GameObject dialogPanel; // Panel dialog yang akan ditampilkan
    public Button continueButton;  // Tombol untuk melanjutkan permainan
    public float delayTime = 2f;   // Waktu tunda sebelum dialog muncul (5 detik)

    void Start()
    {
        // Sembunyikan dialog panel di awal permainan
        dialogPanel.SetActive(false);

        // Memulai coroutine untuk menampilkan dialog setelah delay
        StartCoroutine(ShowDialogAfterDelay());

        // Tambahkan listener pada tombol continue
        continueButton.onClick.AddListener(ContinueGame);
    }

    // Coroutine untuk menunggu beberapa detik sebelum menampilkan dialog
    IEnumerator ShowDialogAfterDelay()
    {
        // Menunggu sesuai delayTime
        yield return new WaitForSeconds(delayTime);

        // Cek apakah kamera utama yang sedang aktif
        if (Camera.main != null && Camera.main.name == "Main Camera")
        {
            // Menampilkan dialog jika Main Camera aktif
            ShowDialog();
        }
    }

    // Fungsi untuk menampilkan dialog dan menghentikan game
    void ShowDialog()
    {
        Debug.Log("Panel Dialog aktif: " + dialogPanel.activeSelf); // Memeriksa status aktif panel
        dialogPanel.SetActive(true); // Menampilkan panel dialog
        Time.timeScale = 0f;         // Pause game
    }

    // Fungsi untuk melanjutkan game saat tombol ditekan
    void ContinueGame()
    {
        dialogPanel.SetActive(false); // Sembunyikan panel dialog
        Time.timeScale = 1f;          // Resume game
    }
}*/

using UnityEngine;
using UnityEngine.UI; // Untuk UI
using System.Collections;

public class DialogSystem : MonoBehaviour
{
    public GameObject dialogPanel; // Panel dialog yang akan ditampilkan
    public Button continueButton;  // Tombol untuk melanjutkan permainan
    public Camera mainCamera;      // Referensi ke kamera utama (akan diakses melalui Inspector)
    public float delayTime = 2f;   // Waktu tunda sebelum dialog muncul (5 detik)

    void Start()
    {
        // Sembunyikan dialog panel di awal permainan
        dialogPanel.SetActive(false);

        // Memulai coroutine untuk menampilkan dialog setelah delay
        StartCoroutine(ShowDialogAfterDelay());

        // Tambahkan listener pada tombol continue
        continueButton.onClick.AddListener(ContinueGame);
    }

    // Coroutine untuk menunggu beberapa detik sebelum menampilkan dialog
    IEnumerator ShowDialogAfterDelay()
    {
        // Menunggu sesuai delayTime
        yield return new WaitForSeconds(delayTime);

        // Cek apakah kamera utama yang diberikan di Inspector
        if (mainCamera != null && mainCamera.name == "Main Camera")
        {
            // Menampilkan dialog jika Main Camera aktif
            ShowDialog();
        }
    }

    // Fungsi untuk menampilkan dialog dan menghentikan game
    void ShowDialog()
    {
        Debug.Log("Panel Dialog aktif: " + dialogPanel.activeSelf); // Memeriksa status aktif panel
        dialogPanel.SetActive(true); // Menampilkan panel dialog
        Time.timeScale = 0f;         // Pause game
    }

    // Fungsi untuk melanjutkan game saat tombol ditekan
    void ContinueGame()
    {
        dialogPanel.SetActive(false); // Sembunyikan panel dialog
        Time.timeScale = 1f;          // Resume game
    }
}
