using UnityEngine;

public class KartMechanics : MonoBehaviour
{
    [Header("Movement Settings")]
    public float acceleration = 500f; // Force applied to move the kart
    public float maxSpeed = 20f; // Maximum speed in units/second
    public float turnSpeed = 100f; // Turning speed in degrees/second
    public float brakeStrength = 2000f; // Brake force applied

    [Header("Wheel Transforms")]
    public Transform[] wheels; // Assign the wheel transforms in the inspector

    private Rigidbody rb;

    void Start()
    {
        // Get the Rigidbody attached to the same GameObject
        rb = GetComponent<Rigidbody>();
        
        // Ensure the Rigidbody is attached
        if (rb == null)
        {
            Debug.LogError("Rigidbody component not found on the Kart GameObject.");
        }
    }

    public void Drive(float throttleInput)
    {
        // Apply forward force based on throttle input
        if (rb.velocity.magnitude < maxSpeed)
        {
            rb.AddForce(transform.forward * throttleInput * acceleration * Time.deltaTime, ForceMode.Force);
        }
    }

    public void Turn(float steeringInput)
    {
        // Apply torque to rotate the kart
        float turnAmount = steeringInput * turnSpeed * Time.deltaTime;
        rb.MoveRotation(rb.rotation * Quaternion.Euler(0f, turnAmount, 0f));
    }

    public void Brake()
    {
        // Apply brake force to slow down
        rb.velocity = Vector3.Lerp(rb.velocity, Vector3.zero, brakeStrength * Time.deltaTime);
    }

    void Update()
    {
        // Rotate the wheels for visual effect
        foreach (var wheel in wheels)
        {
            if (wheel)
                wheel.Rotate(Vector3.right, rb.velocity.magnitude * 5 * Time.deltaTime);
        }
    }
}
