using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnIng : MonoBehaviour
{
    [SerializeField]
    private GameObject targetObject;

    // boolean variables
    bool spawnPress = false;

    public void Update()
    {
        if (spawnPress == true)
        {
            ButtonPress();
        }
        
    }

    public void ButtonPress()
    {
        spawnPress = true;

        Instantiate(targetObject, new Vector3(-1.83f, 0, 1.66f), Quaternion.identity);
        if (Input.GetKeyUp(KeyCode.Space))
        {
            //create or spawn that creates that is not currently exist
            Instantiate(targetObject, new Vector3(-1.83f, 0, 1.66f), Quaternion.identity);

            /**note that the Quaternion is a bit complex as it involves with rotation and hard to explain fully
             *all you need to know is Quaternion.identity keeps how we naturally have it*/


        }
        spawnPress = false;

    }

    
}
