using UnityEngine;
using UnityEngine.SceneManagement;

public class GameFinishManager : MonoBehaviour
{
    public void BackToMainMenu()
    {
        // Muat scene menu utama (misal scene dengan index 0)
        SceneManager.LoadScene(0);
    }

    public void RestartGame()
    {
        // Restart scene saat ini
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1;  // Pastikan waktu berjalan lagi
    }
}
