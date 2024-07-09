using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowDownSwing : MonoBehaviour
{

    [Header("Original Object")]
    public Transform originGameObject;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float MaxAngleDeflection = 30.0f;
        float SpeedOfPendulum = 1.0f;

        float angle = MaxAngleDeflection * Mathf.Sin(Time.time * SpeedOfPendulum);
        originGameObject.localRotation = Quaternion.Euler(0, 0, angle);
    }
}
