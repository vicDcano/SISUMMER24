using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class VRInteraction : MonoBehaviour
{
    public float pushForce = 10.0f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("HandController"))  // Check for the hand controller tag.
        {
            Rigidbody rb = GetComponent<Rigidbody>();
            if (rb != null)
            {
                Vector3 controllerVelocity = other.attachedRigidbody.velocity;
                rb.AddForce(controllerVelocity * pushForce, ForceMode.Impulse);
            }
        }
    }
}
