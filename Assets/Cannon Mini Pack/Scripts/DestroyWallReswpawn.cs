using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyWallReswpawn : MonoBehaviour
{
    public GameObject target;

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        Transform trb = target.transform;
        float x = trb.localPosition.x;

        Vector3 randomSpawnPos = new Vector3(x, Random.Range(1, 10), Random.Range(11, 45));
        Instantiate(target, randomSpawnPos, Quaternion.identity);
    }
}
