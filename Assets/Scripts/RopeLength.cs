using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RopeLength : MonoBehaviour
{
    // naming the this open field to attach a text UI field to this script component
    [Header("Component")]
    public TextMeshProUGUI ropeText; //variable

    public GameObject rope;

    public float ropeLen;
   
    // variable for the rigidbody
    [Header("RigidBody")]
    public Rigidbody rb;

    // format settings to be changed
    [Header("Format Settings")]
    public bool hasFormat;
    public LengthFormats format;

    // Using the enum definistions we wrote
    private Dictionary<LengthFormats, string> lengthFormats = new Dictionary<LengthFormats, string>();

    // boolean variables
    bool upArrowPress = false;
    bool downArrowPress = false;

    // Start is called before the first frame update
    void Start()
    {
        lengthFormats.Add(LengthFormats.TenthDecimal, "0.0m"); // if selected display as with one decimal place
        lengthFormats.Add(LengthFormats.HundrethsDecimal, "0.00m"); // if selected display hundereth decimal place
        lengthFormats.Add(LengthFormats.ThousandsDecimal, "0.000m");// display the thousand decimal place

        // Store in a temporary variable
        var scale = rope.transform.localScale;
        // Now in a variable (or field) you CAN change the field values

        // Assign back to the property
        rope.transform.localScale = scale;

        //at start of the running program the display starts with this
        ropeText.text = hasFormat ? scale.ToString(lengthFormats[format]) : lengthFormats.ToString() + "m";
    }

    // Update is called once per frame
    void Update()
    {
        // Store in a temporary variable
        var scale = rope.transform.localScale;
        // Now in a variable (or field) you CAN change the field values

        // Assign back to the property
        rope.transform.localScale = scale;

        //if one of the arrows were pressed the dispay of the mass will change
        if (upArrowPress == true || downArrowPress == true)
        {
            //massText.text = objectMass.ToString(objectMass + "kg");
            ropeText.text = hasFormat ? scale.ToString(lengthFormats[format]) : lengthFormats.ToString() + "kg";
        }
    }

    // public enum definition
    public enum LengthFormats
    {
        Whole, TenthDecimal, HundrethsDecimal, ThousandsDecimal
    }
}
