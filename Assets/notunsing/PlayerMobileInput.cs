using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerMobileInput : MonoBehaviour
{
    public Vector2 MovementVector { get; private set; }
    public event Action<Vector2> OnMovement;

    [SerializeField]
    private MobileJoystick joystick;
    // Start is called before the first frame update
    void Start()
    {
        joystick.OnMove += Move;
    }

    private void Move(Vector2 input)
    {
        MovementVector = input;
        //OnMovement?.Invoke(MovementVector);// call the function that makes you character move
    }


}
