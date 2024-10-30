/*using UnityEngine;

public class AutoFly : MonoBehaviour
{
    public Transform[] targets;  // Array untuk menyimpan target cube
    public float speed = 7f;     // Kecepatan pesawat
    public float rotationSpeed = 2f; // Kecepatan rotasi pesawat
    public float distanceThreshold = 1f;  // Jarak minimum untuk berpindah ke target berikutnya

    private int currentTargetIndex = 0;  // Target yang sedang dituju

    void Update()
    {
        // Jika semua target sudah dicapai
        if (currentTargetIndex >= targets.Length)
            return;

        // Dapatkan posisi target saat ini
        Transform target = targets[currentTargetIndex];
        
        // Arahkan pesawat menuju target
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);
        
        // Gerakkan pesawat ke depan
        transform.position += transform.forward * speed * Time.deltaTime;

        // Periksa apakah pesawat sudah mendekati target
        if (Vector3.Distance(transform.position, target.position) < distanceThreshold)
        {
            // Berpindah ke target berikutnya
            currentTargetIndex++;
        }
    }
}*/

using UnityEngine;

public class AutoFly : MonoBehaviour
{
    public Transform[] targets;  // Array untuk menyimpan target cube
    public float speed = 15f;     // Kecepatan pesawat
    public float rotationSpeed = 2f; // Kecepatan rotasi pesawat
    public float distanceThreshold = 1f;  // Jarak minimum untuk berpindah ke target berikutnya

    private int currentTargetIndex = 0;  // Target yang sedang dituju

    void Update()
    {
        // Jika tidak ada target, keluar dari update
        if (targets.Length == 0)
            return;

        // Dapatkan posisi target saat ini
        Transform target = targets[currentTargetIndex];
        
        // Arahkan pesawat menuju target
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);
        
        // Gerakkan pesawat ke depan
        transform.position += transform.forward * speed * Time.deltaTime;

        // Periksa apakah pesawat sudah mendekati target
        if (Vector3.Distance(transform.position, target.position) < distanceThreshold)
        {
            // Berpindah ke target berikutnya
            currentTargetIndex++;

            // Jika sudah mencapai target terakhir, kembali ke target pertama
            if (currentTargetIndex >= targets.Length)
            {
                currentTargetIndex = 0;  // Reset ke target pertama
            }
        }
    }
}

