using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CannonAngle : MonoBehaviour
{
    // Get display text to the UI Screen
    [Header("Component")]
    public TextMeshProUGUI angleText; //variable

    // variable for the rigidbody
    [Header("Transform Body")]
    public Transform barrel; // transforming to hold the barrel of the cannon
    public float scrollIncrements = 10f; // how much the barrel tilt

    // variables for the object rotation
    [Header("Degrees")]
    float degrees;

    // boolean variables for buttons
    bool upArrowPress = false;
    bool downArrowPress = false;

    // Start is called before the first frame update
    void Start()
    {
        //massText.text = objectMass.ToString(objectMass + "kg");
        angleText.text = degrees.ToString("0") + "°";
    }

    // Update is called once per frame
    void Update()
    {
        //if one of the arrows were pressed the dispay of the mass will change
        if (upArrowPress == true || downArrowPress == true)
        {
            //massText.text = objectMass.ToString(objectMass + "kg");
            angleText.text = barrel.rotation.eulerAngles.x.ToString("0") + "°";
        }
    }

    // Changes position based on button press
    public void UpPress()
    {
        // button press temporarily true
        upArrowPress = true;

        int addedAngle = 0;

        int minAngle = 0;

        int maxAngle = -75;

        addedAngle = addedAngle - 5;

        int checkAngle = addedAngle;

        if (checkAngle < maxAngle)
        {
            addedAngle = minAngle;

            barrel.Rotate(addedAngle, 0, 0);

            angleText.text = barrel.rotation.x.ToString("0") + "°";
        }

        else
        {
            barrel.Rotate(addedAngle, 0, 0);
            angleText.text = barrel.rotation.eulerAngles.x.ToString("0") + "°";
        }

        /*angleText.text = checkAngle.ToString("0") + "°";*/

        /*while(addedAngle >= maxAngle) 
        {
            
        }*/
    }

    public void downPress()
    {
        // button press temporarily true
        downArrowPress = true;

        int addedAngle = 0;

        int minAngle = 0;

        int maxAngle = -75;

        while (addedAngle >= minAngle)
        {
            if (addedAngle > minAngle)
            {
                addedAngle = maxAngle;

                barrel.Rotate(addedAngle, 0, 0);
                break;
            }

            else
            {
                barrel.Rotate(addedAngle + 5, 0, 0);
                break;
            }

            angleText.text = addedAngle.ToString("0") + "°";
        }
    }


}
