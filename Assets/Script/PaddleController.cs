using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PaddleController : MonoBehaviour
{
    public float speed = 10f;
    public float minSize = 0.5f;
    public float shrinkAmount = 0.2f;
    private Vector3 originalScale;
    public float acceleration = 2f;
    private float moveDirection = 0f;
    private float velocity = 0f; 

    void Start()
    {
        originalScale = transform.localScale;
    }

    void Update()
    {
        velocity = Mathf.Lerp(velocity, moveDirection * speed, Time.deltaTime * acceleration);
        transform.Translate(0, velocity * Time.deltaTime, 0);

        transform.position = new Vector3(
            transform.position.x,
            Mathf.Clamp(transform.position.y, -2.7f, 2.7f),
            transform.position.z
        );
    }

    public void MoveUp()
    {
        moveDirection = 1;
    }

    public void MoveDown()
    {
        moveDirection = -1;
    }

    public void StopMoving()
    {
        moveDirection = 0;
    }

    public void ShrinkPaddle()
    {
        if (transform.localScale.y > 0.5f)
        {
            transform.localScale -= new Vector3(0, 0.2f, 0);
        }
    }

    public IEnumerator GrowPaddle(float growthAmount, float duration)
    {
        float newSize = transform.localScale.y + growthAmount;
        transform.localScale = new Vector3(transform.localScale.x, newSize, transform.localScale.z);

        yield return new WaitForSeconds(duration);

        transform.localScale -= new Vector3(0, growthAmount, 0);
    }
}
