using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bubble : MonoBehaviour
{
    //area of public radius
    public Vector3 start;
    public Vector3 end;
    public float r;//0.26


    //public Vector3 boundsArea = new Vector3();
    SpriteRenderer sr;
    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        Debug.Log(sr.bounds);
        Debug.Log(sr.bounds.center);
        //start = sr.bounds.center;
        //start = transform.position;

    }
    private void OnDrawGizmos()
    {
        //-1.75 - edge
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(start, end);
        //Gizmos.color = Color.yellow;
        //Gizmos.DrawSphere(start, r);
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log($"RECT{sr.bounds.size.x / 2}");
        start = transform.position;//always the centur
        end = new Vector2(this.transform.position.x+ sr.bounds.size.x / 2, this.transform.position.y);
    }
}



//enum on the game object
//base parent too
//parent has a funcition that calles other funtion based on enum passed in
//tell the button what to do

//action button can change postions and action depending on state