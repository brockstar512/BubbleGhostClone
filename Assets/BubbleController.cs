using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleController : MonoBehaviour
{
    public Transform respawnPoint;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    //    private void OnCollisionEnter(Collision other)

    private void OnTriggerEnter2D(Collider2D other)
    {
        switch (other.gameObject.tag)
        {
            case "Friendly":
                Debug.Log("Friendly");
                break;
            case "Finish":
                Debug.Log("End");
                break;
            default:
                Debug.Log("Pop");
                this.transform.position = respawnPoint.position;
                //Destroy(this.gameObject);
                break;

        }
    }
}
