using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class Highscores : MonoBehaviour
{
    const string privateCode = "-ey9Q1Rb30iYMlHyhCKFMQcvDG_k3YbUWOrQIoOBSkmw";
    const string publicCode = "5fce89e6eb36fd27145cf4fd";
    const string webURL = "http://dreamlo.com/lb/";

    public event Action<Highscore[]> OnDownloadDone = delegate { };

    public Highscore[] highscoreList;

    public void AddNewHighscore(string username, int score)
    {
        StartCoroutine(UploadNewHighscore(username, score));
    }

    public IEnumerator UploadNewHighscore(string username, int score)
    {
        UnityWebRequest www = new UnityWebRequest(webURL + privateCode + "/add/" + UnityWebRequest.EscapeURL(username) + "/" + score);
        www.SendWebRequest();
        yield return www;

        if (www.isNetworkError)
        {
            Debug.LogError("Error Uploading " + www.error);
        }
        else
        {
            print("Upload Successful");
            DownloadHighscores();
        }
    }

    public void DownloadHighscores()
    {
        StartCoroutine(DownloadHighscoresFromDatabase());
    }

    public IEnumerator DownloadHighscoresFromDatabase()
    {
        UnityWebRequest www = new UnityWebRequest(webURL + publicCode + "/pipe/");
        www.downloadHandler = new DownloadHandlerBuffer();
        yield return www.SendWebRequest();

        if (www.isNetworkError)
        {
            Debug.LogError("Error Uploading " + www.error);
        }
        else
        {
            FormatHighscores(www.downloadHandler.text);
        }
        OnDownloadDone(highscoreList);
    }

    private void FormatHighscores(string textStream)
    {
        string[] entries = textStream.Split(new char[] { '\n' }, System.StringSplitOptions.RemoveEmptyEntries);
        highscoreList = new Highscore[entries.Length];

        for (int i = 0; i < entries.Length; i++)
        {
            string[] entryInfo = entries[i].Split(new char[] { '|' });
            string username = entryInfo[0];
            int score = int.Parse(entryInfo[1]);

            highscoreList[i] = new Highscore(username, score);
        }
    }
}

public struct Highscore
{
    public string username;
    public int score;

    public Highscore(string name, int scorr)
    {
        username = name;
        score = scorr;
    }
}