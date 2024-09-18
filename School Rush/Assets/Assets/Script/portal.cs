using UnityEngine;
using UnityEngine.SceneManagement; // Diperlukan untuk pengelolaan scene

public class Portal : MonoBehaviour
{
    // Nama scene yang dituju
    public string targetScene;

    // Fungsi yang akan dijalankan ketika player memasuki trigger
    private void OnTriggerEnter(Collider other)
{
    if (other.CompareTag("Player"))
    {
        Debug.Log("Player entered the portal.");
        SceneManager.LoadScene(targetScene);
    }
}

}
