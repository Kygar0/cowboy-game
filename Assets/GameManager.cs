using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Diagnostics;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField]               // De här kan man dra in i Unity och använder vi sen i koden.
    private GameObject iron;
    [SerializeField]
    private AudioClip[] audioClips;

    public GameObject arrowImage;   // De här kan man dra in i Unity och använder vi sen i koden.
    public GameObject goText;
    public GameObject waitText;
    public GameObject startButton;
    public GameObject rtText;
    public GameObject restartButton;
    public GameObject saveButton;

    private AudioSource audioSource;
    private AudioClip currentClip;

    bool drawnCheck = false; // Kollar om strykjärnet blivit draget.
    bool startCheck = true; // Kollar om man har klickat på startknappen och startat grejsimojsen.

    [HideInInspector] // Dessa måste vara public för att användas i andra script men inte så att man kan fiffla med de i Unity.
    public int elapsedTime;
    [HideInInspector]
    public int rand_num;

    Stopwatch stopWatch = new Stopwatch(); // Här skapar vi två separata stopwatches.
    Stopwatch stoopWatch = new Stopwatch();
    public static GameManager Instance; // Instance är för att vi ska kunna accessa denna kod i andra scripts.
    private void Awake()
    {
        if (Instance==null) // Och här sätter vi static field till denna kod.
        {
            Instance = this;
        }
        audioSource = GetComponent<AudioSource>();
        currentClip = audioClips[0];

        startButton.GetComponent<Button>().onClick.AddListener(Startation); // Här tittar vi om man klickat på knappen.
        restartButton.GetComponent<Button>().onClick.AddListener(Restartation);
    }
    private void Restartation() // Startar om scenen.
    {
        SceneManager.LoadScene("GameScene");
    }
    private void Startation() // Kör när man klickar på start knappen.
    {
        startButton.SetActive(false); // Tar bort startknappen från spelarens vy.

        System.Random rd = new System.Random(); // Vi skapar en random object.

        rand_num = rd.Next(1, 5); // Tar fram ett nummer mellan 1 och 4 för antalet sekunder som väntas innan strykjärnet dras.

        print(rand_num + " sec");

        stopWatch.Start(); // Startar klockan för att vänta antalet sekunder vi nyss bestämt.

        startCheck = false;
    }
    private void Update()
    {

        if (startCheck == false) // Kollar om man tryckt på startknappen.
        {
            waitText.SetActive(true); // Visar texten som ber en vänta på strykjärnet.
            TimeSpan ts = stopWatch.Elapsed; // Tar in hur lång tid som har gått i den första stopwatchen.
            elapsedTime = (int)ts.TotalSeconds; // Stoppar in hur långt det gått i sekunder.

            if (drawnCheck == false && elapsedTime >= rand_num) // Tittar om hela randoma tiden har gått och den inte blivit drawn.
            {
                drawnCheck = true;
                startCheck = true;
                stopWatch.Stop();
                SpriteRenderer sr = iron.GetComponent<SpriteRenderer>();
                sr.enabled = true;
                audioSource.PlayOneShot(currentClip);
                stoopWatch.Start(); // Startar klockan för att mäta reaktionstiden.
                waitText.SetActive(false);
                goText.SetActive(true); // Visar en stor visual reminder att strykjärnet är uppe och att man ska trycka.
                arrowImage.SetActive(true);
            }
        }
    }

    public void Win() // Aktiverar restart knappar och visar spelarens reaktionstid.
    {
        TimeSpan ts = stoopWatch.Elapsed;
        int elapsedTime = (int)ts.TotalMilliseconds;
        print("RT: " + elapsedTime+" ms");
        goText.SetActive(false);
        arrowImage.SetActive(false);
        rtText.SetActive(true);
        rtText.GetComponent<Text>().text = elapsedTime + " ms";
        restartButton.SetActive(true);
        saveButton.SetActive(true);
    }
}
