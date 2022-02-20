using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using UnityEngine.EventSystems;

public class VariableJoystick : Joystick
{
    public float MoveThreshold { get { return moveThreshold; } set { moveThreshold = Mathf.Abs(value); } }

    [SerializeField] private float moveThreshold = 1;
    [SerializeField] private JoystickType joystickType = JoystickType.Fixed;

    private Vector2 fixedPosition = Vector2.zero;


    public void SetMode(JoystickType joystickType)
    {
        this.joystickType = joystickType;
        if(joystickType == JoystickType.Fixed)
        {
            background.anchoredPosition = fixedPosition;
            background.gameObject.SetActive(true);
        }
        else
            background.gameObject.SetActive(false);
    }

    protected override void Start()
    {
        base.Start();
        fixedPosition = background.anchoredPosition;
        SetMode(joystickType);
        //sr = background.transform.GetComponent<Image>();

    }

    public override void OnPointerDown(PointerEventData eventData)
    {


        if (joystickType != JoystickType.Fixed)
        {
            background.anchoredPosition = ScreenPointToAnchoredPosition(eventData.position);
            background.gameObject.SetActive(true);
        }
        base.OnPointerDown(eventData);
    }

    public override void OnPointerUp(PointerEventData eventData)
    {
        if(joystickType != JoystickType.Fixed)
            background.gameObject.SetActive(false);

        base.OnPointerUp(eventData);
    }

    protected override void HandleInput(float magnitude, Vector2 normalised, Vector2 radius, Camera cam)
    {
        if (joystickType == JoystickType.Dynamic && magnitude > moveThreshold)
        {
            Vector2 difference = normalised * (magnitude - moveThreshold) * radius;
            background.anchoredPosition += difference;
        }
        base.HandleInput(magnitude, normalised, radius, cam);
    }

    //private void OnDrawGizmos()
    //{
    //    //-1.75 - edge
    //    Gizmos.color = Color.red;

    //    Vector2 radius = background.sizeDelta / 2;
    //    Vector2 start = background.transform.position;//always the centur
    //    Vector2 maybe = background.transform.localPosition;//always the centur
    //    Vector2 radiusBitch = new Vector2(0,background.rect.height/2);
    //    Debug.Log($"Pointer location and start {maybe } and {start}");
    //    Gizmos.DrawLine(start, start + radiusBitch);

    //    //Gizmos.color = Color.yellow;
    //    //Gizmos.DrawSphere(start, r);
    //}


}

public enum JoystickType { Fixed, Floating, Dynamic }