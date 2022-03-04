using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class MyJoystickExample : MonoBehaviour
{
    [Header("Character Dependencies")]
    public float moveSpeed = 7f;
    public VariableJoystick variableJoystick;
    public Rigidbody2D rb;
    Vector2 direction;
    public Vector2 GetDirection{ get { return direction; }}


    public Button _BButton;
    public Button _AButton;

    public float rotationSpeed = 270;

    bool isHoldingPosition;


    //testing purposes
    public GameObject bubble;

    public void Update()
    {
        //get the input
        direction = Vector2.up * variableJoystick.Vertical + Vector2.right * variableJoystick.Horizontal;
    }


    public void FixedUpdate()
    {
        //move the character
        moveCharacter(direction);

        //rotate the character in that direction
        rotateCharacter(direction);

        //Debug.Log($"direction: {this.transform.up} and our version of dir: {direction}");
        //direction: (1.0, 0.0, 0.0) and our version of dir: (0.0, 0.0)
        //direction is what direction our joystick is... if we're not moving/not pressing it, it's 0,0,0

        //SendRay();

    }

    void moveCharacter(Vector2 direction)
    {
        if (isHoldingPosition)
            return;

        //return; if you want it to stay in place ignore the following
        rb.MovePosition(rb.position + direction * moveSpeed * Time.deltaTime);
    }

    void rotateCharacter(Vector2 direction)
    {
        //if the character is moving we want the character to roate in that direction
        if (direction != Vector2.zero)
        {
            //sprite rotation https://www.youtube.com/watch?v=gs7y2b0xthU
            //i think this means make my forwrad this direction
            Quaternion toRotation = Quaternion.LookRotation(Vector3.forward, direction);//gets how much it needs to rotate
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);//rotates that object from x to end by this speed
        }
    }

    public void B_Button()
    {
        //Debug.Log("B for blow");
    }
    public void A_Button(Vector2 target)
    {
        //Debug.Log("A for action");
        transform.up = target; 
    }
    public void A_Button(bool tapped)
    {
        ///inPlace
        isHoldingPosition = !isHoldingPosition;
    }


    //void SendRay()
    //{
        
    //    RaycastHit2D hit = Physics2D.Raycast(this.gameObject.transform.position, this.transform.up);
    //    Debug.DrawRay(transform.position, this.transform.up, Color.green);

    //    //If something was hit.
    //    if (hit.collider != null)
    //    {
    //        //contact point: hit.point
    //        //radius: bubble.GetComponent<CircleCollider2D>().radius

    //        //Display the point in world space where the ray hit the collider's surface.
    //        //Debug.Log(hit.point);

    //        //invervse normal so the direction the bubble should go in
    //        //Vector3 diftDir = new Vector3(bubble.transform.position.x - hit.normal.x, bubble.transform.position.y - hit.normal.y, 0);
    //        //Debug.DrawLine(bubble.transform.position, diftDir, Color.blue);

    //        //local hit of the raycast
    //        //Vector3 localHit = transform.InverseTransformPoint(hit.point);

    //        //ghost to collision point
    //        //Debug.DrawLine(this.transform.position, hit.point, Color.red);

    //        //radius of bubble(center to hitpoint)
    //        //Debug.DrawLine(bubble.transform.position, hit.point, Color.black);

    //        //center of bubble to center of ghost
    //        //Debug.DrawLine(bubble.transform.position, this.transform.position, Color.green);

    //        //vSourceToDestination = vDestination - vSource;
    //        //Debug.Log($"Hit point vector length:  {lengthToHit}");

    //        //radius of circle
    //        //Debug.Log($"Radius::  {bubble.GetComponent<CircleCollider2D>().radius}");//.5
    //        float radius = bubble.GetComponent<CircleCollider2D>().radius;

    //        //the direction the bubble should travel...i think the direction is off on the inverse normal one
    //        Debug.DrawLine(hit.point, bubble.transform.position, Color.black);
    //        Vector3 dir = new Vector3(bubble.transform.position.x - hit.point.x, bubble.transform.position.y- hit.point.y, 0);
    //        Debug.DrawLine(hit.point, hit.point + GetDirection, Color.black);//the continuous direction of hit point

    //        //Debug.Log(dir);

    //        //----------------
    //        //proper tangent line 

    //        //bubble to ghost
    //        Vector3 mainHyp = new Vector3(this.gameObject.transform.position.x - bubble.transform.position.x, this.gameObject.transform.position.y - bubble.transform.position.y, 0);
    //        Debug.DrawLine(this.gameObject.transform.position, bubble.transform.position, Color.blue);


    //        ////
    //        ////----------------

    //        //kite
    //        //center of ghost to center of bubble
    //        Debug.DrawLine(this.gameObject.transform.position, bubble.transform.position, Color.black);
    //        //perpendicular line
    //        Vector3 mainVectorCopy = new Vector3(this.gameObject.transform.position.x- bubble.transform.position.x, this.gameObject.transform.position.y- bubble.transform.position.y, 0);
    //        //top point

    //        Vector3 pointThree = Vector3.Cross(mainVectorCopy, Vector3.forward);
    //        pointThree.Normalize();
    //        var newPoint = (radius/2) * pointThree + bubble.transform.position;
    //        var newPoint2 = (-radius/2) * pointThree + bubble.transform.position;

    //        Debug.DrawLine(newPoint, newPoint2, Color.black);

    //        //hypotonuse up
    //        Debug.DrawLine(newPoint, this.gameObject.transform.position, Color.black);
    //        //hypotonuse down
    //        Debug.DrawLine(newPoint2, this.gameObject.transform.position, Color.black);



    //        //this is the normalized value of the angle out of the perpendicular line from char to bubble
    //        //degree to the top-newPoint
    //        //top/center towrads char
    //        Vector3 top = newPoint;
    //        Vector3 center = bubble.transform.position;
    //        Vector3 centerToChar = new Vector3(this.gameObject.transform.position.x- bubble.transform.position.x, this.gameObject.transform.position.y - bubble.transform.position.y);
    //        Vector3 centerToTop = new Vector3(top.x - center.x, top.y, center.x);
    //        float maxAngle = Vector2.Angle(centerToChar, centerToTop);
    //        //degree to where your pointing - hit
    //        Vector3 pointing = hit.point;
    //        Vector3 centerToPoint = new Vector3(pointing.x - center.x, pointing.y, center.x);
    //        float currentAngle = Vector2.Angle(centerToChar, centerToPoint);

    //        float powerLevelFromAngle = maxAngle - currentAngle;

    //        //normalize value
    //        float normalValue = powerLevelFromAngle/ maxAngle;//power level
    //        //distancew
    //        float distanceForPower = Vector3.Distance(this.gameObject.transform.position, bubble.transform.position);
    //        float normalizedDistance = 6-Mathf.Clamp(distanceForPower, 1, 5);//(distanceForPower - .01f) / (3f - .01f);//

    //        Debug.Log($"Max angle:{maxAngle} current angle:{currentAngle} and here is powerLevel:{powerLevelFromAngle} normal value:{normalValue} and here is distcance normalized:{normalizedDistance}");
    //        //getting perpendicular line
    //        //v = P2 - P1
    //        //P3 = (-v.y, v.x) / Sqrt(v.x ^ 2 + v.y ^ 2) * h
    //        //P4 = (-v.y, v.x) / Sqrt(v.x ^ 2 + v.y ^ 2) * -h







    //        //var force = 1f;
    //        //bubble.transform.GetComponent<Rigidbody2D>().AddForceAtPosition(force * dir, hit.point);

    //        //(bubble to ghost) - radius = the length of the outside


    //        //distance
    //        //power level
    //        //angle at which you hit the circle

    //    }
    //}
}
