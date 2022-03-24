using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowEnemy : MonoBehaviour
{

    public Transform player;
    public Transform bubble;
    public MyJoystickExample playerObject;
    Vector2 PlayerDirection = new Vector2(1,0);
    Vector2 EnemyToPlayerDirection;
    public float speed;


    void Update()
    {
        
        //TODO try to figure out how to rotate axis based on sprite orientation

        EnemyToPlayerDirection = new Vector2(this.transform.position.x - player.position.x, this.transform.position.y - player.position.y).normalized;


        if ((EnemyToPlayerDirection.x > 0 && PlayerDirection.x < 0)|| (EnemyToPlayerDirection.x < 0 && PlayerDirection.x > 0))
        {
            Move();
        }


        //update player direction to the last direction you moved
        if (playerObject.GetDirection.x == 0 && playerObject.GetDirection.y == 0)
            return;
        PlayerDirection = playerObject.GetDirection;


    }

    private void Move()
    {
        this.transform.position = Vector2.MoveTowards(transform.position, bubble.position, speed * Time.deltaTime);
    }



}
