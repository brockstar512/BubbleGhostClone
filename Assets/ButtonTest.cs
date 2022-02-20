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

    public float blowForce { get { return slider.value; } }

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
            blowPower = (CalculateForce(blowPower) * - 1);
            slider.value = lastPos + blowPower * decreaseRate;
        }
        //Debug.Log($"{blowForce}");


  
    }

}
