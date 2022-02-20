using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.EventSystems;
public class MobileJoystick : MonoBehaviour, IPointerUpHandler, IDragHandler,IPointerDownHandler
{
    private RectTransform joyStickTransform;

    [SerializeField]
    private float dragThreshold = 0.6f;//limit diagonal movment
    [SerializeField]
    private int dragMoveDistance = 30;
    [SerializeField]
    private int dragOffsetDistance = 100;//limit movement range

    public event Action<Vector2> OnMove;
    public PlayerController playerController;


    // Start is called before the first frame update
    void Awake()
    {
        joyStickTransform = (RectTransform)transform;
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 offSet;
        //calculates the local position in our joystick transform
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            joyStickTransform,
            eventData.position,
            null,
            out offSet
            );
        //(-1) -> 1
        offSet = Vector2.ClampMagnitude(offSet, dragOffsetDistance) / dragOffsetDistance;
        Debug.Log(offSet);
        joyStickTransform.anchoredPosition = offSet * dragOffsetDistance;

        Vector2 inputVector = CalculateMovementInput(offSet);
        //OnMove? Invoke(inputVector);//move character
        playerController.Move(inputVector);
    }

    //limits diagnoal movement
    Vector2 CalculateMovementInput(Vector2 offSet)
    {
        float x = Mathf.Abs(offSet.x) > dragThreshold ? offSet.x : 0;
        float y = Mathf.Abs(offSet.y) > dragThreshold ? offSet.y : 0;
        return new Vector2(x, y);

    }


    public void OnPointerDown(PointerEventData eventData)
    {
        //this is here so that OnPointer Up works
    }

    //will only work if we have the ipointer down handler
    public void OnPointerUp(PointerEventData eventData)
    {
        joyStickTransform.anchoredPosition = Vector2.zero;
        //OnMove?.Invoke(Vector2.zero);//inform character you have stopped moving
        playerController.Move(Vector2.zero);


    }


}
