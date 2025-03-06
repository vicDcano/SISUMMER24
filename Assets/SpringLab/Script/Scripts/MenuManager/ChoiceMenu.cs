using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChoiceMenu : MonoBehaviour
{
    public void OnClick_Properties()
    {
        MenuManager.OpenMenu(PendulumChangeMenu.properties_menu, gameObject);
    }

    public void OnClick_Angle() 
    {
        MenuManager.OpenMenu(PendulumChangeMenu.angle_display, gameObject);
    }
}
