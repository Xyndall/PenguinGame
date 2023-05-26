using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAudio : MonoBehaviour
{
    [Header("MovementAudio")]
    [SerializeField] private AudioSource movementSource;
    public AudioClip[] hardBonkClips;
    public AudioClip[] softBonkClips;

    [Header("ExtraAudio")]
    [SerializeField] private AudioSource extraSource;
    public AudioClip JumpClip;
    public AudioClip DashClip;


    public void PlayJump()
    {
        extraSource.PlayOneShot(JumpClip);

    }

    public void PlayDash()
    {
        extraSource.PlayOneShot(DashClip);
    }

    public void PlayHardCollision()
    {
        int RandomClip = Random.Range(0, hardBonkClips.Length);

        movementSource.PlayOneShot(hardBonkClips[RandomClip]);
    }

    public void PlaySoftCollision()
    {
        int RandomClip = Random.Range(0, softBonkClips.Length);

        movementSource.PlayOneShot(softBonkClips[RandomClip]);
    }

}
