using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    [SerializeField]
    private AudioClip[] audioClips;
    [SerializeField]
    private ParticleSystem particleSystem;

    private AudioSource audioSource;
    private AudioClip currentClip;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        currentClip = audioClips[0];
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Discharge();
        }
    }
    private void Discharge()
    {
        audioSource.PlayOneShot(currentClip);
        particleSystem.Play();
        StartCoroutine("Wait");
    }
    private IEnumerator Wait()
    {
        yield return new WaitForSeconds(0.1f);
        particleSystem.Stop();
        GameManager.Instance.Win();

    }
}
