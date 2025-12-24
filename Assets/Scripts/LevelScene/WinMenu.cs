using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinMenu : MonoBehaviour
{
    [SerializeField] private string nextLevelSceneName;

    public void LevelSelection()
    {
        AudioManager.Instance.PlaySFX(AudioManager.Instance.clickEffect);
        AudioManager.Instance.ChangeMusic(AudioManager.Instance.backgroundMusic);
        SceneManager.LoadScene("LevelSelection");
    }
        
    public void Restart()
    {
        AudioManager.Instance.PlaySFX(AudioManager.Instance.clickEffect);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void NextLevel()
    {
        AudioManager.Instance.PlaySFX(AudioManager.Instance.clickEffect);
        AudioManager.Instance.RestartMusic();
        SceneManager.LoadScene(nextLevelSceneName);
    }
}
