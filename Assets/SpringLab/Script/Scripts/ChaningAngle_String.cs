using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;
using TMPro;

public class ChaningAngle_String : MonoBehaviour
{
    // Get display text to the UI Screen
    [Header("Component")]
    public TextMeshProUGUI angleText; //variable

    // variable for the rigidbody
    [Header("RigidBody")]
    public Rigidbody rb;
    public Rigidbody rb2;

    // variables for degrees
    float degrees;


    // Start is called before the first frame update
    void Start()
    {
        // get the current GameObject Rotation at the start
        degrees = rb.rotation.eulerAngles.x;

        // Display the current GameObject rotation
        angleText.text = degrees.ToString("0.0000");
    }

    // Update is called once per frame
    void Update()
    {
        // Updating every frame of the current GameObject rotation and set it to the UI text in real time
        readAngleDegree();
        
    }

    // Get, set and display GameObject rotation
    private void readAngleDegree()
    {
        // get GameObject rotation in the x corrdinate
        degrees = rb.rotation.eulerAngles.x;

        if (degrees > 180f)
        {
            degrees = 360f - degrees;
        }

        // set the rotation to the UI text and display it
        angleText.text = degrees.ToString("0.0000");

    }
}
