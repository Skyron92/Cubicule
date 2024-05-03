using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EventManager : MonoBehaviour
{

    private float time;
    private bool hasStarted;

    [SerializeField] private GameObject forest, lac, animals, balloonSpawner, misc;
    [SerializeField] private Image fadeImage;
    [SerializeField, Range(0,5)] private float forestTime, lacTime, animalsTime, balloonTime, miscTime;
    private float maxTime;
    private AudioSource _audioSource;

    private void Start() {
        _audioSource = GetComponent<AudioSource>();
        maxTime = _audioSource.clip.length;
    }

    // Update is called once per frame
    void Update() {
        if (hasStarted) time += Time.deltaTime;
        if (time >= GetTimeInSecond(forestTime) && !forest.activeSelf) forest.SetActive(true);
        if (time >= GetTimeInSecond(lacTime) && !lac.activeSelf) lac.SetActive(true);
        if (time >= GetTimeInSecond(miscTime) && !misc.activeSelf) misc.SetActive(true);
        if (time >= GetTimeInSecond(balloonTime) && !balloonSpawner.activeSelf) balloonSpawner.SetActive(true);
       // if (time >= GetTimeInSecond(animalsTime) && !animals.activeSelf) animals.SetActive(true);
       if (time >= maxTime) {
           time = 0;
           fadeImage.DOFade(1, 2).onComplete += () => SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
       } 
    }

    public void StartAnimations() {
        time = 0;
        hasStarted = true;
    }

    private float GetTimeInSecond(float minute) {
        return minute * 60;
    }
}