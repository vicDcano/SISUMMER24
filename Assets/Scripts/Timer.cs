using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Threading;

public class Timer : MonoBehaviour
{
    bool buttonTimer = false; // boolean variable

    bool resetOn = false; //boolean for resetting the timer

    // naming the this open field to attach a text UI field to this script component
    [Header("Component")]
    public TextMeshProUGUI timertext;

    // StopWatch to reverse to countdown
    [Header("Timer Settings")]
    public bool countUp;
    public float currentTime;

    // Variable and boolean to set the timer have a certain limit
    [Header("Limit Setting")]
    public bool hasLimit;
    public float timerLimit;

    // Using the enum definistions we wrote
    [Header("Format Settings")]
    public bool hasFormat;
    public TimerFormats format;
    private Dictionary<TimerFormats, string> timeFormats = new Dictionary<TimerFormats, string>();

    // Start is called before the first frame update
    void Start()
    {
        timeFormats.Add(TimerFormats.Whole, "Time: 0"); // Timer is set to be a whole digit of seconds
        timeFormats.Add(TimerFormats.TenthDecimal, "Time: 0.0"); // Timer is set to be a second and milisecond
        timeFormats.Add(TimerFormats.HundrethsDecimal, "Time: 0.00"); // Timer is set to be a second and milisecond
    }

    // Update is called once per frame
    void Update()
    {
        // once the button timer is equialent to being true then this statment is activated
        if (buttonTimer == true)
        {
            // gets currentTime is equialent to countUp null of currentTime is currentTime minus time.DeltaTime
            // this statment is to mimmick what a clock does
            currentTime = countUp ? currentTime -= Time.deltaTime : currentTime += Time.deltaTime;

            // if limit is checked then it will actiate here
            if (hasLimit && ((countUp && currentTime <= timerLimit || countUp && currentTime >= timerLimit)))
            {
                
                currentTime = timerLimit; // stops at the current time limit of the limit that is placed
                setTimerText(); // calls this module method
                timertext.color = Color.magenta; // once limit is reached then change it to this color
                enabled = false; // changes the enabled boolean to false
            }
        }

        setTimerText(); // calls this module method

        // old way to call and make the timer to appear
        /*timertext.text = currentTime.ToString();*/
    }

    /*
     * This method is to set up a timer to a string with the format that is placed and set to be
     */
    private void setTimerText()
    {
        /*timertext.text = currentTime.ToString();*/
        timertext.text = hasFormat ? currentTime.ToString(timeFormats[format]) : currentTime.ToString();
    }

    // this method is what starts the timer at any moment
    public void StartTimer()
    {
        buttonTimer = true;
    }

    // this method is to stop the timer at any moment if it is proven to be true
    public void StopTimer()
    {
        buttonTimer = false;
    }

    // this method resets the timer back to zero if the reset butoon is pressed
    public void resetAgain()
    {
        resetOn = true;
        currentTime = 0;
    }
}

// enum definitions
public enum TimerFormats
{
    Whole, TenthDecimal, HundrethsDecimal
}
