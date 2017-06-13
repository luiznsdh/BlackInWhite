using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Facebook.Unity;

//Code: http://greyzoned.com/
//Modifications to this app;

public class FacebookManager : MonoBehaviour
{
	public Text textLogged;

	void Awake()
	{
		FB.Init (SetInit, OnHideUnity);
	}

	private void SetInit()
	{
		if(FB.IsLoggedIn)
		{
			Debug.Log ("FB Logged In");
        }
        else
        {
        }
	}

    void OnHideUnity(bool isGameShown)
    {

        if (!isGameShown)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }

    }

    public void FBlogin()
	{
		FB.LogInWithPublishPermissions(new List<string>() { "email", "publish_actions", "user_friends" }, AuthCallback);
	}

	void AuthCallback(IResult result)
	{
		if(FB.IsLoggedIn)
        {
			Debug.Log ("FB login worked!");
            textLogged.text = "Connected";
            GameObject.Find("GooglePlay").GetComponent<GooglePlayController>().UnlockAchievement("CgkImPGFh-ocEAIQAw");
            //shareBtn.SetActive(true);
            //GetUserInfo();
        }
        else
        {
			Debug.Log ("FB Login fail");
            textLogged.text = "Not Connected";
            //shareBtn.SetActive(false);
        }
	}

    public void ShareWithFriends()
    {
        FB.ShareLink(
            new Uri("https://play.google.com/store/apps/details?id=com.fourhorizons.blackinwhite"),
            callback: ShareCallback
        );
    }

    private void ShareCallback(IShareResult result)
    {
        if (result.Cancelled || result.Error != null)
        {
            Debug.Log("ShareLink Error: " + result.Error);
        }
        else if (result.PostId != null)
        {
            Debug.Log(result.PostId);
        }
        else
        {
            Debug.Log("ShareLink success!");
        }
    }

    public void InviteFriends()
	{
		FB.AppRequest(
			message: "This game is awesome, join me. now.",
			title: "Invite your friends to join you"
			);
	}
}


