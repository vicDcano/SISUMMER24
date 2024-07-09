using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MenuManager
{
    public static bool isIntialised { get; private set; }
    public static GameObject optionMenu, propertiesMenu, angleMenu;


    public static void init()
    {
        GameObject canvas = GameObject.Find("Canvas");
        optionMenu = canvas.transform.Find("ChoiceMenu").gameObject;
        propertiesMenu = canvas.transform.Find("PropertiesMenu").gameObject;
        angleMenu = canvas.transform.Find("AngleMenu").gameObject;

        isIntialised = true;
    }

    public static void OpenMenu(PendulumChangeMenu menu, GameObject callingMenu)
    {
        if(!isIntialised)
            init();

        switch(menu) 
        {
            case PendulumChangeMenu.choice_menu:
                optionMenu.SetActive(true);
                break;
            case PendulumChangeMenu.properties_menu: 
                propertiesMenu.SetActive(true);
                break;
            case PendulumChangeMenu.angle_display:
                angleMenu.SetActive(true);
                break;
        }

        callingMenu.SetActive(false);
    }
}
