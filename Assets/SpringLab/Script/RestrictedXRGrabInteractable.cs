using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class RestrictedXRGrabInteractable : XRGrabInteractable
{
    private Vector3 initialBoxPosition; // The initial position of the box when grabbed
    private Vector3 initialGrabOffset; // The initial offset between the grab point and the box's position
    private Transform interactorTransform; // The transform of the interactor (e.g., the ray interactor)

    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        base.OnSelectEntered(args);

        // Store the initial position of the box when grabbed
        initialBoxPosition = transform.position;

        // Calculate the initial grab offset in the box's local space
        initialGrabOffset = transform.InverseTransformPoint(args.interactorObject.transform.position);

        // Store the interactor's transform (the ray interactor)
        interactorTransform = args.interactorObject.transform;

        // Disable rotation tracking while grabbed
        trackRotation = false;

        Debug.Log("Box grabbed. Initial position: " + initialBoxPosition);
    }

    protected override void OnSelectExited(SelectExitEventArgs args)
    {
        base.OnSelectExited(args);

        // Clear the interactor's transform when released
        interactorTransform = null;

        // Re-enable rotation tracking (optional, if needed for other interactions)
        trackRotation = true;

        Debug.Log("Box released.");
    }

    private void Update()
    {
        if (isSelected && interactorTransform != null)
        {
            // Calculate the interactor's position in the box's local space
            Vector3 localInteractorPosition = transform.InverseTransformPoint(interactorTransform.position);

            // Calculate the target X position in the box's local space
            float targetLocalX = localInteractorPosition.x - initialGrabOffset.x;

            // Restrict movement to the box's local X-axis only
            Vector3 targetLocalPosition = new Vector3(targetLocalX, 0, 0);

            // Convert the target local position back to world space
            Vector3 targetWorldPosition = transform.TransformPoint(targetLocalPosition);

            // Lock the Y and Z positions to the initial position
            targetWorldPosition.y = initialBoxPosition.y;
            targetWorldPosition.z = initialBoxPosition.z;

            // Move the box to the target position
            transform.position = targetWorldPosition;

            // Lock rotation to prevent any rotation
            transform.rotation = Quaternion.identity; // Or use a fixed rotation if needed
        }
    }
}
