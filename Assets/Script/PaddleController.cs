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

    private float moveDirection = 0f; // UI input
    private float velocity = 0f;

    float positiveNewRange = 2.7f;
    float negativeNewRange = -2.7f;


    void Start()
    {
        originalScale = transform.localScale;
    }

    void Update()
    {
        float inputDirection = 0f;

        // PC Keyboard input
        if (Input.GetKey(KeyCode.W))
        {
            inputDirection += 1f;
        }
        if (Input.GetKey(KeyCode.S))
        {
            inputDirection -= 1f;
        }

         // Combine with UI input
        inputDirection += moveDirection;
          // Prevent extreme values
        inputDirection = Mathf.Clamp(inputDirection, -1f, 1f);

       
        velocity = Mathf.Lerp(velocity, inputDirection * speed, Time.deltaTime * acceleration);
        transform.Translate(0, velocity * Time.deltaTime, 0);

        transform.position = new Vector3(
            transform.position.x,
            Mathf.Clamp(transform.position.y,negativeNewRange,positiveNewRange),
            transform.position.z
        );
    }
    
    public void UI_MoveUp()
   {
     moveDirection = 1f;
   }

   public void UI_MoveDown()
  {
    moveDirection = -1f;
  }

   public void UI_StopMoving()
  {
    moveDirection = 0f;
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

    public IEnumerator GrowPaddle(float growthAmount, float duration)
    {
        float newSize = transform.localScale.y + growthAmount;
        transform.localScale = new Vector3(transform.localScale.x, newSize, transform.localScale.z);

        yield return new WaitForSeconds(duration);

        transform.localScale -= new Vector3(0, growthAmount, 0);
    }
}
