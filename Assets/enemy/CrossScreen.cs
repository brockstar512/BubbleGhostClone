using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrossScreen : MonoBehaviour
{
    public float moveSpeed = 5;
    public bool rightToLeft;
    float endPoint
    {
        get
        {
            Vector3 end = Camera.main.ViewportToWorldPoint(new Vector3(-0.1f, 0.5f, 10.0f));
            return end.x;
        }
    }

    Vector3 spawnPos;
    Vector3 endPos;
    //todo make enemy do the inverse if we want it to go left to right
    //todo make is adapted to the active current position when ending vs the end position of the camera when it spawns
    void Awake()
    {
        Spawn();
    }
    void Spawn()
    {
        //        spawnPos = rightToLeft ? Camera.main.ViewportToWorldPoint(new Vector3(1.1f, 0.5f, 10.0f)): Camera.main.ViewportToWorldPoint(new Vector3(-0.1f, 0.5f, 10.0f));
        //        endPos = rightToLeft ? Camera.main.ViewportToWorldPoint(new Vector3(-0.1f, 0.5f, 10.0f)):Camera.main.ViewportToWorldPoint(new Vector3(1.1f, 0.5f, 10.0f));

        spawnPos = Camera.main.ViewportToWorldPoint(new Vector3(1.1f, 0.5f, 10.0f));
        endPos = Camera.main.ViewportToWorldPoint(new Vector3(-0.1f, 0.5f, 10.0f));
        transform.position = spawnPos;
    }
        
    private void FixedUpdate()
    {
        Vector2 pos = transform.position;

        pos.x -= moveSpeed * Time.fixedDeltaTime;



        transform.position = pos;

        //if (transform.position.x < endPos.x)
        if (transform.position.x < endPoint)

        {
            Spawn();
            Debug.Log("Done");
        }
    }

}
