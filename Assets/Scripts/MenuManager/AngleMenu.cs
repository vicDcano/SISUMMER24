using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AngleMenu : MonoBehaviour
{
    public void OnClick_Back()
    {
        MenuManager.OpenMenu(PendulumChangeMenu.choice_menu, gameObject);
    }

    public void OnClick_Properties()
    {
        MenuManager.OpenMenu(PendulumChangeMenu.properties_menu, gameObject);
    }
}
