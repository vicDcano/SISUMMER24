using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Solution_ChangingAngles : MonoBehaviour
{
    //Display text to the UI Screen
    [Header("Component")]
    public TextMeshProUGUI angleText; //variable

    //variable for the rigidbody
    [Header("RigidBody")]
    public Rigidbody rb;

    //variables for the object rotation
    [Header("Rotation")]
     public float degrees;

    //manipulating and publicly setting positon
    [Header("Postion")]
    public float[] p1;
    public float[] p2;
    public float[] p3;

    //boolean variables for buttons
    bool upArrowPress = false;
    bool downArrowPress = false;

    //angle rotation variable and set
    float angleType1 = 30f;
    float angleType2 = 45f;
    float angleType3 = 60f;

    // set current type angle at 1
    private int currentType = 1;


    // Start is called before the first frame update
    void Start()
    {
        // Get rotation of GameObject of the z corrdinate
        degrees = rb.rotation.eulerAngles.z;

        // Display rotation into UI text
        angleText.text = degrees.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        setAngleRamp();

    }

    // transforming the GameObject rotation and position
    private void setAngleRamp()
    {
        switch (currentType)
        {

            // Switch function that goes through the current type of 1 to 3 and loops back to 1 after 3
            case 1:

                // postion transformation
                rb.transform.position = new Vector3(p1[0], p1[1], p1[2]);
                // Hardcoding tester
                /*rb.transform.position = new Vector3(-18.9220009f, 0.921999991f, 5.21000004f);*/

                // roation transoformation
                rb.transform.rotation = Quaternion.Euler(0f, 0f, angleType1);

                // display current angle rotation
                angleText.text = rb.rotation.eulerAngles.z.ToString();
                
                break;

            case 2:

                rb.transform.position = new Vector3(p2[0], p2[1], p2[2]);
                //rb.transform.position = new Vector3(-18.6970005f, 0.731000006f, 5.21000004f);
                rb.transform.rotation = Quaternion.Euler(0f, 0f, angleType2);
                angleText.text = rb.rotation.eulerAngles.z.ToString();

                break;

            case 3:

                rb.transform.position = new Vector3(p3[0], p3[1], p3[2]);
                //rb.transform.position = new Vector3(-18.5139999f, 0.529999971f, 5.21000004f);
                rb.transform.rotation = Quaternion.Euler(0f, 0f, angleType3);
                angleText.text = rb.rotation.eulerAngles.z.ToString();

                break;

            default:
                break;
        }

        //DEBUGGER
        // Switch to the next type on a key press (you can use any key you like)
        if (Input.GetKeyDown(KeyCode.Space))
        {
            currentType = (currentType % 3) + 1;
        }
    }

    // Changes position based on button press
    public void UpPress()
    {
        //button press temporarily true
        upArrowPress = true;

        //current type changes this will help loops back to 1 after 3
        currentType = (currentType % 3) + 1;

        //call module
        setAngleRamp();

    }

    /*public void DownPress()
    {
        downArrowPress = true;

        currentType = (currentType % 3) - 1;

        setAngleRamp();
    }*/
}
