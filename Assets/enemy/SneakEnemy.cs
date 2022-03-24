using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SneakEnemy : MonoBehaviour
{
    [SerializeField] Transform bubble;
    [SerializeField] Transform player;
    public float speed;
    public float minimumDistance;
   public  enum State
        {
            Follow,
            Pause,
            Wonder
        }
    public  State state;


    // Start is called before the first frame update
    void Awake()
    {
        state = State.Follow;
    }

    // Update is called once per frame
    void Update()
    {

        //if (Vector2.Distance(transform.position, player.position) > minimumDistance)
            //Move();
        

            //Vector2.Distance(transform.position, player.position) < minimumDistance)
                //Flee();

            //state = State.Follow;
        
        //switch (state)
        //{
        //    case State.Follow:
        //        //Move();
        //        break;
        //}
    }



    private void Move()
    {
        this.transform.position = Vector2.MoveTowards(transform.position, bubble.position, speed *Time.deltaTime); 
    }

    public void Flee()
    {
        //Debug.Log("Flee");
        this.transform.position = Vector2.MoveTowards(transform.position, player.position, -speed * Time.deltaTime);
    }
}
