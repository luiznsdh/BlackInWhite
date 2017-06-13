using UnityEngine;
using UnityEngine.UI;
using GoogleMobileAds.Api;
using System;

public class AdMobManager : MonoBehaviour
{
    private RewardBasedVideoAd rewardBasedVideoAd;

    public GameObject googlePlay;
    public Text videoText;

    private void Start()
    {
        rewardBasedVideoAd = RewardBasedVideoAd.Instance;

        rewardBasedVideoAd.OnAdClosed += HandleOnAdClosed;
        rewardBasedVideoAd.OnAdFailedToLoad += HandleOnAdFailedToLoad;
        rewardBasedVideoAd.OnAdLeavingApplication += HandleOnAdLeavingApplicaiton;
        rewardBasedVideoAd.OnAdLoaded += HandleOnAdLoaded;
        rewardBasedVideoAd.OnAdOpening += HandleOnAdOpening;
        rewardBasedVideoAd.OnAdRewarded += HandleOnAdRewarded;
        rewardBasedVideoAd.OnAdStarted += HandleOnAdStarted;
    }

    public void ShowRewardBasedAd()
    {
        if (rewardBasedVideoAd.IsLoaded())
        {
            videoText.text = "Rewarded Ads";
            rewardBasedVideoAd.Show();
        }
        else
        {
            videoText.text = "No rewards avaliable";
        }
    }

    public void LoadRewardBaseAd()
    {
#if UNITY_EDITOR
        string adUnitId = "unused";
#elif UNITY_ANDROID
        string adUnitId = "ca-app-pub-2004530989937961/8711624934";
#elif UNITY_IPHONE
        string adUnitId = "ca-app-pub-2004530989937961/8711624934";
#else
        string adUnitId = "unexpected_platform";
#endif

        rewardBasedVideoAd.LoadAd(new AdRequest.Builder().Build(), adUnitId);
    }

    public event EventHandler<EventArgs> OnAdLoaded;

    public event EventHandler<AdFailedToLoadEventArgs> OnAdFailedToLoad;

    public event EventHandler<EventArgs> OnAdOpening;

    public event EventHandler<EventArgs> OnAdStarted;

    public event EventHandler<EventArgs> OnAdClosed;

    public event EventHandler<EventArgs> OnAdRewarded;

    public event EventHandler<EventArgs> OnAdLeavingApplication;

    public void HandleOnAdLoaded(object sender, EventArgs args)
    {

    }

    public void HandleOnAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
    {
        Debug.Log("Error to load ad");
    }

    public void HandleOnAdOpening(object sender, EventArgs args)
    {
        // Pause the action.
    }

    public void HandleOnAdStarted(object sender, EventArgs args)
    {
        // Mute Audio.
    }

    public void HandleOnAdClosed(object sender, EventArgs args)
    {
        // Crank the party back up.
    }

    public void HandleOnAdRewarded(object sender, EventArgs args)
    {
        googlePlay.GetComponent<GooglePlayController>().UnlockAchievement("CgkImPGFh-ocEAIQBA");
    }

    public void HandleOnAdLeavingApplicaiton(object sender, EventArgs args)
    {

    }
}
