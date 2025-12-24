using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] GameObject pauseMenu;
    private void Start()
    {
        
    }

    public void Pause()
    {
        AudioManager.Instance.PlaySFX(AudioManager.Instance.clickEffect);
        pauseMenu.SetActive(true);
        Time.timeScale = 0;
    }

    public void Home()
    {
        AudioManager.Instance.PlaySFX(AudioManager.Instance.clickEffect);
        AudioManager.Instance.ChangeMusic(AudioManager.Instance.backgroundMusic);
        SceneManager.LoadScene("LevelSelection");
        Time.timeScale = 1;
    }
        
    public void resume()
    {
        AudioManager.Instance.PlaySFX(AudioManager.Instance.clickEffect);
        pauseMenu.SetActive(false);
        Time.timeScale = 1;
    }
        
    public void Restart()
    {
        AudioManager.Instance.PlaySFX(AudioManager.Instance.clickEffect);
        AudioManager.Instance.RestartMusic();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1;
    }

    public void Settings()
    {
        AudioManager.Instance.PlaySFX(AudioManager.Instance.clickEffect);
    }

}
