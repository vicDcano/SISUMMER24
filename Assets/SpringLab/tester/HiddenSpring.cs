using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HiddenSpring : MonoBehaviour
{
    [Header("Spring Settings")]
    public Transform movingAnchor; // The anchor point that moves with this object
    public Transform fixedAnchor; // The anchor point that's fixed (e.g., on the wall)
    public float maxDisplacement = 0.05f; // 5 cm in meters (Unity units)
    public float movementSpeed = 0.5f;

    [Header("Debug")]
    public bool enableAutoOscillation = true; // For testing without input
    private Vector3 initialPosition;
    private float currentDisplacement = 0f;
    private bool movingUp = true;

    void Start()
    {
        if (movingAnchor == null)
        {
            Debug.LogError("Moving anchor not assigned!");
            enabled = false;
            return;
        }

        initialPosition = movingAnchor.position;
    }

    void Update()
    {
        // For testing without input - oscillates between +maxDisplacement and -maxDisplacement
        if (enableAutoOscillation)
        {
            AutoOscillate();
        }
        else
        {
            // You can replace this with your own control logic
            ManualControl();
        }
    }

    private void AutoOscillate()
    {
        // Calculate target displacement
        float targetDisplacement = movingUp ? maxDisplacement : -maxDisplacement;

        // Move towards target displacement
        currentDisplacement = Mathf.MoveTowards(currentDisplacement, targetDisplacement, movementSpeed * Time.deltaTime);

        // Check if we reached the target
        if (Mathf.Abs(currentDisplacement - targetDisplacement) < 0.001f)
        {
            movingUp = !movingUp;
        }

        // Apply the displacement
        Vector3 direction = (fixedAnchor.position - initialPosition).normalized;
        movingAnchor.position = initialPosition + direction * currentDisplacement;
    }

    private void ManualControl()
    {
        // Example manual control - you can replace this with your input system
        float input = Input.GetAxis("Vertical"); // Using keyboard up/down arrows for testing

        if (input != 0)
        {
            // Calculate new displacement
            currentDisplacement += input * movementSpeed * Time.deltaTime;
            currentDisplacement = Mathf.Clamp(currentDisplacement, -maxDisplacement, maxDisplacement);

            // Apply the displacement
            Vector3 direction = (fixedAnchor.position - initialPosition).normalized;
            movingAnchor.position = initialPosition + direction * currentDisplacement;
        }
    }

    // Public method to set displacement from another script
    public void SetDisplacement(float displacement)
    {
        displacement = Mathf.Clamp(displacement, -maxDisplacement, maxDisplacement);
        currentDisplacement = displacement;

        Vector3 direction = (fixedAnchor.position - initialPosition).normalized;
        movingAnchor.position = initialPosition + direction * currentDisplacement;
    }
}
