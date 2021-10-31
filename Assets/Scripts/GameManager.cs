using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;
    private int gameTime = 10;
    private int score = 0;

    private void Start()
    {
        //StartCoroutine(Countdown());
    }
    IEnumerator Countdown()
    {
        while (gameTime > 0)
        {
            yield return new WaitForSeconds(1);
            gameTime--;
        }
        if (gameTime == 0)
        {
            GameOver();
        }
    }
    private void GameOver()
    {
        SceneManager.LoadScene(0);
    }

    public void ScoreGame()
    {
        score++;
        scoreText.text = "Score: " + score;
    }
}
