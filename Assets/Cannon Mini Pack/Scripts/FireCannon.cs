using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireCannon : MonoBehaviour
{
    //Setting up the cannon and the cannon ball activity
    public GameObject cannonball; // getting the cannon game object
    public float cannonballSpeed = 20f; // set the velocity of the ball
    public Transform pof; //transforming the empty game object we placed on the cannon
    public Transform barrel; // transforming to hold the barrel of the cannon
    public float scrollIncrements = 10f; // how much the barrel tilt

    public Vector3 initialVelocity;

    private bool fire_Cannon = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    public void Update()
    {
        /*//debug testing
        float rotateCannon = Input.GetAxis("Mouse X"); // gets the mouse x position to rotate the cannon
        transform.Rotate(0, rotateCannon, 0); // as said above using the rotate method to transform the whole cannon with x, y and z values

        //moving the cannon up and down
        barrel.Rotate(Input.mouseScrollDelta.y * scrollIncrements, 0, 0); // x, y, z

        //Firing the cannonball
        if (Input.GetButtonDown("Fire1"))
        {
            FireCannonball();
        }*/

    }

    public void FireCannonball()
    {
        fire_Cannon = true;

        // using instantiate method it will take three things, the object and rotation
        GameObject ball = Instantiate(cannonball, pof.position, Quaternion.identity);

        Rigidbody rb = ball.AddComponent<Rigidbody>(); // creates a rigidbody gor the cannonball
        rb.velocity = cannonballSpeed * pof.forward; // creates a velocity for the cannon ball of any values we want in it

        fire_Cannon = false;

        StartCoroutine(RemoveCannonball(ball));
    }

    IEnumerator RemoveCannonball(GameObject ball)
    {
        yield return new WaitForSeconds(5f);
        Destroy(ball);
    }
}
