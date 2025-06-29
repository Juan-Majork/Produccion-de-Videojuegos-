using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioPlayer : MonoBehaviour
{
    [SerializeField] private AudioClip pickItemSound;
    private AudioSource audioSource;

    public static AudioPlayer audioPlayer;

    void Awake()
    {
        if (AudioPlayer.audioPlayer == null)
        {
            AudioPlayer.audioPlayer = this;
        }
        else
        {
            Destroy(gameObject);
        }

        audioSource = GetComponent<AudioSource>();
    }

    public void PickItem()
    {
        audioSource.PlayOneShot(pickItemSound);
    }
}
