using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class RestrictedXRGrabInteractable : XRGrabInteractable
{
    private Vector3 initialPosition;
    private Quaternion initialRotation;
    private Transform interactorTransform;

    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        base.OnSelectEntered(args);

        // Store the initial position and rotation of the object when grabbed
        initialPosition = transform.position;
        initialRotation = transform.rotation;

        // Store the interactor's transform
        interactorTransform = args.interactorObject.transform;

        // Disable tracking of position and rotation
        trackPosition = false;
        trackRotation = false;

        Debug.Log("Box grabbed. Initial position: " + initialPosition);
    }

    protected override void OnSelectExited(SelectExitEventArgs args)
    {
        base.OnSelectExited(args);

        // Re-enable tracking (optional, if needed for other interactions)
        trackPosition = true;
        trackRotation = true;

        // Clear the interactor's transform
        interactorTransform = null;

        Debug.Log("Box released.");
    }

    private void Update()
    {
        if (isSelected && interactorTransform != null)
        {
            // Calculate the interactor's position relative to the box's initial position
            Vector3 interactorOffset = interactorTransform.position - initialPosition;

            // Project the interactor's offset onto the box's local X-axis
            Vector3 localXAxis = transform.right; // The box's local X-axis
            float movementAmount = Vector3.Dot(interactorOffset, localXAxis);

            // Increase movement sensitivity (adjust the multiplier as needed)
            movementAmount *= 2; // This makes the box move more responsively

            Debug.Log("Interactor offset: " + interactorOffset + ", Movement amount: " + movementAmount);

            // Move the box along its local X-axis using Transform.Translate
            transform.Translate(Vector3.right * movementAmount * Time.deltaTime, Space.Self);

            // Lock the Y and Z positions to the initial position
            Vector3 newPosition = transform.position;
            newPosition.y = initialPosition.y;
            newPosition.z = initialPosition.z;
            transform.position = newPosition;

            // Lock rotation to the initial rotation
            transform.rotation = initialRotation;
        }
    }
}