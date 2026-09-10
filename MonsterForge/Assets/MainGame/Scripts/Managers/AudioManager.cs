using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{ // this manages the audio for one single scene. Each scene will have a diff audioManager.

    public AudioSource audioSource;
    public AudioClip phase2;
    public AudioClip playerDeath;
    public bool toggleOneShot = false; // so that sounds dont feedback loop lol.
    public void Start()
    {
        
    }
    public void Update()
    {
        if (GameManager.Instance != null)
        {
            if (phase2 == null)
            {
                //nothing
            }
            else if (GameManager.Instance.bossPhase2 && phase2 != null)
            {
                audioSource.Stop();
                audioSource.clip = phase2;
                audioSource.Play();
                GameManager.Instance.bossPhase2 = false;
            }
            if (GameManager.Instance.playerDeath && !toggleOneShot)
            {
                toggleOneShot = true;
                audioSource.Stop();
                audioSource.clip = playerDeath;
                audioSource.Play();
            }
        }
        else
        {
            //eh
        }
        
    }
}
