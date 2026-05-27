using UnityEngine;

public class UIButtonSound : MonoBehaviour
{
    public AudioSource clickSound;

    public void PlayClickSound()
    {
        clickSound.PlayOneShot(clickSound.clip);
    }
}