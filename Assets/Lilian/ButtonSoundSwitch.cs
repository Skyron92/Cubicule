using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class ButtonSoundSwitch : MonoBehaviour
{
    private GameObject citySoundSource;
    public AudioSource[] Sounds;
    private float[] initialVolumes;
    public float fadeDuration = 2f;
    private float fadeTimer;
    private bool ActiveButton = false;


    private void Start()
    {
        citySoundSource = GameObject.FindGameObjectWithTag("CitySound");
        Sounds = citySoundSource.GetComponents<AudioSource>();
        Debug.Log("je trouve : " + Sounds);


        initialVolumes = new float[Sounds.Length];
        for (int i = 0; i < Sounds.Length; i++)
        {
            initialVolumes[i] = Sounds[i].volume;
        }

        fadeTimer = 0f;



    }


    public void ButtonPressed()
    {
        ActiveButton = true;
    }

    private void Update()
    {

        if (ActiveButton == true)
        {
            if (fadeTimer < fadeDuration)
            {
                Debug.Log("test");


                float progress = fadeTimer / fadeDuration;
                for (int i = 0; i < Sounds.Length; i++)
                {
                    float newVolume = Mathf.Lerp(initialVolumes[i], 0f, progress);
                    Sounds[i].volume = newVolume;
                }


                fadeTimer += Time.deltaTime;
            }
            else
            {

                for (int i = 0; i < Sounds.Length; i++)
                {
                    Sounds[i].Stop();
                    ActiveButton = false;
                }
            }
        }
    }
}
    

