using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HowToPlayController : MonoBehaviour {

	public GameObject mainUI;
	public MediaPlayerCtrl player;
	public GameObject how_to_play_btn;
	// Use this for initialization
	void Start () {
		Screen.sleepTimeout = SleepTimeout.NeverSleep;

		how_to_play_btn.SetActive (false);

		player.OnReady += onVideoReady;
		player.OnEnd += onVideoEnd;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void onVideoReady()
	{
		how_to_play_btn.SetActive (true);
		Debug.Log ("onVideoReady");
	}

	void onVideoEnd()
	{
		SceneManager.LoadScene ("SelectMode");
	}

	public void onStartPlay()
	{
		SceneManager.LoadScene ("SelectMode");
	}

	public void onInstraction()
	{
		player.Play ();
		mainUI.SetActive (false);
	}
}
