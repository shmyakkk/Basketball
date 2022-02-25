using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private GameObject StartedScreen;

    [SerializeField] private TextMeshProUGUI bestScoreText;
    private int bestScore = 0;


    private void Start()
    {
        SaveDataManager.Instance.LoadData();
        bestScore = SaveDataManager.Instance.bestScore;
        bestScoreText.text = "score " + bestScore;
    }
    public void ToGame()
    {
        SceneManager.LoadScene(1);
    }
}
