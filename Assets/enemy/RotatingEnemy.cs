using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingEnemy : MonoBehaviour
{
    private float rotZ;
    public float rotationSpeed;
    public bool clockWise;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!clockWise)
        {
            rotZ += Time.deltaTime * rotationSpeed;
        }
        else
        {
            rotZ += -Time.deltaTime * rotationSpeed;

        }

        transform.rotation = Quaternion.Euler(0, 0, rotZ);
    }
}
