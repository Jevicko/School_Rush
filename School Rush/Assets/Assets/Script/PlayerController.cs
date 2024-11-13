/*using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Rendering;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Animator animator;
    public CharacterController characterController;

    public AudioSource audioSource;        // Audio Source untuk memutar suara
    public AudioClip walkingClip;          // Suara saat berjalan
    public AudioClip runningClip;          // Suara saat berlari
    public AudioClip jumpingClip;          // Suara saat melompat

    public bool isGrounded;
    bool isMoving;
    bool isJumping;
    bool isRunning;

    Vector3 velocity = Vector3.zero;

    float speed = 0f;
    float speedWalking = 2f;
    float speedRunning = 4f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = checkGrounded();
        animator.SetBool("isGrounded", isGrounded);

        // cek input lari / shift sebelah kiri
        isRunning = Input.GetAxis("Lari") > 0f;
        animator.SetBool("isRunning", isRunning);
        speed = isRunning ? speedRunning : speedWalking;

        // cek input WASD atau panah
        float inputX = Input.GetAxis("Horizontal");
        float inputY = Input.GetAxis("Vertical");

        if (Mathf.Abs(inputX) > 0.1f || Mathf.Abs(inputY) > 0.1f)
        {
            float sudutInput = Mathf.Atan2(inputX, inputY) * 180f / 3.14f;
            float sudutCamera = Camera.main.transform.eulerAngles.y;
            float sudutTarget = sudutInput + sudutCamera;

            transform.rotation = Quaternion.Euler(0f, sudutTarget, 0f);
            if (isGrounded && !isJumping)
            {
                float y = velocity.y;
                velocity = transform.rotation * Vector3.forward * speed;
                velocity.y = y;
                animator.SetBool("isMoving", true);

                // Memutar suara berjalan atau berlari
                if (!audioSource.isPlaying)
                {
                    audioSource.clip = isRunning ? runningClip : walkingClip;
                    audioSource.Play();
                }
            }
        }
        else
        {
            velocity.x = 0;
            velocity.z = 0;
            animator.SetBool("isMoving", false);
            audioSource.Stop(); // Stop audio ketika karakter berhenti
        }

        // cek pergerakan lompat
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            animator.SetBool("isJumping", true);
            animator.SetBool("isMoving", false);
            isJumping = true;

            // Memutar suara lompat
            audioSource.clip = jumpingClip;
            audioSource.Play();

            StartCoroutine(clearJumping());
        }

        // gravitasi
        if (!isGrounded)
        {
            velocity.y -= 30 * Time.deltaTime;
            animator.SetBool("isMoving", false);
        }
        else if (velocity.y < 0)
        {
            velocity.y = -10f;
        }

        // gerakin player berdasarkan velocity
        characterController.Move(velocity * Time.deltaTime);
    }

    IEnumerator clearJumping()
    {
        yield return new WaitForSeconds(0.1f);
        animator.SetBool("isJumping", false);
        isJumping = false;
        velocity.y = 5;
    }

    bool checkGrounded()
    {
        RaycastHit hit;
        int layerMask = 1 << 9;
        layerMask = ~layerMask;
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), out hit, 0.4899999f, layerMask))
        {
            return true;
        }
        return false;
    }
}

using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public Animator animator;
    public CharacterController characterController;

    public AudioSource audioSource;        // Audio Source untuk memutar suara
    public AudioClip walkingClip;          // Suara saat berjalan
    public AudioClip runningClip;          // Suara saat berlari
    public AudioClip jumpingClip;          // Suara saat melompat

    public Joystick joystick;              // Referensi untuk joystick
    public Button runButton;               // Referensi untuk tombol lari
    public Button jumpButton;              // Referensi untuk tombol lompat
    public bool isGrounded;
    bool isMoving;
    bool isJumping;
    bool isRunning;

    Vector3 velocity = Vector3.zero;

    float speed = 0f;
    float speedWalking = 2f;
    float speedRunning = 4f;

    void Start()
    {
        // Hubungkan fungsi lompat ke tombol lompat
        jumpButton.onClick.AddListener(Jump);
    }

    void Update()
    {
        isGrounded = checkGrounded();
        animator.SetBool("isGrounded", isGrounded);

        // Mengatur kecepatan berdasarkan status lari
        speed = isRunning ? speedRunning : speedWalking;

        // Mengambil input dari joystick untuk arah gerakan
        float inputX = joystick.Horizontal;
        float inputY = joystick.Vertical;

        if (Mathf.Abs(inputX) > 0.1f || Mathf.Abs(inputY) > 0.1f)
        {
            float sudutInput = Mathf.Atan2(inputX, inputY) * Mathf.Rad2Deg;
            float sudutCamera = Camera.main.transform.eulerAngles.y;
            float sudutTarget = sudutInput + sudutCamera;

            transform.rotation = Quaternion.Euler(0f, sudutTarget, 0f);
            if (isGrounded && !isJumping)
            {
                float y = velocity.y;
                velocity = transform.rotation * Vector3.forward * speed;
                velocity.y = y;
                animator.SetBool("isMoving", true);

                // Memutar suara berjalan atau berlari
                if (!audioSource.isPlaying)
                {
                    audioSource.clip = isRunning ? runningClip : walkingClip;
                    audioSource.Play();
                }
            }
        }
        else
        {
            velocity.x = 0;
            velocity.z = 0;
            animator.SetBool("isMoving", false);
            audioSource.Stop(); // Berhenti memutar audio ketika karakter berhenti
        }

        // Gravitasi
        if (!isGrounded)
        {
            velocity.y -= 30 * Time.deltaTime;
            animator.SetBool("isMoving", false);
        }
        else if (velocity.y < 0)
        {
            velocity.y = -10f;
        }

        // Gerakkan player berdasarkan velocity
        characterController.Move(velocity * Time.deltaTime);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        // Mulai lari ketika tombol ditekan
        if (eventData.pointerCurrentRaycast.gameObject == runButton.gameObject)
        {
            isRunning = true;
            animator.SetBool("isRunning", true);
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        // Berhenti lari ketika tombol dilepaskan
        if (eventData.pointerCurrentRaycast.gameObject == runButton.gameObject)
        {
            isRunning = false;
            animator.SetBool("isRunning", false);
        }
    }

    void Jump()
    {
        if (isGrounded && !isJumping)
        {
            animator.SetBool("isJumping", true);
            animator.SetBool("isMoving", false);
            isJumping = true;

            // Memutar suara lompat
            audioSource.clip = jumpingClip;
            audioSource.Play();

            StartCoroutine(clearJumping());
        }
    }

    IEnumerator clearJumping()
    {
        yield return new WaitForSeconds(0.1f);
        animator.SetBool("isJumping", false);
        isJumping = false;
        velocity.y = 5;
    }

    bool checkGrounded()
    {
        RaycastHit hit;
        int layerMask = 1 << 9;
        layerMask = ~layerMask;
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), out hit, 0.4899999f, layerMask))
        {
            return true;
        }
        return false;
    }
}*/
/*
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public Animator animator;
    public CharacterController characterController;

    public AudioSource audioSource;
    public AudioClip walkingClip;
    public AudioClip runningClip;
    public AudioClip jumpingClip;

    public Joystick joystick;
    public Button runButton;
    public Button jumpButton;

    public Camera playerCamera;            // Referensi untuk kamera
    public float cameraSensitivity = 0.2f; // Sensitivitas pergerakan kamera

    private bool isGrounded;
    private bool isJumping;
    private bool isRunning;
    private Vector3 velocity = Vector3.zero;

    private float speedWalking = 2f;
    private float speedRunning = 4f;

    void Start()
    {
        jumpButton.onClick.AddListener(Jump);
    }

    void Update()
    {
        isGrounded = checkGrounded();
        animator.SetBool("isGrounded", isGrounded);
        float speed = isRunning ? speedRunning : speedWalking;

        // Mengambil input dari joystick
        float inputX = joystick.Horizontal;
        float inputY = joystick.Vertical;

        if (Mathf.Abs(inputX) > 0.1f || Mathf.Abs(inputY) > 0.1f)
        {
            float sudutInput = Mathf.Atan2(inputX, inputY) * Mathf.Rad2Deg;
            float sudutCamera = Camera.main.transform.eulerAngles.y;
            float sudutTarget = sudutInput + sudutCamera;

            transform.rotation = Quaternion.Euler(0f, sudutTarget, 0f);
            if (isGrounded && !isJumping)
            {
                float y = velocity.y;
                velocity = transform.rotation * Vector3.forward * speed;
                velocity.y = y;
                animator.SetBool("isMoving", true);

                if (!audioSource.isPlaying)
                {
                    audioSource.clip = isRunning ? runningClip : walkingClip;
                    audioSource.Play();
                }
            }
        }
        else
        {
            velocity.x = 0;
            velocity.z = 0;
            animator.SetBool("isMoving", false);
            audioSource.Stop();
        }

        if (!isGrounded)
        {
            velocity.y -= 30 * Time.deltaTime;
            animator.SetBool("isMoving", false);
        }
        else if (velocity.y < 0)
        {
            velocity.y = -10f;
        }

        characterController.Move(velocity * Time.deltaTime);

        // Kontrol kamera sisi kanan layar
        ControlCamera();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (eventData.pointerCurrentRaycast.gameObject == runButton.gameObject)
        {
            isRunning = true;
            animator.SetBool("isRunning", true);
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (eventData.pointerCurrentRaycast.gameObject == runButton.gameObject)
        {
            isRunning = false;
            animator.SetBool("isRunning", false);
        }
    }

    void Jump()
    {
        if (isGrounded && !isJumping)
        {
            animator.SetBool("isJumping", true);
            animator.SetBool("isMoving", false);
            isJumping = true;
            audioSource.clip = jumpingClip;
            audioSource.Play();
            StartCoroutine(clearJumping());
        }
    }

    IEnumerator clearJumping()
    {
        yield return new WaitForSeconds(0.1f);
        animator.SetBool("isJumping", false);
        isJumping = false;
        velocity.y = 5;
    }

    bool checkGrounded()
    {
        RaycastHit hit;
        int layerMask = 1 << 9;
        layerMask = ~layerMask;
        return Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), out hit, 0.4899999f, layerMask);
    }

    private void ControlCamera()
    {
        // Periksa jumlah sentuhan di layar
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            // Pastikan sentuhan terjadi di sisi kanan layar
            if (touch.position.x > Screen.width / 2)
            {
                Debug.Log("layar kanan dipencet");
                // Rotasi kamera berdasarkan gerakan sentuhan
                Vector2 deltaTouch = touch.deltaPosition;
                float rotationX = deltaTouch.y * cameraSensitivity;
                float rotationY = deltaTouch.x * cameraSensitivity;

                playerCamera.transform.Rotate(-rotationX, rotationY, 0);

                // Hindari rotasi yang tidak diinginkan pada sumbu z
                Vector3 cameraEulerAngles = playerCamera.transform.eulerAngles;
                playerCamera.transform.eulerAngles = new Vector3(cameraEulerAngles.x, cameraEulerAngles.y, 0);
            }
        }
    }
}
*/ 
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public Animator animator;
    public CharacterController characterController;

    public AudioSource audioSource;
    public AudioClip walkingClip;
    public AudioClip runningClip;
    public AudioClip jumpingClip;

    public Joystick joystick;
    public Button runButton;
    public Button jumpButton;

    public Camera playerCamera;             // Referensi untuk kamera
    public Transform cameraTarget;          // Target yang diikuti kamera
    public float cameraSensitivity = 0.2f;  // Sensitivitas pergerakan kamera
    public float maxCameraDistance = 5f;    // Jarak maksimum kamera dari target

    private bool isGrounded;
    private bool isJumping;
    private bool isRunning;
    private Vector3 velocity = Vector3.zero;

    private float speedWalking = 2f;
    private float speedRunning = 4f;
    private float cameraRotationX = 0f;
    private float cameraRotationY = 0f;
    private Vector3 offsetCameraPosition = new Vector3(0, 1.5f, -3f); // Offset posisi kamera dari target

    void Start()
    {
        runButton.onClick.AddListener(ToggleRun);
        jumpButton.onClick.AddListener(Jump);
    }

    void Update()
    {
        isGrounded = checkGrounded();
        animator.SetBool("isGrounded", isGrounded);
        float speed = isRunning ? speedRunning : speedWalking;

        // Mengambil input dari joystick
        float inputX = joystick.Horizontal;
        float inputY = joystick.Vertical;

        if (Mathf.Abs(inputX) > 0.1f || Mathf.Abs(inputY) > 0.1f)
        {
            float sudutInput = Mathf.Atan2(inputX, inputY) * Mathf.Rad2Deg;
            float sudutCamera = playerCamera.transform.eulerAngles.y;
            float sudutTarget = sudutInput + sudutCamera;

            transform.rotation = Quaternion.Euler(0f, sudutTarget, 0f);
            if (isGrounded && !isJumping)
            {
                float y = velocity.y;
                velocity = transform.rotation * Vector3.forward * speed;
                velocity.y = y;
                animator.SetBool("isMoving", true);

                if (!audioSource.isPlaying)
                {
                    audioSource.clip = isRunning ? runningClip : walkingClip;
                    audioSource.Play();
                }
            }
        }
        else
        {
            velocity.x = 0;
            velocity.z = 0;
            animator.SetBool("isMoving", false);
            audioSource.Stop();
        }

        if (!isGrounded)
        {
            velocity.y -= 30 * Time.deltaTime;
            animator.SetBool("isMoving", false);
        }
        else if (velocity.y < 0)
        {
            velocity.y = -10f;
        }

        characterController.Move(velocity * Time.deltaTime);

        // Kontrol kamera untuk tetap mengarah pada target
        ControlCamera();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (eventData.pointerCurrentRaycast.gameObject == runButton.gameObject)
        {
            isRunning = true;
            animator.SetBool("isRunning", true);
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (eventData.pointerCurrentRaycast.gameObject == runButton.gameObject)
        {
            isRunning = false;
            animator.SetBool("isRunning", false);
        }
    }

    void ToggleRun()
    {
        // Mengaktifkan atau menonaktifkan lari
        isRunning = !isRunning;
        animator.SetBool("isRunning", isRunning);
    }

    void Jump()
    {
        if (isGrounded && !isJumping)
        {
            animator.SetBool("isJumping", true);
            animator.SetBool("isMoving", false);
            isJumping = true;
            audioSource.clip = jumpingClip;
            audioSource.Play();
            StartCoroutine(clearJumping());
        }
    }

    IEnumerator clearJumping()
    {
        yield return new WaitForSeconds(0.1f);
        animator.SetBool("isJumping", false);
        isJumping = false;
        velocity.y = 5;
    }

    bool checkGrounded()
    {
        RaycastHit hit;
        int layerMask = 1 << 9;
        layerMask = ~layerMask;
        return Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), out hit, 0.4899999f, layerMask);
    }

    private void ControlCamera()
    {
        // Mengecek input sentuhan untuk kontrol kamera
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            // Mengecek apakah sentuhan terjadi di sisi kanan layar
            if (touch.position.x > Screen.width / 2)
            {
                Vector2 deltaTouch = touch.deltaPosition;
                cameraRotationY += deltaTouch.x * cameraSensitivity;
                cameraRotationX -= deltaTouch.y * cameraSensitivity;
                cameraRotationX = Mathf.Clamp(cameraRotationX, -45f, 45f); // Batasi rotasi ke atas/bawah
            }
        }

        // Update posisi kamera berdasarkan target dan offset
        Vector3 desiredPosition = cameraTarget.position + Quaternion.Euler(cameraRotationX, cameraRotationY, 0f) * offsetCameraPosition;
        
        // Hitung jarak antara kamera dan target
        float distanceToTarget = Vector3.Distance(cameraTarget.position, desiredPosition);
        if (distanceToTarget > maxCameraDistance)
        {
            // Batasi jarak maksimum kamera
            desiredPosition = cameraTarget.position + (desiredPosition - cameraTarget.position).normalized * maxCameraDistance;
        }

        playerCamera.transform.position = desiredPosition;
        
        // Pastikan kamera menghadap target
        playerCamera.transform.LookAt(cameraTarget.position);
    }
}

