/*using UnityEngine;
using UnityEngine.SceneManagement; // Jika ingin mengakhiri dengan ganti scene

public class checkpointFinish : MonoBehaviour
{
    // Nama tag dari karakter pemain
    public string playerTag = "Player";

    // Fungsi ini dipanggil ketika sesuatu masuk ke dalam trigger collider
    private void OnTriggerEnter(Collider other)
    {
        // Periksa apakah objek yang masuk adalah pemain
        if (other.CompareTag(playerTag))
        {
            Debug.Log("Pemain sudah mencapai sekolah!");

            // Mengakhiri game atau melakukan aksi lain
            EndGame();
        }
    }

    // Fungsi untuk mengakhiri game atau berpindah ke scene lain
    void EndGame()
    {
        // Cara 1: Menampilkan pesan "Game Over"
        Debug.Log("Game Selesai!");

        // Cara 2: Jika ingin reload atau pindah scene (misalnya, Scene 0 adalah menu utama)
        // SceneManager.LoadScene(0);
        
        // Cara 3: Jika ingin keluar dari aplikasi
        // Application.Quit();
    }
}*/

using UnityEngine;
using UnityEngine.UI;

public class Checkpoint : MonoBehaviour
{
    public string playerTag = "Player";
    public GameObject gameFinishPanel;  // Referensi ke panel UI Game Finish

    private void OnTriggerEnter(Collider other)
    {
        // Periksa apakah objek yang masuk adalah pemain
        if (other.CompareTag(playerTag))
        {
            Debug.Log("Pemain sudah mencapai sekolah!");

            // Tampilkan panel "Game Finish"
            ShowGameFinishPanel();

            // Hentikan game
            EndGame();
        }
    }

    // Fungsi untuk menampilkan panel Game Finish
    void ShowGameFinishPanel()
    {
        if (gameFinishPanel != null)
        {
            gameFinishPanel.SetActive(true);  // Aktifkan panel
        }
    }

    // Fungsi untuk mengakhiri game
    void EndGame()
    {
        // Nonaktifkan semua input atau hentikan waktu (opsional)
        Time.timeScale = 0;  // Menghentikan waktu di game (Pause)

        // Tambahan: bisa mengunci kontrol player
        // Contoh: jika menggunakan CharacterController atau sistem input lain, nonaktifkan di sini
    }
}

