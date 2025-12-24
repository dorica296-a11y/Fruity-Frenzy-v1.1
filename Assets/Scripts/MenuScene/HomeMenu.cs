using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class HomeMenu : MonoBehaviour
{
    private void Start()
    {
        
    }

    public void Play()
    {
        AudioManager.Instance.PlaySFX(AudioManager.Instance.clickEffect);
        SceneManager.LoadScene("LevelSelection");
    }

    public void Options()
    {
        AudioManager.Instance.PlaySFX(AudioManager.Instance.clickEffect);
    }
        
    public void Shop()
    {
        AudioManager.Instance.PlaySFX(AudioManager.Instance.clickEffect);
        SceneManager.LoadScene("ShopScene");
    }

    public void Quit()
    {
        AudioManager.Instance.PlaySFX(AudioManager.Instance.clickEffect);
        Application.Quit();
    }

    public void BackBtn()
    {
        AudioManager.Instance.PlaySFX(AudioManager.Instance.clickEffect);
    }
}
