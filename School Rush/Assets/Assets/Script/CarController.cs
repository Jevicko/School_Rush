/*using UnityEngine;

public class CarController : MonoBehaviour
{
    public float speed = 10f;             // Kecepatan mobil
    public float turnSpeed = 50f;         // Kecepatan berbelok mobil
    public float brakeForce = 100f;       // Gaya pengereman
    private float moveInput;              // Input untuk gerakan maju atau mundur
    private float turnInput;              // Input untuk belokan

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // Dapatkan input untuk bergerak maju atau mundur
        moveInput = Input.GetAxis("Vertical") * speed;

        // Dapatkan input untuk membelok
        turnInput = Input.GetAxis("Horizontal") * turnSpeed;

        // Gerakkan mobil maju atau mundur
        rb.MovePosition(transform.position + transform.forward * moveInput * Time.deltaTime);

        // Putar mobil saat bergerak
        transform.Rotate(Vector3.up, turnInput * Time.deltaTime);
    }

    public void ApplyBrake()
    {
        rb.velocity = Vector3.Lerp(rb.velocity, Vector3.zero, brakeForce * Time.deltaTime);
    }
}*/

using UnityEngine;

public class CarController : MonoBehaviour
{
    public float speed = 15f;             // Kecepatan mobil
    public float turnSpeed = 1000f;         // Kecepatan berbelok mobil
    public float brakeForce = 10f;       // Gaya pengereman
    private float moveInput;              // Input untuk gerakan maju atau mundur
    private float turnInput;              // Input untuk belokan

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // Dapatkan input untuk bergerak maju atau mundur
        moveInput = Input.GetAxis("Vertical") * speed;

        // Dapatkan input untuk membelok
        turnInput = Input.GetAxis("Horizontal") * turnSpeed;

        // Gerakkan mobil maju atau mundur
        rb.MovePosition(transform.position + transform.forward * moveInput * Time.deltaTime);

        // Buat rotasi yang mulus
        if (moveInput != 0)  // Hanya rotasi ketika mobil bergerak
        {
            Quaternion targetRotation = Quaternion.Euler(0, transform.eulerAngles.y + turnInput * Time.deltaTime, 0);
            rb.MoveRotation(Quaternion.Slerp(rb.rotation, targetRotation, 0.1f)); // Lerp untuk rotasi mulus
        }
    }

    public void ApplyBrake()
    {
        rb.velocity = Vector3.Lerp(rb.velocity, Vector3.zero, brakeForce * Time.deltaTime);
    }
}
