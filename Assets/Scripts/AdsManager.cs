using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;
using TMPro;

public class AdsManager : MonoBehaviour, IUnityAdsListener
{
    private string gameID = "4639637";
    [SerializeField] private TextMeshProUGUI bonusOption;
    [SerializeField] private TextMeshProUGUI bonusMenu;
    [SerializeField] private TextMeshProUGUI bonusGame;


    private void Start()
    {
        Advertisement.Initialize(gameID);
        Advertisement.AddListener(this);
    }

    public void PlayRewardedAd()
    {
        if (Advertisement.IsReady("rewardedVideo"))
        {
            Advertisement.Show("rewardedVideo");
        }
        else
        {
            Debug.Log("Rewarded ad is not ready!");
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
    }

    public void OnUnityAdsDidFinish(string placementId, ShowResult showResult)
    {
        if(placementId=="rewardedVideo" && showResult == ShowResult.Finished)
        {
            SaveDataManager.Instance.bonus += 100;
            Debug.Log(SaveDataManager.Instance.bonus);
            bonusGame.text = SaveDataManager.Instance.bonus.ToString();
            bonusOption.text = SaveDataManager.Instance.bonus.ToString();
            bonusMenu.text = SaveDataManager.Instance.bonus.ToString();

        }
    }
}
