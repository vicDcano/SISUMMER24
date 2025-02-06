using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class RestrictedXRGrabInteractable : XRGrabInteractable
{
    private Vector3 initialPosition;
    private Quaternion initialRotation;

    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        base.OnSelectEntered(args);

        // Store the initial position and rotation of the object when grabbed
        initialPosition = transform.position;
        initialRotation = transform.rotation;

        // Disable tracking of position and rotation
        trackPosition = false;
        trackRotation = false;
    }

    protected override void OnSelectExited(SelectExitEventArgs args)
    {
        base.OnSelectExited(args);

        // Re-enable tracking (optional, if needed for other interactions)
        trackPosition = true;
        trackRotation = true;
    }

    private void Update()
    {
        if (isSelected)
        {
            // Get the interactor's position in world space
            Vector3 interactorPosition = firstInteractorSelecting.transform.position;

            // Project the interactor's position onto the box's local X-axis
            Vector3 localOffset = transform.InverseTransformPoint(interactorPosition);
            localOffset.y = 0; // Lock Y-axis
            localOffset.z = 0; // Lock Z-axis

            // Convert the local offset back to world space
            Vector3 newPosition = transform.TransformPoint(new Vector3(localOffset.x, 0, 0));

            // Clamp the position to the initial X-axis
            newPosition.y = initialPosition.y;
            newPosition.z = initialPosition.z;

            // Apply the new position
            transform.position = newPosition;

            // Lock rotation to the initial rotation
            transform.rotation = initialRotation;
        }
    }
}