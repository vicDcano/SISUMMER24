using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class SolidBoundary : MonoBehaviour
{
    private void OnCollisionStay(Collision collision)
    {
        if (collision.rigidbody != null && !collision.rigidbody.isKinematic)
        {
            // Calculate pushback direction
            Vector3 pushDirection = collision.contacts[0].point - transform.position;
            pushDirection.y = 0; // Optional: Remove vertical component
            pushDirection.Normalize();

            // Apply forceful pushback
            collision.rigidbody.velocity = Vector3.zero;
            collision.rigidbody.AddForce(pushDirection * 50f, ForceMode.Impulse);
        }
    }
}