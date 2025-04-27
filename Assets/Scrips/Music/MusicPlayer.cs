using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicPlayer : MonoBehaviour
{
    [SerializeField] private AudioClip musicClipLevel;
    private AudioSource musicSource;

    void Start()
    {
        musicSource = GetComponent<AudioSource>();

        if (SceneManager.GetActiveScene().name == "SampleScene")
        {
            LevelMusic();
        }
    }

    public void LevelMusic()
    {
        musicSource.clip = musicClipLevel;
        musicSource.loop = true;
        musicSource.Play();
    }
}
