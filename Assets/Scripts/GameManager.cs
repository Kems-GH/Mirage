using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public bool gameOver = false;
    
    [SerializeField] private TMPro.TextMeshProUGUI scoreText;
    [SerializeField] private GameObject menuGameOver;

    private int score = 0;

    public void AddScore(int scoreToAdd)
    {
        score += scoreToAdd;
        scoreText.text = "Score: " + score;
    }

    public void GameOver()
    {
        gameOver = true;

        GameObject[] monsters = GameObject.FindGameObjectsWithTag("Monster");
        foreach (GameObject monster in monsters)
        {
            Destroy(monster);
        }

        menuGameOver.SetActive(true);
    }
}
