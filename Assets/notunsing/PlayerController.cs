using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{


    public Vector2 movementDirection = new Vector2();
    public float movementSpeed;
    public float movementBaseSpeed = 1.0f;


    public float decceleration = -1;
    public float acceleration = 1;
    [SerializeField] private Rigidbody2D _rb;
    [SerializeField] private GameObject bubble;


    private void Awake()
    {
        _rb = this.GetComponent<Rigidbody2D>();
    }
    #region notCurrentlyUsing
    //void Update()
    //{
        //moveInputX = Input.GetAxis("Horizontal");
        //moveInputY = Input.GetAxis("Vertical");

        //Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0.0f);
        //transform.position = transform.position + movement * Time.deltaTime * _speed;

    //}
    void FixedUpdate()
    {

        return;

        Vector2 TargetSpeed = movementDirection * movementBaseSpeed;
        Vector2 SpeedDiff = TargetSpeed - _rb.velocity;
        float AccelRate = (Mathf.Abs(TargetSpeed.x) > 0.01f) || (Mathf.Abs(TargetSpeed.y) > 0.01f) ? acceleration : decceleration;
        ProcessInputs();
        Move();
    }


    void ProcessInputs()
    {
        movementDirection = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        movementSpeed = Mathf.Clamp(movementDirection.magnitude, 0.0f, 1.0f);
        movementDirection.Normalize();
    }

    public void Move()
    {
        //        _rb.velocity = direction * movementBaseSpeed;//* Time.fixedDeltaTime


        //how long your magnitude is at 1
        //acceleration should only affect magnitude

        //magnitude at what your pressing //the fastest you want it to go
        _rb.velocity = movementDirection * movementSpeed * movementBaseSpeed;//* Time.fixedDeltaTime
        //_rb.AddForce( movementDirection * movementSpeed * movementBaseSpeed);

    }

    #endregion
    public void Move(Vector2 inputVector)
    {
        _rb.velocity = inputVector * movementSpeed;//* Time.fixedDeltaTime

    }
    void Update()
    {
        Debug.Log($"distance between bubble and ghost {Vector3.Distance(transform.position, bubble.transform.position)}");
    }
    }
