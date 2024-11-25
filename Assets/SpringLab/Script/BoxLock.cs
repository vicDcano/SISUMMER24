using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class BoxLock : MonoBehaviour
{
    private Rigidbody rb;
    public float initialXRotation = 270f;  // Store the initial rotation as a Quaternion
    private float intialYRotation;
    private float intialZRotation;
    private Vector3 initialPosition;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        //initialXRotation = transform.rotation.eulerAngles.x;
        intialYRotation = 0f;
        intialZRotation = 0f;

        // Store the initial position to lock movement to only the x-axis
        initialPosition = transform.position;
    }

    void Update()
    {
        // Constrain the box to move only on the X-axis in position
        Vector3 constrainedPosition = new Vector3(transform.position.x, initialPosition.y, initialPosition.z);
        transform.position = constrainedPosition;

        // Only apply the initial rotation's X component, with Y and Z rotations fixed at 0
        transform.rotation = Quaternion.Euler(initialXRotation, intialYRotation, intialZRotation);
    }
}
