/*using UnityEngine;
using Cinemachine;

public class CameraSwitchCinemachine : MonoBehaviour
{
    public CinemachineVirtualCamera playerCam;  // Virtual camera untuk pemain
    public CinemachineVirtualCamera carCam;     // Virtual camera untuk mobil
    public EnterCar enterCarScript;             // Referensi ke script EnterCar

    private float transitionDuration = 1.0f;    // Durasi transisi kamera
    private float transitionProgress = 0f;       // Progres transisi

    void Update()
    {
        // Cek apakah pemain ada di dalam mobil
        if (enterCarScript.isInCar)
        {
            // Jika pemain di dalam mobil, beri waktu untuk kamera mengarah ke mobil
            if (transitionProgress < transitionDuration)
            {
                transitionProgress += Time.deltaTime; // Meningkatkan progres
                float t = transitionProgress / transitionDuration;
                Camera.main.transform.position = Vector3.Lerp(Camera.main.transform.position, carCam.transform.position, t);
                Camera.main.transform.LookAt(carCam.transform.position);
            }
            else
            {
                SwitchToCarCamera();  // Alihkan ke kamera mobil
            }
        }
        else
        {
            transitionProgress = 0; // Reset progres transisi jika pemain keluar
            SwitchToPlayerCamera();  // Alihkan ke kamera pemain
        }
    }

    void SwitchToCarCamera()
    {
        carCam.Priority = 10;  // Set priority camera mobil lebih tinggi
        playerCam.Priority = 5; // Set priority camera pemain lebih rendah
    }

    void SwitchToPlayerCamera()
    {
        carCam.Priority = 5;   // Set priority camera mobil lebih rendah
        playerCam.Priority = 10; // Set priority camera pemain lebih tinggi
    }
}*/
/*
using UnityEngine;
using Cinemachine;

public class CameraSwitchCinemachine : MonoBehaviour
{
    public CinemachineVirtualCamera playerCam;  // Virtual camera untuk pemain
    public CinemachineVirtualCamera carCam;     // Virtual camera untuk mobil
    public EnterCar enterCarScript;             // Referensi ke script EnterCar

    void Update()
    {
        // Cek apakah pemain ada di dalam mobil
        if (enterCarScript.isInCar)
        {
            SwitchToCarCamera();  // Alihkan ke kamera mobil
        }
        else
        {
            SwitchToPlayerCamera();  // Alihkan ke kamera pemain
        }
    }

    void SwitchToCarCamera()
    {
        carCam.Priority = 10;  // Set priority camera mobil lebih tinggi
        playerCam.Priority = 5; // Set priority camera pemain lebih rendah
    }

    void SwitchToPlayerCamera()
    {
        carCam.Priority = 5;   // Set priority camera mobil lebih rendah
        playerCam.Priority = 10; // Set priority camera pemain lebih tinggi
    }
}*/

using UnityEngine;
using Cinemachine;

public class CameraSwitchCinemachine : MonoBehaviour
{
    public CinemachineFreeLook playerCamera;  // Kamera yang mengikuti player
    public CinemachineFreeLook carCamera;     // Kamera yang mengikuti mobil
    public GameObject player;                 // Objek player
    public GameObject car;                    // Objek mobil
    private bool isInCar = false;             // Flag untuk mengecek apakah player sedang berada di mobil

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E)) // Tombol untuk masuk/keluar mobil, misalnya 'E'
        {
            isInCar = !isInCar; // Ubah status apakah player sedang di mobil atau tidak

            if (isInCar)
            {
                EnterCar();
            }
            else
            {
                ExitCar();
            }
        }
    }

    void EnterCar()
    {
        // Matikan player movement dan sembunyikan player jika perlu
        player.SetActive(false);

        // Tingkatkan prioritas kamera mobil, sehingga kamera beralih ke mobil
        carCamera.Priority = 20;
        playerCamera.Priority = 10;
    }

    void ExitCar()
    {
        // Aktifkan kembali player dan posisikan di luar mobil
        player.SetActive(true);

        // Kembalikan prioritas kamera ke player
        playerCamera.Priority = 20;
        carCamera.Priority = 10;
    }
}
