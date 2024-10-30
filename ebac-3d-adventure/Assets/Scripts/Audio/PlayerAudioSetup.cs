using System.Collections.Generic;
using UnityEngine;

public class PlayerAudioSetup : MonoBehaviour
{
    [Header("FOOTSTEPS")]
    public List<AudioClip> footstepSounds; // Lista de sons de passos
    public AudioSource stepAudioSource; // Componente AudioSource
    [Space]
    [Header("JUMP")]
    public AudioClip jumpSound;
    public AudioSource jumpAudioSource; // Componente AudioSource
    //[Space]
    //[Header("SHOOT")]
    //public AudioClip shootSound;
    //public AudioSource shootAudioSource; // Componente AudioSource


    public void PlayFootstepSound()
    {
        if (footstepSounds.Count > 0 && stepAudioSource != null)
        {
            // Escolhe um som aleatório da lista
            int index = Random.Range(0, footstepSounds.Count);
            AudioClip footstepSound = footstepSounds[index];

            // Toca o som selecionado
            stepAudioSource.PlayOneShot(footstepSound);
        }
    }

    public void PlayJumpSound()
    {
        jumpAudioSource.PlayOneShot(jumpSound);
    }

    //public void PlayShootSound()
    //{
    //    shootAudioSource.PlayOneShot(shootSound);
    //}
}