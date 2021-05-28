using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Diagnostics;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private GameObject iron;
    [SerializeField]
    private AudioClip[] audioClips;

    public GameObject arrowImage;
    public GameObject goText;
    public GameObject waitText;
    public GameObject startButton;

    private AudioSource audioSource;
    private AudioClip currentClip;

    bool drawnCheck = false;
    bool startCheck = true;

    int rand_num;
    Stopwatch stopWatch = new Stopwatch();
    Stopwatch stoopWatch = new Stopwatch();
    public static GameManager Instance;
    private void Awake()
    {
        if (Instance==null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        audioSource = GetComponent<AudioSource>();
        currentClip = audioClips[0];

        startButton.GetComponent<Button>().onClick.AddListener(Startation);
    }

    private void Startation()
    {
        System.Random rd = new System.Random();

        rand_num = rd.Next(1, 5);

        print(rand_num + " sec");

        stopWatch.Start();

        startCheck = false;
    }
    private void Update()
    {
        if (startCheck == false)
        {
            startButton.SetActive(false);
            waitText.SetActive(true);
            TimeSpan ts = stopWatch.Elapsed;
            int elapsedTime = (int)ts.TotalSeconds;

            if (drawnCheck == false && elapsedTime >= rand_num)
            {
                drawnCheck = true;
                startCheck = true;
                elapsedTime = 0;
                stopWatch.Stop();
                SpriteRenderer sr = iron.GetComponent<SpriteRenderer>();
                sr.enabled = true;
                audioSource.PlayOneShot(currentClip);
                stoopWatch.Start();
                waitText.SetActive(false);
                goText.SetActive(true);
                arrowImage.SetActive(true);
            }
        }
    }

    public void Win()
    {
        TimeSpan ts = stoopWatch.Elapsed;
        int elapsedTime = (int)ts.TotalMilliseconds;
        print("RT: " + elapsedTime+" ms");
        goText.SetActive(false);
        arrowImage.SetActive(false);
    }
}
