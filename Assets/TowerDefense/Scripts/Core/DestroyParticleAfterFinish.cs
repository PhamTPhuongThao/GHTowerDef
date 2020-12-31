using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyParticleAfterFinish : MonoBehaviour
{
    private ParticleSystem thisParticleSystem;
    void Start()
    {
        thisParticleSystem = GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        if (thisParticleSystem.isPlaying)
        {
            return;
        }
        Destroy(gameObject);

    }
}
