using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BG_Sounds_Manager : MonoBehaviour
{
    //create a list of audio clips
    public List<AudioClip> bgSounds = new List<AudioClip>();
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //invoke a function repeteadly to play a random sound from the list of bgSounds
       // InvokeRepeating("PlayRandomSound", 0f, 10f);
    }

    //create a function to play a random sound from the list of bgSounds
    void PlayRandomSound()
    {
        //get a random sound from the list of bgSounds
        AudioClip randomSound = bgSounds[Random.Range(0, bgSounds.Count)];
        //play the random sound
        AudioSource.PlayClipAtPoint(randomSound, transform.position);
    }
}
