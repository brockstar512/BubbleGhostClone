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

    //public AnimationController animationController;

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


    }

    void moveCharacter(Vector2 direction)
    {
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
        Debug.Log("B for blow");
    }
    public void A_Button()
    {
        Debug.Log("A for action");

    }
}
