using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Adventure1_9: MonoBehaviour {

	public MediaPlayerCtrl chapterVideo;
	public GameObject mainUI;
	public GameObject physicalMissionScreen;

	public GameObject pauseUI;
	// Use this for initialization
	void Start () {
		physicalMissionScreen.SetActive (false);
		chapterVideo.OnEnd += onChapterFinished;
		pauseUI.SetActive (false);
	}


	void onPlayChapter()
	{
		mainUI.SetActive (false);
		chapterVideo.Play ();
	}

	void onChapterFinished()
	{
		physicalMissionScreen.SetActive (true);
		mainUI.SetActive (true);
	}

	// Update is called once per frame
	void Update () {
		
	}
}
