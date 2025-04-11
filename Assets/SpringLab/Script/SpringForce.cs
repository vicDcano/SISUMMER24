using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpringForce: MonoBehaviour
{
    [Header("Anchor Points")]
    public Transform anchor1; // The anchor that moves (displacement control)
    public Transform anchor2; // The fixed anchor

    [Header("Spring Settings")]
    public float maxDisplacement = 0.05f; // 5cm in meters
    public float movementSpeed = 0.5f;
    public float returnSpeed = 1.0f; // Speed when returning to center

    private Vector3 initialPosition;
    private float currentDisplacement = 0f;
    private bool isAtLimit = false;

    void Start()
    {
        if (anchor1 == null || anchor2 == null)
        {
            Debug.LogError("Both anchors must be assigned!");
            enabled = false;
            return;
        }

        initialPosition = anchor1.position;
    }

    void Update()
    {
        UpdateSpringVisual();
    }

    public void SetDisplacement(float displacement)
    {
        // First, clamp the input displacement to our maximum allowed range
        float clampedDisplacement = Mathf.Clamp(displacement, -maxDisplacement, maxDisplacement);

        // If we're trying to move beyond current position while already at limit, ignore
        if (isAtLimit && Mathf.Abs(displacement) >= Mathf.Abs(currentDisplacement))
        {
            return;
        }

        // Calculate movement direction
        float direction = Mathf.Sign(clampedDisplacement - currentDisplacement);
        float speed = (direction == Mathf.Sign(-currentDisplacement)) ? returnSpeed : movementSpeed;

        // Calculate new displacement
        float newDisplacement = currentDisplacement + direction * speed * Time.deltaTime;

        // If we're moving toward the limit, clamp the final position
        if (Mathf.Sign(newDisplacement - currentDisplacement) == Mathf.Sign(clampedDisplacement - currentDisplacement))
        {
            newDisplacement = Mathf.Clamp(newDisplacement,
                                        -maxDisplacement,
                                        maxDisplacement);
        }

        // Update current displacement
        currentDisplacement = newDisplacement;
        isAtLimit = Mathf.Abs(currentDisplacement) >= maxDisplacement;

        // Apply the position
        UpdateAnchorPosition();
    }

    public void MoveToDisplacement(float targetDisplacement)
    {
        SetDisplacement(targetDisplacement);
    }

    private void UpdateAnchorPosition()
    {
        Vector3 newPos = initialPosition;
        newPos.x += currentDisplacement;
        anchor1.position = newPos;
    }

    private void UpdateSpringVisual()
    {
        if (anchor1 == null || anchor2 == null) return;

        Vector3 direction = anchor2.position - anchor1.position;
        float distance = direction.magnitude;

        if (distance > 0.01f)
        {
            // Position and rotate the spring
            transform.position = anchor1.position;
            transform.rotation = Quaternion.LookRotation(direction);

            // Stretch the spring visually (assuming Z-axis is spring length)
            transform.localScale = new Vector3(
                transform.localScale.x,
                transform.localScale.y,
                distance
            );
        }
    }

    public void ResetPosition()
    {
        currentDisplacement = 0f;
        isAtLimit = false;
        UpdateAnchorPosition();
    }

    public float GetCurrentDisplacement()
    {
        return currentDisplacement;
    }
}

/*public class SpringForce : MonoBehaviour
{*/
/*public Transform fixedPoint;       // Reference to the fixed end of the spring
public Rigidbody boxRb;            // Reference to the box's Rigidbody
public float springConstant = 10f; // The stiffness of the spring (k)
public float damping = 0.2f;         // Damping to reduce oscillation
public float restLength = 1f;      // Natural length of the spring
private Vector3 initialScale;

void Start()
{
    // Store the initial scale of the spring
    initialScale = transform.localScale;
}

*//*void Update()
{
    // Calculate the direction and distance from the fixed point to the box
    Vector3 direction = boxRb.position - fixedPoint.position;
    float distance = direction.magnitude;

    // Position the spring's center between the fixed point and the box
    transform.position = fixedPoint.position + direction * 0.5f;

    // Align the spring to face the box
    transform.rotation = Quaternion.LookRotation(direction);

    // Scale the spring along its Z-axis based on the distance to the box
    transform.localScale = new Vector3(initialScale.x, initialScale.y, distance / initialScale.z);
}*//*

void Update()
{
    // Calculate the direction and distance from the fixed point to the box
    Vector3 direction = boxRb.position - fixedPoint.position;
    float distance = direction.magnitude;

    // Keep the spring coil at the fixed point
    transform.position = fixedPoint.position;

    // Align the spring to face the box
    transform.rotation = Quaternion.LookRotation(direction);

    // Stretch the spring visually based on the distance to the box
    transform.localScale = new Vector3(initialScale.x, initialScale.y, distance / initialScale.z);
}


void FixedUpdate()
{
    // Only apply the spring force if not grabbed (handled in GrabLock script)
    if (boxRb.constraints != RigidbodyConstraints.FreezeAll)
    {
        // Calculate spring displacement only along the X-axis
        Vector3 displacement = new Vector3(boxRb.position.x - fixedPoint.position.x, 0, 0);
        float stretch = displacement.magnitude - restLength;

        // Apply Hooke's Law (spring force: F = -kx) along the X-axis
        Vector3 springForce = -springConstant * stretch * displacement.normalized;

        // Apply damping force only along the X-axis
        Vector3 dampingForce = new Vector3(-damping * boxRb.velocity.x, 0, 0);

        // Apply total force to the box
        boxRb.AddForce(springForce + dampingForce);
    }
}*/

