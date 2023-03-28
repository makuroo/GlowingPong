using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollide : MonoBehaviour
{
    private float playerWidth = 0.79f;
    private ParticleSystem playerParticleSystem;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        playerParticleSystem = transform.GetChild(0).GetComponent<ParticleSystem>();
        if (collision.gameObject.CompareTag("Ball"))
        {
            float ballY = collision.transform.position.y;
            if (ballY <= playerWidth || ballY >= -playerWidth)
            {
                playerParticleSystem.transform.position = new Vector2(playerParticleSystem.transform.position.x, ballY);
                GameManager.instance.ActivateParticleEfffects(playerParticleSystem);
            }
        }
    }
}
