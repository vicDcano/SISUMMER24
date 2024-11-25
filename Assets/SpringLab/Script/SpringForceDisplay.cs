using TMPro;
using UnityEngine;
using UnityEngine.UI; // Use TMPro if using TextMeshPro

public class SpringForceDisplay : MonoBehaviour
{
    public Canvas forceCanvas;
    public TextMeshProUGUI forceText; // Assign this in the Inspector
    public Transform springObject; // Reference to your spring object
    public Transform boxObject; // Reference to your box object
    public float springConstant = 10f; // Example spring constant

    void Update()
    {
        float displacement = Vector3.Distance(springObject.position, boxObject.position);
        float force = springConstant * displacement;
        forceText.text = $"Force: {force:F2} N"; // Display force with 2 decimal places
    }

    void OnGrabStart()
    {
        forceCanvas.gameObject.SetActive(true);
    }

    void OnGrabEnd()
    {
        forceCanvas.gameObject.SetActive(false);
    }
}
