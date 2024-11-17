/*using UnityEngine;
using UnityEngine.UI; // Untuk UI Text

public class Gamefinish : MonoBehaviour
{
    public Text timestampText; // UI Text legacy untuk menampilkan waktu

    private void Start()
    {
        if (Timer.instance != null)
        {
            float finalTime = Timer.instance.timeElapsed;
            string minutes = Mathf.Floor(finalTime / 60).ToString("00");
            string seconds = (finalTime % 60).ToString("00");

            timestampText.text = "Time: " + minutes + ":" + seconds;
        }
        else
        {
            timestampText.text = "Timer not found!";
        }
    }
}*/
/*
using UnityEngine;
using UnityEngine.UI;

public class Gamefinish : MonoBehaviour
{
    public Text timestampText; // UI Text legacy untuk menampilkan waktu
    public TimerWithDigitalClock timer; // Referensi ke TimerWithDigitalClock

    private void Start()
    {
        if (timer != null)
        {
            // Akses waktu dari timer yang dihubungkan di Inspector
            float finalTime = timer.elapsedTime;
            string minutes = Mathf.Floor(finalTime / 60).ToString("00");
            string seconds = (finalTime % 60).ToString("00");

            // Tampilkan waktu pada UI
            timestampText.text = "Time: " + minutes + ":" + seconds;
        }
        else
        {
            // Jika timer belum dihubungkan, tampilkan pesan error
            timestampText.text = "Timer not found!";
        }
    }
}*/

using UnityEngine;
using UnityEngine.UI;

public class Gamefinish : MonoBehaviour
{
    public Text timestampText;  // Text untuk menampilkan hasil timer

    private void Start()
    {
        if (TimerWithDigitalClock.instance != null)
        {
            // Mengakses waktu yang telah berlalu dari instance TimerWithDigitalClock
            float finalTime = TimerWithDigitalClock.instance.elapsedTime;

            // Menghitung jam dan menit yang sebenarnya dalam rentang 5:00 hingga 7:00
            float timePercentage = finalTime / TimerWithDigitalClock.instance.totalTime;
            float currentHour = Mathf.Lerp(5f, 7f, timePercentage);  // Menghitung waktu berdasarkan persentase waktu yang telah berlalu

            // Menghitung jam dan menit dari currentHour
            int hours = Mathf.FloorToInt(currentHour);
            int minutes = Mathf.FloorToInt((currentHour - hours) * 60);

            // Tampilkan waktu dalam format HH:MM
            timestampText.text = string.Format("{0:00}:{1:00}", hours, minutes);
        }
        else
        {
            // Tampilkan pesan jika instance TimerWithDigitalClock tidak ditemukan
            timestampText.text = "Timer not found!";
        }
    }
}

