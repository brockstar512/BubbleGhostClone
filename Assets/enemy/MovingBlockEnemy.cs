using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingBlockEnemy : MonoBehaviour
{
    public AnimationCurve _animCurve;
    public AnimationCurve _animCurveDown;


    [SerializeField] Transform pointUp;
    [SerializeField] Transform pointDown;


    public float upSpeed = 0.5f;
    public float downSpeed = 0.5f;

    private float elapsedTime;
    public float upDuration;
    public float downDuration;



    void Start()
    {
        this.transform.position = pointDown.position;
    }

    [ContextMenu("Down")]
    public void Down()
    {
        StartCoroutine(DownLerp());

    }
    [ContextMenu("Up")]
    public void Up()
    {
        StartCoroutine(UpLerp());
    }

    IEnumerator UpLerp()
    {
        float elapsedTime = 0;
        while (elapsedTime < upDuration)
        {
            elapsedTime += Time.deltaTime;
            float percentageComplete = elapsedTime / upDuration;

            transform.position = Vector3.Lerp(pointDown.position, pointUp.position, _animCurve.Evaluate(percentageComplete));
            yield return null;
        }
        transform.position = pointUp.position;
        yield return new WaitForSeconds(.15f);
        StartCoroutine(DownLerp());

    }

    IEnumerator DownLerp()
    {
        float elapsedTime = 0;
        while (elapsedTime < downDuration)
        {
            elapsedTime += Time.deltaTime;
            float percentageComplete = elapsedTime / downDuration;

            transform.position = Vector3.Lerp(pointUp.position, pointDown.position, _animCurveDown.Evaluate(percentageComplete));
            yield return null;
        }
        transform.position = pointDown.position;
        yield return new WaitForSeconds(1f);
        StartCoroutine(UpLerp());
    }






}
