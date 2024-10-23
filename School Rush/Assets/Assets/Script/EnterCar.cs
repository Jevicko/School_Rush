using UnityEngine;
using Cinemachine;

public class EnterCar : MonoBehaviour
{
    public GameObject car;                      // Referensi ke mobil
    public GameObject player;                   // Referensi ke karakter pemain
    public GameObject enterCarPanel;            // Referensi ke panel UI
    public Transform seatPosition;              // Posisi duduk di dalam mobil
    public Transform exitPosition;              // Posisi keluar dari mobil
    public bool isNearCar = false;              // Apakah karakter dekat dengan mobil
    public bool isInCar = false;                // Apakah karakter sudah di dalam mobil

    private CarController carController;        // Referensi ke script CarController

    public CinemachineFreeLook playerCamera;    // Kamera yang mengikuti player
    public CinemachineFreeLook carCamera;       // Kamera yang mengikuti mobil

    void Start()
    {
        // Sembunyikan panel saat memulai
        enterCarPanel.SetActive(false);

        // Dapatkan komponen CarController dari mobil
        carController = car.GetComponent<CarController>();

        // Pastikan kontrol mobil tidak aktif saat memulai
        carController.enabled = false;

        // Setel prioritas kamera, agar kamera player aktif di awal
        playerCamera.Priority = 20;
        carCamera.Priority = 10;
    }

    void Update()
    {
        if (isNearCar && !isInCar)
        {
            // Munculkan panel ketika karakter dekat dengan mobil
            enterCarPanel.SetActive(true);

            // Deteksi jika pemain menekan "F" untuk masuk ke dalam mobil
            if (Input.GetKeyDown(KeyCode.F))
            {
                EnterTheCar();
            }
        }
        else if (isInCar)
        {
            // Deteksi jika pemain menekan "F" untuk keluar dari mobil
            if (Input.GetKeyDown(KeyCode.F))
            {
                ExitTheCar();
            }
        }

        // Sembunyikan panel jika karakter menjauh atau sudah di dalam mobil
        if (!isNearCar || isInCar)
        {
            enterCarPanel.SetActive(false);
        }
    }

    // Memasuki mobil
    void EnterTheCar()
    {
        // Matikan kontrol karakter
        player.SetActive(false);

        // Posisikan karakter di tempat duduk mobil
        player.transform.position = seatPosition.position;

        // Aktifkan kontrol mobil
        carController.enabled = true;

        // Perpindahan ke kamera mobil
        carCamera.Priority = 20;
        playerCamera.Priority = 10;

        // Tandai bahwa karakter sudah di dalam mobil
        isInCar = true;
    }

    // Keluar dari mobil
    void ExitTheCar()
    {
        // Matikan kontrol mobil
        carController.enabled = false;

        // Pindahkan karakter ke posisi keluar dari mobil
        player.transform.position = exitPosition.position;

        // Aktifkan kontrol karakter kembali
        player.SetActive(true);

        // Perpindahan ke kamera player
        playerCamera.Priority = 20;
        carCamera.Priority = 10;

        // Tandai bahwa karakter sudah keluar dari mobil
        isInCar = false;
    }

    // Deteksi ketika karakter memasuki area trigger mobil
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isNearCar = true;
        }
    }

    // Deteksi ketika karakter menjauh dari area trigger mobil
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isNearCar = false;
        }
    }
}
