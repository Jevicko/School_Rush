using UnityEngine;

public class TaskManager : MonoBehaviour
{
    // Status tugas
    public bool hasShowered = false;
    public bool hasEaten = false;

    // Singleton untuk akses global
    public static TaskManager Instance;

    private void Awake()
    {
        // Singleton pattern
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Cek apakah semua tugas selesai
    public bool AreTasksCompleted()
    {
        return hasShowered && hasEaten;
    }

    // Fungsi untuk menyelesaikan tugas mandi
    public void CompleteShowerTask()
    {
        hasShowered = true;
        Debug.Log("Player has showered.");
    }

    // Fungsi untuk menyelesaikan tugas makan
    public void CompleteEatTask()
    {
        hasEaten = true;
        Debug.Log("Player has eaten.");
    }
}
