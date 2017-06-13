using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class GooglePlayController : MonoBehaviour
{
    //Score id = "CgkImPGFh-ocEAIQAQ";

    private void Awake ()
    {
        PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder().Build();

        PlayGamesPlatform.DebugLogEnabled = true;
        PlayGamesPlatform.Activate();
    }

    private void Start()
    {
        Authenticate();
        GooglePopup();
    }

    public void Authenticate()
    {
        Social.localUser.Authenticate((bool success) => {
            if (success)
            {
                Debug.LogWarning("Google Play Authenticated!");
            }
            else
            {
                Debug.LogError("Authentication Error!");
            }
        });
    }

    public void GooglePopup()
    {
        Social.localUser.Authenticate((bool success) =>
        {
            if (success)
            {
                ((GooglePlayGames.PlayGamesPlatform)Social.Active).SetGravityForPopups(Gravity.BOTTOM);
            }
        });
    }

    public void SignOut()
    {
        PlayGamesPlatform.Instance.SignOut();
    }

    public void GetStats()
    {
        ((PlayGamesLocalUser)Social.localUser).GetStats((rc, stats) =>
        {
            if (rc <= 0 && stats.HasDaysSinceLastPlayed())
            {
                Debug.Log("It has been " + stats.DaysSinceLastPlayed + " days");
            }
        });
    }

    public void ShowLeaderboard()
    {
        Social.ShowLeaderboardUI();
    }

    public void ShowAchievements()
    {
        Social.ShowAchievementsUI();
    }

    public void SetScore(string id,int score)
    {
        Social.ReportScore(score, id, (bool success) => {
            if (success)
            {
                Debug.LogWarning("Score Reported! " + score);
            }
            else
            {
                Debug.LogError("Error on report score. " + score);
            }
        });
    }

    public void AddAchievementProgress(string id, int progress)
    {
        PlayGamesPlatform.Instance.IncrementAchievement(
        id, progress, (bool success) => {
            if (success)
            {
                Debug.LogWarning("Achievement progress added! " + progress);
            }
            else
            {
                Debug.LogError("Error on set achievement progress.");
            }
        });
    }

    public void UnlockAchievement(string id)
    {
        Social.ReportProgress(id, 100.0f, (bool success) => {
            if (success)
            {
                Debug.LogWarning("Achievement Unlocked!");
            }
            else
            {
                Debug.LogError("Error on unlock achievement.");
            }
        });
    }
}
