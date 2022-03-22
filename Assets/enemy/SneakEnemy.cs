using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SneakEnemy : MonoBehaviour
{
    [SerializeField] GameObject bubble;
    [SerializeField] GameObject player;
    enum State
        {
            Follow,
            Pause,
            Wonder
        }
    private State state;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }



    private void Move()
    {
        
    }
}
