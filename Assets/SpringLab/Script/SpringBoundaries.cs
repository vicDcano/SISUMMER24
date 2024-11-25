using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpringBoundaries : MonoBehaviour
{
    public Transform springAnchor;  // Fixed end of the spring
    public Transform mass;          // Moving mass at the other end
    public int coilSegments = 20;   // Number of segments in the coil (higher for smoother spring)
    public float coilRadius = 0.1f; // Radius of each coil
    public float coilTurns = 5f;    // Number of turns in the coil


    void Start()
    {
    }

    void Update()
    {
        UpdateSpringVisualization();
    }

    void UpdateSpringVisualization()
    {
        // Calculate the direction and current distance between anchor and mass
        Vector3 direction = mass.position - springAnchor.position;
        float currentLength = direction.magnitude;
        direction.Normalize();

        // Determine the segment length for each coil segment
        float segmentLength = currentLength / coilSegments;

        // Loop through each coil segment to calculate positions
        for (int i = 0; i <= coilSegments; i++)
        {
            float progress = (float)i / coilSegments;  // Progress along the spring length
            Vector3 basePosition = springAnchor.position + direction * (segmentLength * i);

            // Create the coil offset using a sine and cosine pattern for a circular coil effect
            float coilAngle = progress * coilTurns * Mathf.PI * 2;
            float adjustedRadius = coilRadius * (1 - progress);  // Optional: taper coils toward the end
            Vector3 offset = new Vector3(Mathf.Cos(coilAngle), Mathf.Sin(coilAngle), 0) * adjustedRadius;

            // Rotate the offset to align along the direction of the spring
            Vector3 rotatedOffset = Quaternion.LookRotation(direction) * offset;

        }
    }
}
