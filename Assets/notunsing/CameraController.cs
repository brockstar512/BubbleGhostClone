using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    //this script is from press start https://www.youtube.com/watch?v=GTxiCzvYNOc
    //im going to try and use cinemachine instead because its less of a headache
    //here are other videos from the series of press start
    //https://www.youtube.com/watch?v=Mp6BWCMJZH4&t=3s
    //https://www.youtube.com/watch?v=ailbszpt_AI

    bool fixedCamera;
    public GameObject followObject;
    public Vector2 followOffset;
    private Vector2 threshold;
    public float speed = 3f;
    private Rigidbody2D followObjectRb;
    // Start is called before the first frame update
    void Start()
    {
        threshold = calculateThreshold();
        followObjectRb = followObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector2 follow = followObject.transform.position;

        //if the differences exceed our threshhol move the character
        float xDifference = Vector2.Distance(Vector2.right * transform.position.x, Vector2.right * follow.x);
        float yDifference = Vector2.Distance(Vector2.up * transform.position.y, Vector2.up * follow.y);// this helps add a value/ multiply a value by 1 and turn it into a vector 2 or 3

        Vector3 newPosition = transform.position;
        //differen will always be a position number and if its more than the threshold move the character
        if(Mathf.Abs(xDifference)>= threshold.x)
        {
            newPosition.x = follow.x;
        }
        if (Mathf.Abs(yDifference) >= threshold.y)
        {
            newPosition.y = follow.y;
        }
        //transform.position = newPosition;
        float moveSpeed = followObjectRb.velocity.magnitude > speed ? followObjectRb.velocity.magnitude : speed;
        Debug.Log($"moveSpeed: {moveSpeed}");

        transform.position = Vector3.MoveTowards(transform.position, newPosition, moveSpeed * Time.deltaTime);

    }

    /// <summary>
    /// the bounds of the screen our character can move before the camera starts moving
    /// </summary>
    /// <returns></returns>
    private Vector3 calculateThreshold()
    {
        Rect aspect = Camera.main.pixelRect;
        Vector2 t = new Vector2(Camera.main.orthographicSize * aspect.width / aspect.height, Camera.main.orthographicSize);
        //Debug.Log($"t: {t}");
        t.x -= followOffset.x;
        t.y -= followOffset.y;
        return t;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Vector2 border= calculateThreshold();
        Gizmos.DrawWireCube(transform.position, new Vector3(border.x *2, border.y * 2, 1));
    }
}
