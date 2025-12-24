using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// 1. Tambahkan library ini untuk mengatur pindah scene/restart
using UnityEngine.SceneManagement; 

public class PlayerHealth : MonoBehaviour
{
    public int health;
    public int maxHealth = 3;

    public SpriteRenderer playerSr;
    public PlayerMovement2 playerMovement;
    
    void Start()
    {
        health = maxHealth;
    }

    public void TakeDamage(int amount)
    {
        health -= amount;
        if(health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        //destroy player
        Destroy(gameObject);

        //auto restart
        AudioManager.Instance.RestartMusic();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1;
    }

}

