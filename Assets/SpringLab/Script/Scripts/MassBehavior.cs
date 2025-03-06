using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;
using UnityEngine.UIElements;
using System;
using System.Security.Cryptography;
using UnityEditor;

public class MassBehavior : MonoBehaviour
{
    // naming the this open field to attach a text UI field to this script component
    [Header("Component")]
    public TextMeshProUGUI massText; //variable

    // variable for the rigidbody
    [Header("RigidBody")]
    public Rigidbody rb;

    // variables for the object mass and drag
    [Header("Physics")]
    public float objectMass;
    public float objectDrag;

    // format settings to be changed
    [Header("Format Settings")]
    public bool hasFormat;
    public MassFormats format;
    
    // Using the enum definistions we wrote
    private Dictionary<MassFormats, string> massFormats = new Dictionary<MassFormats, string>();

    // boolean variables
    bool upArrowPress = false;
    bool downArrowPress = false;

    // Start is called before the first frame update
    void Start()
    {
        massFormats.Add(MassFormats.Whole, "0kg"); // if selected display as single digit with no decimal
        massFormats.Add(MassFormats.TenthDecimal, "0.0kg"); // if selected display as with one decimal place
        massFormats.Add(MassFormats.HundrethsDecimal, "0.00kg"); // if selected display hundereth decimal place
        massFormats.Add(MassFormats.ThousandsDecimal, "0.000kg");// display the thousand decimal place

        rb.mass = objectMass; //start of the object mass
        rb.drag = objectDrag; //start of the object drag

        //at start of the running program the display starts with this
        massText.text = hasFormat ? objectMass.ToString(massFormats[format]) : massFormats.ToString() + "kg";
    }

    // Update is called once per frame
    void Update()
    {
        //if one of the arrows were pressed the dispay of the mass will change
        if (upArrowPress == true || downArrowPress == true)
        {
            //massText.text = objectMass.ToString(objectMass + "kg");
            massText.text = hasFormat ? objectMass.ToString(massFormats[format]) : massFormats.ToString() + "kg";
        }
        
    }

    /*
     * in this method it is checking if the button was pressed on
     * OnClick() function in one of the button components
     * once it detects the up arrow was pressed, then the mass and
     * drag are incremented
     */
    public void upArrow()
    {
        
        upArrowPress = true;

        // Switch function for if it has format and the things to do for that format
        switch (hasFormat)
        {
            case true:
                {

                    switch (format)
                    {
                        case MassFormats.Whole:

                            {
                                objectMass = objectMass + 1f;
                                objectDrag = objectDrag + 1f;
                                break;
                            }
                            
                        case MassFormats.TenthDecimal:

                            {
                                objectMass = objectMass + 0.1f;
                                objectDrag = objectDrag + 0.1f;
                                break;
                            }

                         case MassFormats.HundrethsDecimal:

                            {
                                objectMass = objectMass + 0.01f;
                                objectDrag = objectDrag + 0.01f;
                                break;
                            }
                         case MassFormats.ThousandsDecimal:

                            {
                                objectMass = objectMass + 0.001f;
                                objectDrag = objectDrag + 0.001f;
                                break;
                            }

                            default: 
                            {
                               break;
                            }
                    }

                    break;
                }
                
        };
        
        rb.mass = objectMass; // GameObject mass is reflective of the variable
        rb.drag = objectDrag; // GameObject drag is reflective of the variable

    }

    /*
     * in this method if the down arrow is pressed it will reduce the mass depending on the format
     */
    public void downArrow()
    {
        downArrowPress = true;

        // old fomrat that is without any format
        /*if (objectMass > 0.1f)
        {
            objectMass = objectMass - 0.1f;

            objectDrag = objectDrag - 0.1f;

            rb.mass = objectMass;
            rb.drag = objectDrag;
        }

        if (objectMass == 0.1)
        {
            downArrowPress = false;

            objectMass = 0.1f;

            objectDrag = 0.1f;

            rb.mass = objectMass;
            rb.drag = objectDrag;
        }*/

        //switch function that checks on the format
        switch (hasFormat)
        {
            case true:
                {
                    switch (format)
                    {
                        case MassFormats.Whole:

                            {

                                if (objectMass > 1)
                                {
                                    objectMass = objectMass - 1;

                                    objectDrag = objectDrag - 1;

                                    rb.mass = objectMass;
                                    rb.drag = objectDrag;
                                }

                                if (objectMass == 1)
                                {
                                    downArrowPress = false;

                                    objectMass = 1;

                                    objectDrag = 1;

                                    rb.mass = objectMass;
                                    rb.drag = objectDrag;
                                }

                                break;
                            }

                        case MassFormats.TenthDecimal:

                            {
                                if (objectMass > 0.1)
                                {
                                    objectMass = objectMass - 0.1f;

                                    objectDrag = objectDrag - 0.1f;

                                    rb.mass = objectMass;
                                    rb.drag = objectDrag;
                                }

                                if (objectMass == 0.1)
                                {
                                    downArrowPress = false;

                                    objectMass = 0.1f;

                                    objectDrag = 0.1f;

                                    rb.mass = objectMass;
                                    rb.drag = objectDrag;
                                }

                                break;
                            }

                        case MassFormats.HundrethsDecimal:

                            {
                                if (objectMass > 0.01)
                                {
                                    objectMass = objectMass - 0.01f;

                                    objectDrag = objectDrag - 0.01f;

                                    rb.mass = objectMass;
                                    rb.drag = objectDrag;
                                }

                                if (objectMass == 0.01)
                                {
                                    downArrowPress = false;

                                    objectMass = 0.01f;

                                    objectDrag = 0.01f;

                                    rb.mass = objectMass;
                                    rb.drag = objectDrag;
                                }

                                break;
                            }
                        case MassFormats.ThousandsDecimal:

                            {
                                if (objectMass > 0.001)
                                {
                                    objectMass = objectMass - 0.001f;

                                    objectDrag = objectDrag - 0.001f;

                                    rb.mass = objectMass;
                                    rb.drag = objectDrag;
                                }

                                if (objectMass == 0.001)
                                {
                                    downArrowPress = false;

                                    objectMass = 0.001f;

                                    objectDrag = 0.001f;

                                    rb.mass = objectMass;
                                    rb.drag = objectDrag;
                                }

                                break;
                            }
                    }

                    break;
                }
        }
    }
}

// public enum definition
public enum MassFormats
{
    Whole, TenthDecimal, HundrethsDecimal, ThousandsDecimal
}
