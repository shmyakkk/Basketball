using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;
using TMPro;

public class AdsManager : MonoBehaviour, IUnityAdsListener
{
    private string gameID = "4639637";

    [SerializeField] GameManager gameManager;

    private int addedBonus;

    private void Start()
    {
        Advertisement.Initialize(gameID);
        Advertisement.AddListener(this);
    }

    public void PlayRewardedVideoAddBonus()
    {
        if (Advertisement.IsReady("rewardedVideoAddBonus"))
        {
            SaveDataManager.Instance.Bonus += 50;

            Advertisement.Show("rewardedVideoAddBonus");
        }
    }

    public void PlayRewardedVideoDoubleBonus()
    {
        if (Advertisement.IsReady("rewardedVideoDoubleBonus"))
        {
            SaveDataManager.Instance.Bonus += (gameManager.Score * 5);

            Advertisement.Show("rewardedVideoDoubleBonus");
        }
    }

    public void OnUnityAdsReady(string placementId)
    {
        Debug.Log("ADS ARE READY!");
    }

    public void OnUnityAdsDidError(string message)
    {
        Debug.Log("ERROR: " + message);
    }

    public void OnUnityAdsDidStart(string placementId)
    {
        Debug.Log("VIDEO STARTED");
        gameManager.AdStarted = true;
    }

    public void OnUnityAdsDidFinish(string placementId, ShowResult showResult)
    {
        gameManager.AdStarted = false;

        if (showResult == ShowResult.Finished)
        {
            gameManager.ChangeBonusValue();

            SaveDataManager.Instance.SaveData();
        }
    }
}
