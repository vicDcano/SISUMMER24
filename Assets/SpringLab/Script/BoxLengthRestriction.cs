using UnityEngine;

public class BoxLengthRestriction : MonoBehaviour
{
    // Reference to the platform (assign in the Inspector)
    public Transform platform;

    // How far from center the box can move (as percentage of platform width)
    [Range(0f, 0.5f)]
    public float movementRangePercent = 0.25f;

    // Cached platform boundaries
    private float platformHalfWidth;
    private float platformCenterX;
    private float allowedMinX;
    private float allowedMaxX;

    private void Start()
    {
        if (platform == null)
        {
            Debug.LogError("Platform reference not set!");
            return;
        }

        // Calculate platform boundaries
        platformHalfWidth = platform.localScale.x / 2f;
        platformCenterX = platform.position.x;

        // Calculate allowed movement area (staying in the middle section)
        allowedMinX = platformCenterX - (platformHalfWidth * movementRangePercent);
        allowedMaxX = platformCenterX + (platformHalfWidth * movementRangePercent);
    }

    private void Update()
    {
        if (platform == null) return;

        // Get current position
        Vector3 currentPosition = transform.position;

        // Clamp X position to stay within allowed range
        currentPosition.x = Mathf.Clamp(currentPosition.x, allowedMinX, allowedMaxX);

        // Apply only the X restriction
        transform.position = new Vector3(
            currentPosition.x,
            transform.position.y,
            transform.position.z
        );
    }

    // Optional: Visualize the allowed area in the editor
    private void OnDrawGizmosSelected()
    {
        if (platform == null) return;

        float halfWidth = platform.localScale.x / 2f;
        float centerX = platform.position.x;
        float range = halfWidth * (Application.isPlaying ? movementRangePercent : 0.25f);

        Vector3 minPos = new Vector3(centerX - range, platform.position.y, platform.position.z);
        Vector3 maxPos = new Vector3(centerX + range, platform.position.y, platform.position.z);

        Gizmos.color = Color.green;
        Gizmos.DrawLine(minPos, maxPos);
        Gizmos.DrawSphere(minPos, 0.1f);
        Gizmos.DrawSphere(maxPos, 0.1f);
    }
}