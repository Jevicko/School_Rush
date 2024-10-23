/*using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Rendering;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Animator animator;
    public CharacterController characterController;

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
        animator.SetBool("isGrounded",isGrounded);

        //cek input lari / shift sebelah kiri

        isRunning = Input.GetAxis("Lari") > 0f;
       // isRunning = input.GetKeyDown(KeyCode.LeftShift);
        animator.SetBool("isRunning", isRunning);
        speed = isRunning ? speedRunning : speedWalking;

        //cek input WASD atau panah

        float inputX = Input.GetAxis("Horizontal");
        float inputY = Input.GetAxis("Vertical");
        if(Mathf.Abs(inputX)>0.1f || Mathf.Abs(inputY) > 0.1f)
        {
            float sudutInput = Mathf.Atan2(inputX, inputY) * 180f / 3.14f;
            float sudutCamera = Camera.main.transform.eulerAngles.y;
            float sudutTarget = sudutInput + sudutCamera;

            transform.rotation = Quaternion.Euler(0f,sudutTarget, 0f);
            if(isGrounded && !isJumping)
            {
                float y = velocity.y;
                velocity = transform.rotation * Vector3.forward * speed;
                velocity.y = y;
                animator.SetBool("isMoving", true);
            }
        }
        else
        {
            velocity.x = 0;
            velocity.z = 0;
            animator.SetBool("isMoving", false);
        }

        // cek pergerakan lompat

        if(Input.GetButtonDown("Jump") && isGrounded)
        {
            animator.SetBool("isJumping", true);
            animator.SetBool("isMoving", false);

            isJumping = true;

            //set isJumping false setelah delay 0.1 detik
            StartCoroutine(clearJumping());

        }

        // gravitasi

        if (!isGrounded)
        {
            //asumsi gravity = 10 m/s
            velocity.y -= 30 * Time.deltaTime;
            animator.SetBool("isMoving", false);

        }
        else if(velocity.y<0)
        {
            //supaya kaki tetep napak tanah
            velocity.y = -10f;
        }

        // gerakin player berdasarkan velocity

        characterController.Move(velocity * Time.deltaTime);

    }

    IEnumerator clearJumping()
    {
        yield return new WaitForSeconds(0.1f);
        animator.SetBool("isJumping", false);
        isJumping=false;
        velocity.y = 5;
    }

    bool checkGrounded() {
        RaycastHit hit;
        int layerMask = 1 << 9;
        layerMask = ~layerMask;
        if(Physics.Raycast(transform.position,transform.TransformDirection(Vector3.down),out hit, 0.4899999f, layerMask))
        {
            return true;
        }
        return false;
    }
} */

using System.Collections;
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
