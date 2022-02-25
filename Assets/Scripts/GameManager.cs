using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using DragonBones;

public class GameManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI bestScore;

    [SerializeField] private GameObject throwsManager;

    [SerializeField] private GameObject timeline;

    [SerializeField] private GameObject best;

    [SerializeField] private GameObject menu;
    [SerializeField] private GameObject close;

    [SerializeField] private GameObject StartedScreen;
    [SerializeField] private GameObject GameScreen;
    [SerializeField] private GameObject MenuScreen;

    [SerializeField] private Material wallMaterial;
    [SerializeField] private Material ballMaterial;

    private bool isGameStarted = false;

    private int gameTime = 30;
    private float timelineScaleX;

    private int score = 0;

    public static float mod;
    [SerializeField] private Color defaultWallColor;
    [SerializeField] private Color defaultBallColor;

    private void Awake()
    {
        mod = Screen.width / (float)Screen.height;
    }
    private void Start()
    {
        SaveDataManager.Instance.LoadData();
        SetColors();

        if (SaveDataManager.Instance.bestScore > 0)
        {
            best.SetActive(true);
            bestScore.text = SaveDataManager.Instance.bestScore.ToString();
        }
        scoreText.text = "";
        timelineScaleX = timeline.transform.localScale.x / gameTime;
    }
    private void Update()
    {
        ChangeTimeScale();
    }
    private void SetColors()
    {
        wallMaterial.color = SaveDataManager.Instance.wallColor == Color.gray ? defaultWallColor : SaveDataManager.Instance.wallColor;
        ballMaterial.color = SaveDataManager.Instance.ballColor == new Color(0, 0, 0, 0) ? defaultBallColor : SaveDataManager.Instance.ballColor;
    }
    public void GoToMenu()
    {
        menu.GetComponent<UnityArmatureComponent>().animation.Play("animtion0", 1);
        Invoke(nameof(SetActiveMenu), 0.7f);
    }
    private void SetActiveMenu()
    {
        MenuScreen.SetActive(true);
        StartedScreen.SetActive(false);
    }
    public void GoToStartedScreen()
    {
        SetColors();
        close.GetComponent<UnityArmatureComponent>().animation.Play("animtion0", 1);
        Invoke(nameof(SetActiveStartedScreen), 0.7f);
    }
    private void SetActiveStartedScreen()
    {
        MenuScreen.SetActive(false);
        StartedScreen.SetActive(true);
    }
    public void StartGame()
    {
        StartedScreen.SetActive(false);
        GameScreen.SetActive(true);
        score = 0;
        isGameStarted = true;
        scoreText.text = score.ToString();
        throwsManager.SetActive(true);
        StartCoroutine(CountdownGame());
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
    private void ChangeTimeScale()
    {
        if (isGameStarted && timeline.transform.localScale.x > 0)
        {
            timeline.transform.localScale += Time.deltaTime * timelineScaleX * Vector3.left;
        }
    }

    public void ScoreGame()
    {
        score++;
        scoreText.text = score.ToString();
    }
    private void GameOver()
    {
        if (score > SaveDataManager.Instance.bestScore)
        {
            SaveDataManager.Instance.bestScore = score;
        }
        SaveDataManager.Instance.SaveData();
        SceneManager.LoadScene("Game");
    }
}
