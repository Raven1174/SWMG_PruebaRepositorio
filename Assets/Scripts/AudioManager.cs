using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager audioManager;

    public AudioSource powerUpAudio;
    public AudioSource damageAudio;

    private void Awake() {
        if(audioManager != null)
            Destroy(this.gameObject);
        else
        {   
            audioManager = this;
            DontDestroyOnLoad(this);
        }
    }

    public void GetDamageSound()
    {
        damageAudio.Play();
    }

    public void GetPowerUpSound()
    {
        powerUpAudio.Play();
    }
    //when the inpostor is sus
    //akjf
}
