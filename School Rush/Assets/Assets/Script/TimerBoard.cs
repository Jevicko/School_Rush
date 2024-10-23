/*using UnityEngine;
using UnityEngine.UI;

public class TimerBoard : MonoBehaviour
{
    public Text timerText; // Referensi ke UI Text untuk menampilkan waktu akhir
    private Timer timer; // Tambahkan referensi ke script timer

    private void Start()
    {
        // Pastikan panel finish tidak aktif saat permainan dimulai
        gameObject.SetActive(false);
        
        // Mendapatkan referensi ke script Timer
        timer = FindObjectOfType<Timer>(); // Pastikan script Timer ada di scene
    }

    // Panggil fungsi ini untuk menampilkan panel finish dan hasil akhir
    public void ShowFinalTime(float finalTime)
    {
        // Menghitung menit dan detik
        string minutes = ((int)finalTime / 60).ToString();
        string seconds = (finalTime % 60).ToString("f2");

        // Tampilkan waktu akhir di Text
        timerText.text = "Final Time: " + minutes + ":" + seconds;

        // Tampilkan panel finish
        gameObject.SetActive(true);
    }

    // Fungsi ini dapat dipanggil ketika timer selesai
    public void OnTimerFinished()
    {
        float finalTime = timer.GetFinalTime(); // Ganti dengan metode yang sesuai di script Timer
        ShowFinalTime(finalTime);
    }
}


/*using UnityEngine;
using UnityEngine.UI; // Untuk mengakses UI seperti Text

public class TimerBoard : MonoBehaviour
{
    public Text timerText; // Referensi ke UI Text untuk menampilkan waktu akhir

    private void Start()
    {
        // Pastikan panel finish tidak aktif saat permainan dimulai
        gameObject.SetActive(false);
    }

    // Panggil fungsi ini untuk menampilkan panel finish dan hasil akhir
    public void ShowFinalTime(float finalTime)
    {
        // Menghitung menit dan detik
        string minutes = ((int)finalTime / 60).ToString();
        string seconds = (finalTime % 60).ToString("f2");

        // Tampilkan waktu akhir di Text
        timerText.text = "Final Time: " + minutes + ":" + seconds;

        // Tampilkan panel finish
        gameObject.SetActive(true);
    }
}

using UnityEngine;
using UnityEngine.UI;

public class TimerBoard : MonoBehaviour
{
    public Text timerText; // Drag and drop Text UI ke sini

    void Start()
    {
        TimerManager timerManager = FindObjectOfType<TimerManager>();
        float finalTime = timerManager.GetTimer();
        timerText.text = "Waktu Akhir: " + finalTime.ToString("F2") + " detik"; // Format dua desimal
    }
}*/
