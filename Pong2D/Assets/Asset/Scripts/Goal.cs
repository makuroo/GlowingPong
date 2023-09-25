using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            collision.transform.GetComponent<TrailRenderer>().enabled = false;
            GameManager.instance.Score(this.gameObject.tag);
            if (GameManager.instance.player1Score >= GameManager.instance.targetScore || GameManager.instance.player2Score >= GameManager.instance.targetScore)
            {
                GameManager.instance.ActivateParticleEfffects(collision.GetComponent<ParticleSystem>());
                StartCoroutine(GameManager.instance.GameEnded());
            }
            else
            {
                collision.gameObject.SendMessage("RestartGame", 5.0f, SendMessageOptions.RequireReceiver);
            }
            
        }
    }
}
