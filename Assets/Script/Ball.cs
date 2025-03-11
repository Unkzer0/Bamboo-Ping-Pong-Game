using UnityEngine;

public class Ball : MonoBehaviour
{
    public float speed = 10f;  
    private Rigidbody2D rb;
    public AudioClip hitSound;  
    private AudioSource audioSource;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
    }

    void Start()
    {
        LaunchBall();
    }

    public void IncreaseSpeed(float amount)
    {
        speed += amount;  
        rb.velocity = rb.velocity.normalized * speed;  
    }

    void LaunchBall()
    {
        float xDirection = Random.Range(0, 2) == 0 ? -1 : 1;
        float yDirection = Random.Range(-1f, 1f);
        rb.velocity = new Vector2(xDirection, yDirection).normalized * speed;
    }

    public void Respawn()
    {
        transform.position = Vector2.zero;
        rb.velocity = Vector2.zero;
        LaunchBall();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Paddle")) 
        {
            speed += 0.5f;  
            rb.velocity = rb.velocity.normalized * speed;  

            if (hitSound != null)
            {
                audioSource.PlayOneShot(hitSound);  
            }
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Goal_Left"))
        { 
            GameManager.Instance.ScorePoint("Right");
            GameManager.Instance.RespawnBall();  
        }
        else if (collision.CompareTag("Goal_Right"))
        {   
            GameManager.Instance.ScorePoint("Left");
            GameManager.Instance.RespawnBall();  
        }
    }
}

