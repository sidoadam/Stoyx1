using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Adventure1_8: MonoBehaviour {

	public MediaPlayerCtrl adminVideo;
	public MediaPlayerCtrl chapterVideo;
	public TakePhotoController photoController;
	public GameObject mainUI;
	public GameObject physicalMissionScreen;
	// Use this for initialization
	void Start () {
		physicalMissionScreen.SetActive (false);
		adminVideo.OnEnd += onAdminVideoEnded;
		chapterVideo.OnEnd += onChapterVideoEnded;
	}

	void onAdminVideoEnded()
	{
		adminVideo.OnEnd -= onAdminVideoEnded;
		mainUI.SetActive (true);
		physicalMissionScreen.SetActive (true);
	
	}

	void disableAdminVideo()
	{
		adminVideo.gameObject.SetActive (false);
	}

	void playChapterVideo()
	{
		adminVideo.gameObject.SetActive (false);
		chapterVideo.Play ();
	}

	void enableNextBtn()
	{
		physicalMissionScreen.SendMessage ("enableNextBtn");
	}

	public void onChapterVideoEnded()
	{
		chapterVideo.OnEnd -= onChapterVideoEnded;
		SceneManager.LoadScene ("Adventure1_9");
	}

	// Update is called once per frame
	void Update () {
		
	}
}
