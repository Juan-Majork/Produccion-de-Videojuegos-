using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicPlayer : MonoBehaviour
{
    [SerializeField] private AudioClip musicClipLevel;
    [SerializeField] private AudioClip musicClipArena;
    private AudioSource musicSource;

    public static MusicPlayer musicPlayer;

    void Awake()
    {
        if (MusicPlayer.musicPlayer == null)
        {
            MusicPlayer.musicPlayer = this;
        }
        else
        {
            Destroy(gameObject);
        }

        musicSource = GetComponent<AudioSource>();

        if (SceneManager.GetActiveScene().name == "SampleScene")
        {
            LevelMusic();
        }
    }

    public void BattleMusic()
    {
        musicSource.clip = musicClipArena;
        musicSource.loop = true;
        musicSource.Play();
    }

    public void LevelMusic()
    {
        musicSource.clip = musicClipLevel;
        musicSource.loop = true;
        musicSource.Play();
    }
}
