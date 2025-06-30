using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicPlayer : MonoBehaviour
{
    [SerializeField] private AudioClip musicClipLevel;
    [SerializeField] private AudioClip musicClipMenu;
    [SerializeField] private AudioClip musicClipArena;
    [SerializeField] private AudioClip musicCliBoss;
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

        if (SceneManager.GetActiveScene().name == "menu")
        {
            MenuMusic();
        }

        if (SceneManager.GetActiveScene().name == "level1" || SceneManager.GetActiveScene().name == "transicion")
        {
            LevelMusic();
        }

        if (SceneManager.GetActiveScene().name == "boss1")
        {
            BossMusic();
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

    public void BossMusic()
    {
        musicSource.clip = musicCliBoss;
        musicSource.loop = true;
        musicSource.Play();
    }

    public void MenuMusic()
    {
        musicSource.clip = musicClipMenu;
        musicSource.loop = true;
        musicSource.Play();
    }

    public void StopMusic()
    {
        musicSource.Stop();
    }
}
