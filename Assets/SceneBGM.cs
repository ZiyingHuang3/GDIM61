using UnityEngine;

public class SceneBGM : MonoBehaviour
{
    public AudioClip firstBGM;
    public AudioClip secondBGM;

    public GameObject success;
    public GameObject failed;

    private AudioSource audioSource;

    static bool hasVisited = false;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        if (!hasVisited)
        {
            audioSource.clip = firstBGM;
            hasVisited = true;
        }
        else
        {
            audioSource.clip = secondBGM;
        }

        audioSource.Play();
    }

    public void ShowSuccess()
    {
        audioSource.Stop();

        success.SetActive(true);

        success.GetComponent<AudioSource>().Play();
    }

    public void ShowFailed()
    {
        audioSource.Stop();

        failed.SetActive(true);

        failed.GetComponent<AudioSource>().Play();
    }
}