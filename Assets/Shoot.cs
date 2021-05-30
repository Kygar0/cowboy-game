using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Shoot : MonoBehaviour
{
    [SerializeField]                          // De här kan man dra in i Unity och använder vi sen i koden.
    private AudioClip[] audioClips;
    [SerializeField]
    private ParticleSystem particleSystem;

    private AudioSource audioSource;
    private AudioClip currentClip;

    private bool shotten = false;             // Används när vi testar om man har skjutit efter snubben dragit strykjärn!

    private void Awake()                      // Vi kallar in ljudet från audioclips.
    {
        audioSource = GetComponent<AudioSource>();
        currentClip = audioClips[0];
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)&&shotten==false) // Om någon trycker på så ska det skjutas om man inte redan gjort det efter strykjärnet dragits.
        {
            if (GameManager.Instance.elapsedTime >= GameManager.Instance.rand_num) // Vi tittar så att man inte kan skjuta innan strykjärnet dragits.
            {
                Discharge();
                shotten = true;
            }
        }
        if (Input.GetKeyDown(KeyCode.Escape)) // Om man trycker escape går man tillbaka till main menyn.
        {
            SceneManager.LoadScene("MenuScene");
        }
    }
    private void Discharge() // Spelar upp ljudeffekter och particleeffects när man skjuter.
    {
        audioSource.PlayOneShot(currentClip);
        particleSystem.Play();
        StartCoroutine("Wait");
    }
    private IEnumerator Wait() // Väntar in particles och startar win metoden som kallas i GameManager.cs.
    {
        yield return new WaitForSeconds(0.1f);
        particleSystem.Stop();
        GameManager.Instance.Win();

    }
}
