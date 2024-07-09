using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.UI.Image;

public class DeSpawning : MonoBehaviour
{
    public GameObject targetObject;

    bool despawnPress = false;

    public void Update()
    {

        if (despawnPress == true)
        {
            ButtonPress();
        }
        
    }

    public void ButtonPress()
    {
        /*if (Input.GetKeyUp(KeyCode.D))
        {
            Destroy(targetObject);
        }*/
        Instantiate(targetObject, new Vector3(-1.83f, 0, 1.66f), Quaternion.identity);

    }

    
}
