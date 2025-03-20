using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxLengthRestriction : MonoBehaviour
{
    // Reference to the platform (assign in the Inspector)
    public Transform platform;

    // Platform boundaries (calculated based on platform's scale and position)
    private float platformMinX;
    private float platformMaxX;

    // Movement range (how far the box can move from the center of the platform)
    public float movementRange = 0.05f;   // Adjust this to your desired range

    private void Start()
    {
        // Calculate the platform's edges in world space
        platformMinX = platform.position.x - (platform.localScale.x / 2);
        platformMaxX = platform.position.x + (platform.localScale.x / 2);

        // Ensure the box starts within the allowed range
        Vector3 initialPosition = transform.position;
        initialPosition.x = Mathf.Clamp(initialPosition.x, platformMinX + movementRange, platformMaxX - movementRange);
        transform.position = initialPosition;
    }

    private void Update()
    {
        // Get the current position of the box
        Vector3 currentPosition = transform.position;

        // Calculate the allowed movement range within the platform
        float allowedMinX = platformMinX + movementRange;
        float allowedMaxX = platformMaxX - movementRange;

        // Clamp the box's X position to stay within the allowed range
        currentPosition.x = Mathf.Clamp(currentPosition.x, allowedMinX, allowedMaxX);

        // Update the box's position (only modify the X position)
        transform.position = new Vector3(currentPosition.x, transform.position.y, transform.position.z);
    }
}
