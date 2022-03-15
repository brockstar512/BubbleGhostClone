using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class ButtonTest : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    //this only manages blow power
    bool isDown = false;
    public Slider slider;
    public float startTime = 0;
    public float endTime = 0;
    public float blowPower = 0;
    const float MaxBlowForce = 10;
    public float blowForce { get { return slider.value * MaxBlowForce; } }

    float lastPos = 0;
    float increaseRate = 1.5f;
    float decreaseRate = 2f;



    public GameObject bubble;
    public GameObject ghost;



    public void OnPointerDown(PointerEventData eventData)
    {


        //needs to start at current slider value
        lastPos = slider.value;
        isDown = true;
        startTime = Time.time;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        //needs to start at current slider value
        lastPos = slider.value;
        isDown = false;
        endTime = Time.time;
    }
    //out of how long you've held the button what is that normalized value
    private float CalculateForce(float holdTime)
    {
        float maxForceHoldDownTime = 2f;
        float holdTimeNormalized = Mathf.Clamp01(holdTime / maxForceHoldDownTime);
        //force = holdTimeNormalized * maxForce
        return holdTimeNormalized;
    }


    // Update is called once per frame
    void Update()
    {
        if (isDown)
        {
            blowPower = (Time.time - startTime);//how long youve been holding the button
            blowPower = CalculateForce(blowPower);
            slider.value = lastPos + blowPower * increaseRate;
            //Debug.Log(blowPower);

        }
        else
        {
            blowPower = (Time.time - endTime);
            blowPower = (CalculateForce(blowPower) * -1);
            slider.value = lastPos + blowPower * decreaseRate;
        }
        //Debug.Log($"{blowForce}");

    }

    public void FixedUpdate()
    {
        SendRay();
    }


    void SendRay()
    {

        RaycastHit2D hit = Physics2D.Raycast(ghost.transform.position, ghost.transform.up);
        Debug.DrawRay(transform.position, ghost.transform.up, Color.green);

        //If something was hit.
        if (hit.collider != null)
        {


            float radius = bubble.GetComponent<CircleCollider2D>().radius;

            //the direction the bubble should travel...i think the direction is off on the inverse normal one
            Debug.DrawLine(hit.point, bubble.transform.position, Color.black);
            Vector3 dir = new Vector3(bubble.transform.position.x - hit.point.x, bubble.transform.position.y - hit.point.y, 0);



            //kite
            //center of ghost to center of bubble
            Debug.DrawLine(ghost.transform.position, bubble.transform.position, Color.black);
            //perpendicular line
            Vector3 mainVectorCopy = new Vector3(ghost.transform.position.x - bubble.transform.position.x, ghost.transform.position.y - bubble.transform.position.y, 0);
            //top point

            Vector3 pointThree = Vector3.Cross(mainVectorCopy, Vector3.forward);
            pointThree.Normalize();
            var newPoint = (radius / 2) * pointThree + bubble.transform.position;
            var newPoint2 = (-radius / 2) * pointThree + bubble.transform.position;

            Debug.DrawLine(newPoint, newPoint2, Color.black);

            //hypotonuse up
            Debug.DrawLine(newPoint, ghost.transform.position, Color.black);
            //hypotonuse down
            Debug.DrawLine(newPoint2, ghost.transform.position, Color.black);



            //this is the normalized value of the angle out of the perpendicular line from char to bubble
            //degree to the top-newPoint
            //top/center towrads char
            Vector3 top = newPoint;
            Vector3 center = bubble.transform.position;
            Vector3 centerToChar = new Vector3(ghost.transform.position.x - bubble.transform.position.x, ghost.transform.position.y - bubble.transform.position.y);
            Vector3 centerToTop = new Vector3(top.x - center.x, top.y, center.x);
            float maxAngle = Vector2.Angle(centerToChar, centerToTop);
            //degree to where your pointing - hit
            Vector3 pointing = hit.point;
            Vector3 centerToPoint = new Vector3(pointing.x - center.x, pointing.y, center.x);
            float currentAngle = Vector2.Angle(centerToChar, centerToPoint);

            float powerLevelFromAngle = maxAngle - currentAngle;

            //normalize value
            float normalValue = powerLevelFromAngle / maxAngle;//power level
            //distancew
            float distanceForPower = Vector3.Distance(ghost.transform.position, bubble.transform.position);
            float normalizedDistance = 6 - Mathf.Clamp(distanceForPower, 1, 5);//(distanceForPower - .01f) / (3f - .01f);//

            //Debug.Log($"Max angle:{maxAngle} current angle:{currentAngle} and here is powerLevel:{powerLevelFromAngle} normal value:{normalValue} and here is distcance normalized:{normalizedDistance}");
            //getting perpendicular line
            //v = P2 - P1
            //P3 = (-v.y, v.x) / Sqrt(v.x ^ 2 + v.y ^ 2) * h
            //P4 = (-v.y, v.x) / Sqrt(v.x ^ 2 + v.y ^ 2) * -h







            //var force = 1f;//took out displaced power from angle
            float finalForce = blowForce * normalizedDistance;//final power

            //Debug.Log($"Final Force:{finalForce} blow for {blowForce} distance {normalizedDistance}");

            //bubble.transform.GetComponent<Rigidbody2D>().AddForceAtPosition(finalForce * dir, hit.point);

            //(bubble to ghost) - radius = the length of the outside
            if (isDown)
            {
                ApplyForce(finalForce, dir, hit.point);
            }
        }

    }

    //blow power/residual pwer //direction and point
    void ApplyForce(float power, Vector2 direction, Vector2 contactPoint)
    {
        //bubble.transform.GetComponent<Rigidbody2D>().AddForceAtPosition(power * direction, contactPoint);
        bubble.transform.GetComponent<Rigidbody2D>().AddForce(power * direction, ForceMode2D.Impulse);

    }

}
