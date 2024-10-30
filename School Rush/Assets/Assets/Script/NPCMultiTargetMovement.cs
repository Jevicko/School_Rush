using UnityEngine;
using UnityEngine.AI;

public class NPCMultiTargetMovement : MonoBehaviour
{
    public Transform[] targets;  // Array berisi beberapa target cube
    private NavMeshAgent navMeshAgent;
    private int currentTargetIndex = 0;  // Indeks untuk menandai target yang sedang dituju

    public float stoppingDistance = 1.5f;  // Jarak berhenti saat mendekati target

    void Start()
    {
        // Dapatkan komponen NavMeshAgent dari mobil NPC
        navMeshAgent = GetComponent<NavMeshAgent>();

        // Tentukan kecepatan NPC sesuai dengan mobil
        navMeshAgent.speed = 5f;
        navMeshAgent.acceleration = 8f;

        // Mulai bergerak menuju target pertama
        if (targets.Length > 0)
        {
            navMeshAgent.SetDestination(targets[currentTargetIndex].position);
        }
    }

    void Update()
    {
        // Jika ada target yang ditetapkan
        if (targets.Length == 0)
            return;

        // Periksa jarak ke target saat ini
        if (!navMeshAgent.pathPending && navMeshAgent.remainingDistance <= stoppingDistance)
        {
            // Pindah ke target berikutnya ketika sudah sampai di target saat ini
            currentTargetIndex = (currentTargetIndex + 1) % targets.Length;  // Modulo untuk kembali ke target pertama setelah selesai
            navMeshAgent.SetDestination(targets[currentTargetIndex].position);
        }
    }
}
