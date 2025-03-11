using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    public enum PowerUpType { Grow, SpeedBoost }
    public PowerUpType powerUpType;
    public AudioClip powerUpSound;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ball"))
        {
            PaddleController closestPaddle = FindClosestPaddle();
            if (closestPaddle != null)
            {
                ApplyPowerUp();
            }
            Destroy(gameObject);
        }
    }

    void ApplyPowerUp()
    {
        AudioSource.PlayClipAtPoint(powerUpSound, transform.position);
        
        PaddleController paddle = FindObjectOfType<PaddleController>();  
        Ball ball = FindObjectOfType<Ball>();  

        if (powerUpType == PowerUpType.Grow)
        {
            paddle.transform.localScale += new Vector3(0, 0.5f, 0);
        }
        else if (powerUpType == PowerUpType.SpeedBoost)
        {
            ball.IncreaseSpeed(2f);
        }
    }

    PaddleController FindClosestPaddle()
    {
        PaddleController[] paddles = FindObjectsOfType<PaddleController>();
        PaddleController closest = null;
        float minDistance = Mathf.Infinity;

        foreach (PaddleController paddle in paddles)
        {
            float distance = Vector2.Distance(transform.position, paddle.transform.position);
            if (distance < minDistance)
            {
                minDistance = distance;
                closest = paddle;
            }
        }
        return closest;
    }
}
