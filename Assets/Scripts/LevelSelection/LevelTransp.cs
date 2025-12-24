using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelTransp : MonoBehaviour
{
    public static AudioManager Instance;

    private void Start()
    {
        
    }

    public void Level1()
    {
        AudioManager.Instance.ChangeMusic(AudioManager.Instance.stage1Music);
        SceneManager.LoadScene("Level1Scene");
    }
        
    public void Level2()
    {
        AudioManager.Instance.ChangeMusic(AudioManager.Instance.stage1Music);
        SceneManager.LoadScene("Level2Scene");
    }

    public void Level3()
    {
        AudioManager.Instance.ChangeMusic(AudioManager.Instance.stage1Music);
        SceneManager.LoadScene("Level3Scene");
    }

    public void Level4()
    {
        AudioManager.Instance.ChangeMusic(AudioManager.Instance.stage1Music);
        SceneManager.LoadScene("Level4Scene");
    }

    public void Level5()
    {
        AudioManager.Instance.ChangeMusic(AudioManager.Instance.stage1Music);
        SceneManager.LoadScene("Level5Scene");
    }

    public void Level6()
    {
        AudioManager.Instance.ChangeMusic(AudioManager.Instance.stage1Music);
        SceneManager.LoadScene("Level6Scene");
    }

    public void BackBtn()
    {
        AudioManager.Instance.PlaySFX(AudioManager.Instance.clickEffect);
        SceneManager.LoadScene("MenuScene");
    }
}
