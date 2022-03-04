using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class ButtonTwoTest : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{

    public GameObject player;
    public GameObject bubble;
    float timeLapse = 0;
    int clickCount = 0;
    bool isDown = false;
    private float lastClick;
    private const float DOUBLIE_CLICK_TIME = .2f;
    bool currentState = false;

    bool isTracking = false;
    float trackStart;
    public float trackingTime = 3f;



    private void Update()
    {


        if (isTracking)
        {
            LookAtTarget();
            //one second after you look at target or if you are holding down a 
            if (Time.time - trackStart >= trackingTime)//|| isTracking
            {
                Debug.Log($"Stop Tracking");
                isTracking = false;
            }
        }


    }

    public void OnPointerDown(PointerEventData eventData)
    {
        isTracking = true;
        trackStart = Time.time;


        float timeSinceLastClick = Time.time - lastClick;
        if (timeSinceLastClick <= DOUBLIE_CLICK_TIME)
        {
            Debug.Log("Double Click!");
            HoldInPlace(currentState);
        }
        else
        {
            LookAtTarget();
        }

        lastClick = Time.time;

        isDown = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isDown = false;

    }
    void HoldInPlace(bool holdInPlace)
    {
        isTracking = false;
        currentState = !holdInPlace;
        switch (holdInPlace)
        {
            case true:        
                player.GetComponent<MyJoystickExample>().A_Button(holdInPlace);
                break;
            case false:
                player.GetComponent<MyJoystickExample>().A_Button(holdInPlace);
                break;
        }

    }

    void LookAtTarget()
    {
        Vector2 target = new Vector2(bubble.transform.position.x - player.transform.position.x, bubble.transform.position.y - player.transform.position.y);
        //Debug.Log($"Trying to look at{target} by subtracting {bubble.transform.position} with {player.transform.position}");
        player.GetComponent<MyJoystickExample>().A_Button(target);//make delegates insted

    }
}
