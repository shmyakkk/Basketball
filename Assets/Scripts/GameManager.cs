using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject hoop;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private GameObject timeline;
    private float timelineScaleX;

    private int gameTime = 10;
    private int score = 0;
    public static float mod = 1;

    void Start()
    {
        mod = (float)Screen.width / Screen.height;
        Debug.Log(mod);
        Instantiate(hoop);
        timelineScaleX = timeline.transform.localScale.x / gameTime;
        StartCoroutine(CountdownGame());
    }
    void Update()
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
            SceneManager.LoadScene(0);
        }
    }
    private void ChangeTimeScale()
    {
        if (timeline.transform.localScale.x > 0)
        {
            timeline.transform.localScale += Time.deltaTime * timelineScaleX * Vector3.left;
        }
    }
    public void ScoreGame()
    {
        score++;
        scoreText.text = score.ToString();
    }
}
