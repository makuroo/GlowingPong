using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallParticleEffects : MonoBehaviour
{

    public ParticleSystem particleSystems { get; private set; }
    // Start is called before the first frame update
    void Start()
    {
        particleSystems = transform.GetChild(0).GetComponent<ParticleSystem>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameManager.instance.ActivateParticleEfffects(particleSystems);
    }
     
    
}
