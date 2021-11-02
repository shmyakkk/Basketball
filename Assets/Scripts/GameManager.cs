using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private GameObject timeline;
    private float timelineScaleX;

    [SerializeField] private GameObject hoop;
    private GameObject hoopPrefab;
    private Vector3 hoopPos;

    private int gameTime = 59;
    private int score = 0;

    public static bool isStartInMainMenu;

    private void Start()
    {
        if (isStartInMainMenu)
        {
            Camera.main.backgroundColor = SaveDataManager.Instance.color;
        }
        timelineScaleX = timeline.transform.localScale.x / gameTime;
        StartCoroutine(CountdownGame());
        StartCoroutine(CountdownHoop());
    }
    private void Update()
    {
        ChangeTimeScale();
    }
    IEnumerator CountdownGame()
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
    void ChangeTimeScale()
    {
        if (timeline.transform.localScale.x > 0)
        {
            timeline.transform.localScale += Time.deltaTime * timelineScaleX * Vector3.left;
        }
    }
    IEnumerator CountdownHoop()
    {
        InstantiateHoop();
        yield return new WaitForSeconds(10);
        Destroy(hoopPrefab);
        StartCoroutine(CountdownHoop());
    }
    private void InstantiateHoop()
    {
        float z = Random.Range(-1, 2);

        float maxX = (2 + z) / 4.0f;
        float x = Random.Range(-maxX, maxX);

        float maxY = (1.5f * z + 9) / 5.0f;
        float y = Random.Range(-maxY, maxY);

        hoopPos = new Vector3(x, y, z);
        hoopPrefab = Instantiate(hoop, hoopPos, Quaternion.Euler(0, Random.Range(-30, 30), 0));
    }
    private void GameOver()
    {
        if (isStartInMainMenu)
        {
            if (score > SaveDataManager.Instance.bestScore)
            {
                SaveDataManager.Instance.bestScore = score;
                SaveDataManager.Instance.SaveData();
            }
        }
        SceneManager.LoadScene(0);
    }

    public void ScoreGame()
    {
        score++;
        scoreText.text = "Score: " + score;
    }
}
