using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EventManager : MonoBehaviour
{

    private float time;
    private bool hasStarted;

    [SerializeField] private GameObject forest, lac, balloonSpawner, misc, building, firefly, firefly2, firefly3; //fire;
    [SerializeField] private Image fadeImage;

    [SerializeField, Range(0, 5)]
    private float forestTime, lacTime, balloonTime, miscTime, fireflyTime, firefly2Time, firefly3Time;// fireTime;
    private float maxTime;
    private AudioSource _audioSource;

    public static EventManager current;

    public event EventHandler AnimationStarted;

    private void Awake() {
        current = this;
    }

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
        if (time >= GetTimeInSecond(fireflyTime) && !firefly.activeSelf) firefly.SetActive(true);
        if (time >= GetTimeInSecond(firefly2Time) && !firefly2.activeSelf) firefly2.SetActive(true);
        if (time >= GetTimeInSecond(firefly3Time) && !firefly3.activeSelf) firefly3.SetActive(true);
      //  if (time >= GetTimeInSecond(fireTime) && !fire.activeSelf) fire.SetActive(true);
        if (time >= maxTime) {
           time = 0;
           fadeImage.DOFade(1, 2).onComplete += () => SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
       } 
    }

    public void StartAnimations() {
        time = 0;
        hasStarted = true;
        AnimationStarted?.Invoke(this, EventArgs.Empty);
    }

    private float GetTimeInSecond(float minute) {
        return minute * 60;
    }
}