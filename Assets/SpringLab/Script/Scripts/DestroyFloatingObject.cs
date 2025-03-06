using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyFloatingObject : MonoBehaviour
{
    // GamObject variable that will be manipulated
    public GameObject GameObject;

    public float maxHeight = 3.0f; // max height variable
    public float velocity = 1.0f; // velocity variable speed is set

    float startHeight = 0; //variable where GameObject starts

    // Start is called before the first frame update
    void Start()
    {

        startHeight = transform.position.y; // from the start height the Gameobject sets at a y position
        maxHeight = maxHeight + startHeight; // from the startHeight the maxHeight is set from the distance of the startHeight
        velocity -= Random.Range(-0.5f, 0.5f); // Velocity from random is set
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 temp = transform.position; // sets temporarily variable copy of the position
        temp.y -= velocity * Time.deltaTime; // from temporarily subtracts velocity and multiplies deltaTime making it move

        // within start height and max height let the velocity change
        if(temp.y < startHeight || temp.y > maxHeight)
        {
            velocity *= -1;
        }

        transform.position = temp; // GameObject position gets temp position
    }

    // this method is to destroy a game object on Collision
    private void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject);
        Destroy(collision.gameObject);
    }
}
