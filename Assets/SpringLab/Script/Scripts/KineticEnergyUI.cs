using UnityEngine;
using UnityEngine.UI;

public class KineticEnergyUI : MonoBehaviour
{
    public Text energyText;
    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        // Calculate and update kinetic energy text.
        float kineticEnergy = 0.5f * rb.mass * rb.velocity.magnitude * rb.velocity.magnitude;
        energyText.text = "Kinetic Energy: " + kineticEnergy.ToString("F2") + " J";
    }
}
