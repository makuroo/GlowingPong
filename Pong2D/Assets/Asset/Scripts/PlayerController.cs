using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 3f;
    public float boundY = 5f;
    private Rigidbody2D rb;
    public ParticleSystem playerParticleSystem;
    public Skill skillType;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerParticleSystem = transform.GetChild(0).GetComponent<ParticleSystem>();

    }

    // Update is called once per frame
    void Update()
    {
        Move();
        BoundCheck();
    }


    private void BoundCheck()
    {
        var pos = transform.position;
        if (pos.y > boundY)
        {
            pos.y = boundY;
        }
        else if (pos.y < -boundY)
        {
            pos.y = -boundY;
        }
        transform.position = pos;
    }

    private void Move()
    {
        if (transform.CompareTag("Player"))
        {
            if (Input.GetKey(KeyCode.W))
            {
                rb.velocity = new Vector2(0, moveSpeed);
            }
            else if (Input.GetKey(KeyCode.S))
            {
                rb.velocity = new Vector2(0, -moveSpeed);
            }
            else
            {
                rb.velocity = new Vector2(0, 0);
            }
        }
        else
        {
            if (Input.GetKey(KeyCode.UpArrow))
            {
                rb.velocity = new Vector2(0, moveSpeed);
            }
            else if (Input.GetKey(KeyCode.DownArrow))
            {
                rb.velocity = new Vector2(0, -moveSpeed);
            }
            else
            {
                rb.velocity = new Vector2(0, 0);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            float ballY = collision.transform.position.y;
            if (ballY <= 0.79 || ballY >= -0.79)
            {
                playerParticleSystem.transform.position = new Vector2(playerParticleSystem.transform.position.x, ballY);
                GameManager.instance.ActivateParticleEfffects(playerParticleSystem);
            }
        }
    }
}
