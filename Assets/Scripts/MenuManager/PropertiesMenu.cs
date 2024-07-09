using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropertiesMenu : MonoBehaviour
{
    public void OnClick_Back()
    {
        MenuManager.OpenMenu(PendulumChangeMenu.choice_menu, gameObject);
    }

    public void OnClick_Angle()
    {
        MenuManager.OpenMenu(PendulumChangeMenu.angle_display, gameObject);
    }
}
