using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class BallPhysics : MonoBehaviour
{
    [SerializeField] private GameManager GM;
    private Rigidbody2D rb;
    private Vector2 vel;
    private float ballForceX = 20, ballForceY = 15;
    [SerializeField] private TrailRenderer trailRenderer;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        trailRenderer = transform.GetComponent<TrailRenderer>();
        Invoke("GoBall", 2f);
    }

    // Update is called once per frame
    void Update()
    {
        BallOutOfBound();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        SkillManager player = collision.gameObject.GetComponent<SkillManager>();
        AudioManager.audioManagerInstance.PlayOneShot("Hit");
        if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Player2"))
        {
            vel.x = rb.velocity.x;
            vel.y = (rb.velocity.y / 2) + (collision.collider.attachedRigidbody.velocity.y / 3); //mengambil nilai velocity player
            while (Mathf.Abs(vel.x) < 10f)
            {
                vel.x += 20;
            }
            rb.velocity = vel;
        }

        if (player == null)
        {
            GameManager.instance.CheckShieldStatus(collision);
            trailRenderer.emitting = true;
            StartCoroutine(DisableTrailRendererEmitter(.5f));
        }
        else  
            player.SetExtraShieldPercentage(player);
       
    }

    void GoBall()
    {
        float rand = Random.Range(0, 2); //akan random nilai diantara 0-1
        ballForceX = Random.Range(80, 90);
        ballForceY = Random.Range(-15, 15.1f);

        if (rand < 1)
        {
            rb.AddForce(new Vector2(ballForceX, ballForceY));//add force memberikan tenaga
                                                             //liat doc add force disini https://docs.unity3d.com/ScriptReference/Rigidbody.AddForce.html
        }
        else
        {
            rb.AddForce(new Vector2(-ballForceX, ballForceY));
        }
        trailRenderer.enabled = true;
        if (trailRenderer.emitting == false)
            trailRenderer.emitting = true;
        StartCoroutine(DisableTrailRendererEmitter(.5f));
    }

    void ResetBall() //ini kita buat nilai transform jadi 0
    {
        rb.velocity = Vector2.zero;
        transform.position = new Vector2(0, 0.28f);
    }

    void RestartGame()
    {
        // GameManager.instance.vCam.GetComponent<CinemachineVirtualCamera>().enabled = false;
        ResetBall();
        Invoke("GoBall", 1);
    }

    private IEnumerator DisableTrailRendererEmitter(float time)
    {
        yield return new WaitForSeconds(time);
        trailRenderer.emitting = false;
    }

    private void BallOutOfBound()
    {
        if (transform.position.x > 22f || transform.position.y >10.5)
        {
            ResetBall();
        }
    }
}