/*
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public Animator animator;
    public CharacterController characterController;

    public AudioSource audioSource;
    public AudioClip walkingClip;
    public AudioClip runningClip;
    public AudioClip jumpingClip;

    public Joystick joystick;
    public Button runButton;
    public Button jumpButton;

    public Camera playerCamera;             // Referensi untuk kamera
    public Transform cameraTarget;          // Target yang diikuti kamera
    public float cameraSensitivity = 0.2f;  // Sensitivitas pergerakan kamera
    public float maxCameraDistance = 5f;    // Jarak maksimum kamera dari target

    private bool isGrounded;
    private bool isJumping;
    private bool isRunning;
    private Vector3 velocity = Vector3.zero;

    private float speedWalking = 2f;
    private float speedRunning = 4f;
    private float cameraRotationX = 0f;
    private float cameraRotationY = 0f;
    private Vector3 offsetCameraPosition = new Vector3(0, 1.5f, -3f); // Offset posisi kamera dari target

    void Start()
    {
        jumpButton.onClick.AddListener(Jump);

        // Mengatur playerCamera ke kamera utama jika belum diset
        if (playerCamera == null)
        {
            playerCamera = Camera.main; // Mengambil kamera dengan tag "Main Camera"
        }
    }

    void Update()
    {
        isGrounded = checkGrounded();
        animator.SetBool("isGrounded", isGrounded);
        float speed = isRunning ? speedRunning : speedWalking;

        // Mengambil input dari joystick
        float inputX = joystick.Horizontal;
        float inputY = joystick.Vertical;

        if (Mathf.Abs(inputX) > 0.1f || Mathf.Abs(inputY) > 0.1f)
        {
            float sudutInput = Mathf.Atan2(inputX, inputY) * Mathf.Rad2Deg;
            float sudutCamera = playerCamera.transform.eulerAngles.y;
            float sudutTarget = sudutInput + sudutCamera;

            transform.rotation = Quaternion.Euler(0f, sudutTarget, 0f);
            if (isGrounded && !isJumping)
            {
                float y = velocity.y;
                velocity = transform.rotation * Vector3.forward * speed;
                velocity.y = y;
                animator.SetBool("isMoving", true);

                if (!audioSource.isPlaying)
                {
                    audioSource.clip = isRunning ? runningClip : walkingClip;
                    audioSource.Play();
                }
            }
        }
        else
        {
            velocity.x = 0;
            velocity.z = 0;
            animator.SetBool("isMoving", false);
            audioSource.Stop();
        }

        if (!isGrounded)
        {
            velocity.y -= 30 * Time.deltaTime;
            animator.SetBool("isMoving", false);
        }
        else if (velocity.y < 0)
        {
            velocity.y = -10f;
        }

        characterController.Move(velocity * Time.deltaTime);

        // Kontrol kamera untuk tetap mengarah pada target
        ControlCamera();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (eventData.pointerCurrentRaycast.gameObject == runButton.gameObject)
        {
            isRunning = true;
            animator.SetBool("isRunning", true);
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (eventData.pointerCurrentRaycast.gameObject == runButton.gameObject)
        {
            isRunning = false;
            animator.SetBool("isRunning", false);
        }
    }

    void Jump()
    {
        if (isGrounded && !isJumping)
        {
            animator.SetBool("isJumping", true);
            animator.SetBool("isMoving", false);
            isJumping = true;
            audioSource.clip = jumpingClip;
            audioSource.Play();
            StartCoroutine(clearJumping());
        }
    }

    IEnumerator clearJumping()
    {
        yield return new WaitForSeconds(0.1f);
        animator.SetBool("isJumping", false);
        isJumping = false;
        velocity.y = 5;
    }

    bool checkGrounded()
    {
        RaycastHit hit;
        int layerMask = 1 << 9;
        layerMask = ~layerMask;
        return Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), out hit, 0.4899999f, layerMask);
    }

    private void ControlCamera()
    {
        // Mengecek input sentuhan untuk kontrol kamera
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            // Mengecek apakah sentuhan terjadi di sisi kanan layar
            if (touch.position.x > Screen.width / 2)
            {
                Vector2 deltaTouch = touch.deltaPosition;
                cameraRotationY += deltaTouch.x * cameraSensitivity;
                cameraRotationX -= deltaTouch.y * cameraSensitivity;
                cameraRotationX = Mathf.Clamp(cameraRotationX, -45f, 45f); // Batasi rotasi ke atas/bawah
            }
        }

        // Update posisi kamera berdasarkan target dan offset
        Vector3 desiredPosition = cameraTarget.position + Quaternion.Euler(cameraRotationX, cameraRotationY, 0f) * offsetCameraPosition;
        
        // Hitung jarak antara kamera dan target
        float distanceToTarget = Vector3.Distance(cameraTarget.position, desiredPosition);
        if (distanceToTarget > maxCameraDistance)
        {
            // Batasi jarak maksimum kamera
            desiredPosition = cameraTarget.position + (desiredPosition - cameraTarget.position).normalized * maxCameraDistance;
        }

        playerCamera.transform.position = desiredPosition;
        
        // Pastikan kamera menghadap target
        playerCamera.transform.LookAt(cameraTarget.position);
    }
}*/
/*
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public Animator animator;
    public CharacterController characterController;

    public AudioSource audioSource;        // Audio Source untuk memutar suara
    public AudioClip walkingClip;          // Suara saat berjalan
    public AudioClip runningClip;          // Suara saat berlari
    public AudioClip jumpingClip;          // Suara saat melompat

    public Joystick joystick;              // Referensi untuk joystick
    public Button runButton;               // Referensi untuk tombol lari
    public Button jumpButton;              // Referensi untuk tombol lompat
    public bool isGrounded;
    bool isMoving;
    bool isJumping;
    bool isRunning;

    Vector3 velocity = Vector3.zero;

    float speed = 0f;
    float speedWalking = 2f;
    float speedRunning = 4f;

    void Start()
    {
        // Menghubungkan fungsi ke tombol lari dan lompat
        runButton.onClick.AddListener(ToggleRun);
        jumpButton.onClick.AddListener(Jump);
    }

    void Update()
    {
        isGrounded = checkGrounded();
        animator.SetBool("isGrounded", isGrounded);

        // Mengatur kecepatan berdasarkan status lari
        speed = isRunning ? speedRunning : speedWalking;

        // Mengambil input dari joystick untuk arah gerakan
        float inputX = joystick.Horizontal;
        float inputY = joystick.Vertical;

        if (Mathf.Abs(inputX) > 0.1f || Mathf.Abs(inputY) > 0.1f)
        {
            float sudutInput = Mathf.Atan2(inputX, inputY) * Mathf.Rad2Deg;
            float sudutCamera = Camera.main.transform.eulerAngles.y;
            float sudutTarget = sudutInput + sudutCamera;

            transform.rotation = Quaternion.Euler(0f, sudutTarget, 0f);
            if (isGrounded && !isJumping)
            {
                float y = velocity.y;
                velocity = transform.rotation * Vector3.forward * speed;
                velocity.y = y;
                animator.SetBool("isMoving", true);

                // Memutar suara berjalan atau berlari
                if (!audioSource.isPlaying)
                {
                    audioSource.clip = isRunning ? runningClip : walkingClip;
                    audioSource.Play();
                }
            }
        }
        else
        {
            velocity.x = 0;
            velocity.z = 0;
            animator.SetBool("isMoving", false);
            audioSource.Stop(); // Berhenti memutar audio ketika karakter berhenti
        }

        // Gravitasi
        if (!isGrounded)
        {
            velocity.y -= 30 * Time.deltaTime;
            animator.SetBool("isMoving", false);
        }
        else if (velocity.y < 0)
        {
            velocity.y = -10f;
        }

        // Gerakkan player berdasarkan velocity
        characterController.Move(velocity * Time.deltaTime);
    }

    void ToggleRun()
    {
        // Mengaktifkan atau menonaktifkan lari
        isRunning = !isRunning;
        animator.SetBool("isRunning", isRunning);
    }

    void Jump()
    {
        if (isGrounded && !isJumping)
        {
            animator.SetBool("isJumping", true);
            animator.SetBool("isMoving", false);
            isJumping = true;

            // Memutar suara lompat
            audioSource.clip = jumpingClip;
            audioSource.Play();

            StartCoroutine(clearJumping());
        }
    }

    IEnumerator clearJumping()
    {
        yield return new WaitForSeconds(0.1f);
        animator.SetBool("isJumping", false);
        isJumping = false;
        velocity.y = 5;
    }

    bool checkGrounded()
    {
        RaycastHit hit;
        int layerMask = 1 << 9;
        layerMask = ~layerMask;
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), out hit, 0.4899999f, layerMask))
        {
            return true;
        }
        return false;
    }
}
*/