using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIPaddle : MonoBehaviour
{
    private Transform ball;    
    public float speed = 10f;
    public float reactionSpeed = 0.1f; 
    public float minSize = 0.5f;
    public float shrinkAmount = 0.2f;
    float positiveNewRange = 2.7f;
    float negativeNewRange = -2.7f;

    private Vector3 originalScale;

    void Start()
    {   
        originalScale = transform.localScale;
        GameObject foundBall = GameObject.FindWithTag("Ball");
        if (foundBall != null)
        {
            ball = foundBall.transform;
        }
    }

    public void ShrinkPaddle()
    {
        if (transform.localScale.y > 0.5f)  
        {
            transform.localScale -= new Vector3(0, 0.2f, 0);
            positiveNewRange+= 0.1f;
            negativeNewRange-= 0.1f;
        }
    }

    void Update()
    {
        transform.position = new Vector3(
            transform.position.x,
            Mathf.Clamp(transform.position.y,negativeNewRange,positiveNewRange),
            transform.position.z
        );
    }

    void FixedUpdate()
    {
        if (ball == null) 
        {
            GameObject foundBall = GameObject.FindWithTag("Ball");
            if (foundBall != null)
                ball = foundBall.transform;
        }

        if (ball == null) return;

        float targetY = Mathf.Lerp(transform.position.y, ball.position.y, reactionSpeed);
        transform.position = new Vector2(transform.position.x, targetY);
    }
}











