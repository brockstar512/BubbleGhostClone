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



    public void OnPointerDown(PointerEventData eventData)
    {

        float timeSinceLastClick = Time.time - lastClick;
        if (timeSinceLastClick <= DOUBLIE_CLICK_TIME)
        {
            Debug.Log("Double Click!");
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
        //change ui
        player.GetComponent<MyJoystickExample>().A_Button(holdInPlace);
    }

    void LookAtTarget()
    {
        Vector2 target = new Vector2(bubble.transform.position.x - player.transform.position.x, bubble.transform.position.y - player.transform.position.y);
        //Debug.Log($"Trying to look at{target} by subtracting {bubble.transform.position} with {player.transform.position}");
        player.GetComponent<MyJoystickExample>().A_Button(target);//make delegates insted

    }
}