/*public Transform fixedPoint;       // Reference to the fixed end of the spring
public Rigidbody boxRb;

public Transform anchor1;          // Position near the fixed point
public Transform anchor2;          // Position near the box
public float springConstant = 10f; // The stiffness of the spring (k)
public float damping = 0.2f;       // Damping to reduce oscillation
public float restLength = 1f;      // Natural length of the spring
private Vector3 initialScale;

void Start()
{
    // Store the initial scale of the spring
    initialScale = transform.localScale;

    // Force the initial positions and alignment based on anchors
    Vector3 initialDirection = anchor2.position - anchor1.position;
    float initialDistance = initialDirection.magnitude;

    if (initialDistance > 0.01f)
    {
        // Position the spring between the anchors without modifying Rigidbody position
        transform.position = anchor1.position + initialDirection * 0.5f;

        // Align the spring with the anchors
        transform.rotation = Quaternion.LookRotation(initialDirection);

        // Set the scale based on the initial distance between anchors
        transform.localScale = new Vector3(initialScale.x, initialScale.y, initialDistance / initialScale.z);
    }
    else
    {
        Debug.LogWarning("Anchor1 and Anchor2 positions are too close at the start!");
    }
}

void Update()
{
    // Calculate direction and distance between anchor1 and anchor2
    Vector3 direction = anchor2.position - anchor1.position;
    float distance = direction.magnitude;

    // Prevent zero vector errors by checking for small distances
    if (distance > 0.01f)
    {
        // Update the scale based on the distance between anchors
        transform.localScale = new Vector3(initialScale.x, initialScale.y, distance / initialScale.z);
    }
    else
    {
        Debug.LogWarning("Anchor1 and Anchor2 positions are too close! Direction vector is zero.");
    }
}

void FixedUpdate()
{
    // Calculate the direction and distance between the fixed point and box
    Vector3 direction = boxRb.position - fixedPoint.position;
    float distance = direction.magnitude;

    // Calculate the force based on Hooke's law
    Vector3 springForce = direction.normalized * (distance - restLength) * springConstant;

    // Apply the spring force to the box (or the spring itself)
    boxRb.AddForce(springForce);

    // Optional: Add damping
    Vector3 dampingForce = boxRb.velocity * damping;
    boxRb.AddForce(-dampingForce);
}*/
/*

    public Transform anchor1;          // Fixed anchor position
    public Transform anchor2;          // Moving anchor position
    public float springConstant = 10f; // The stiffness of the spring (k)
    public float damping = 0.2f;       // Damping to reduce oscillation
    public float restLength = 1f;      // Natural length of the spring
    private Vector3 initialScale;

    void Start()
    {
        // Store the initial scale of the spring
        initialScale = transform.localScale;

        // Debug checks to ensure anchors are assigned
        if (anchor1 == null)
        {
            Debug.LogError("anchor1 is not assigned! Please assign it in the Inspector.");
        }
        else
        {
            Debug.Log("anchor1 assigned to: " + anchor1.gameObject.name);
        }

        if (anchor2 == null)
        {
            Debug.LogError("anchor2 is not assigned! Please assign it in the Inspector.");
        }
        else
        {
            Debug.Log("anchor2 assigned to: " + anchor2.gameObject.name);
        }
    }

    void Update()
    {
        UpdateSpringVisual();
    }

    private void UpdateSpringVisual()
    {
        // Calculate the direction and distance between anchor1 and anchor2
        Vector3 direction = anchor2.position - anchor1.position;
        float distance = direction.magnitude;

        // Prevent zero vector errors by adding a small threshold check
        if (distance > 0.01f) // Avoid zero or near-zero distance
        {
            // Position the spring to keep anchor1 and anchor2 fixed
            transform.position = anchor1.position;

            // Align the spring to face anchor2
            transform.rotation = Quaternion.LookRotation(direction);

            // Stretch the spring visually along its local Z-axis
            transform.localScale = new Vector3(
                initialScale.x,
                initialScale.y,
                distance / initialScale.z
            );
        }
    }

}*/