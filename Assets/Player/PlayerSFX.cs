using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSFX : MonoBehaviour
{
    public AudioClip footStepClip;
    public AudioClip onHitClip;
    public AudioClip punchClip;
    public AudioClip jumpClip;
    public AudioClip powerUpClip;
    public AudioClip coinClip;

    AudioSource source;

    void Start()
    {
        source = GetComponent<AudioSource>();    
    }

    
    void Update()
    {
        
    }

    public void PlayFootStep()
    {
        if (gameObject.tag == "Player")
        {
            source.PlayOneShot(footStepClip);
            GetComponent<PlayerController>().CreateDust();
        }
        
    }

    public void PlayOnHit()
    {
        source.PlayOneShot(onHitClip);
    }

    public void PlayPunch()
    {
        source.PlayOneShot(punchClip);
    }

    public void PlayJump()
    {
        source.PlayOneShot(jumpClip);
    }

    public void PlayCoin()
    {
        source.PlayOneShot(coinClip);
    }

    public void PlayPowerUp()
    {
        source.PlayOneShot(powerUpClip);
    }
}
