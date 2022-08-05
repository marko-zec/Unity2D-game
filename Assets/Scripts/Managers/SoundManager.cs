using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static AudioClip playerHitSound;
    public static AudioClip pickUpCoinSound;
    public static AudioClip pickupHealthSound;
    public static AudioClip pistol1Sound, pistol2Sound, pistol3Sound;
    public static AudioClip enemyKilledSound;
    
    static AudioSource audioSrc;

    void Start()
    {
        playerHitSound = Resources.Load<AudioClip>("oof");
        pickUpCoinSound = Resources.Load<AudioClip>("handleCoins");
        pickupHealthSound = Resources.Load<AudioClip>("pickupHealth");
        pistol1Sound = Resources.Load<AudioClip>("pistolFire2");
        pistol2Sound = Resources.Load<AudioClip>("pistolFire2");
        pistol3Sound = Resources.Load<AudioClip>("pistolFire3");
        enemyKilledSound = Resources.Load<AudioClip>("splat");

        audioSrc = GetComponent<AudioSource>();
    }

    public static void PlaySound(string clip)
    {
        switch(clip)
        {
            case "playerHit":
                audioSrc.PlayOneShot(playerHitSound);
                break;
            case "pickedUpCoin":
                audioSrc.PlayOneShot(pickUpCoinSound);
                break;
            case "pickedUpHealth":
                audioSrc.PlayOneShot(pickupHealthSound);
                break;
            case "pistol1":
                audioSrc.PlayOneShot(pistol1Sound);
                break;
            case "pistol2":
                audioSrc.PlayOneShot(pistol2Sound);
                break;
            case "pistol3":
                audioSrc.PlayOneShot(pistol3Sound);
                break;
            case "enemyKilled":
                audioSrc.PlayOneShot(enemyKilledSound);
                break;
        }
    }
}
