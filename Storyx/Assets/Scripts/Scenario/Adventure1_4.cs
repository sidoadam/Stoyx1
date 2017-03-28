using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Adventure1_4 : MonoBehaviour {

	public MediaPlayerCtrl adminVideo;
	public MediaPlayerCtrl adminVideo2;
	public MediaPlayerCtrl adminVideo3;
	public MediaPlayerCtrl chapterVideo;
	public TakePhotoController photoController;
	public GameObject mainUI;
	public GameObject physicalMissionScreen;
	public AudioSource missionSFX;

	public GameObject pauseUI;
	// Use this for initialization
	void Start () {
		physicalMissionScreen.SetActive (false);
		adminVideo.OnEnd += onAdminVideoEnded;
		adminVideo2.OnEnd += onAdminVideo2Ended;
		adminVideo3.OnEnd += onAdminVideo3Ended;
		chapterVideo.OnEnd += onChapterVideoEnded;

		//adminVideo3.Load ("putitback.mp4");

		adminVideo2.gameObject.SetActive (false);
		adminVideo3.gameObject.SetActive (false);
		//chapterVideo.gameObject.SetActive (false);

		pauseUI.GetComponent <PauseController> ().currentVideo = adminVideo;
	}

	void onAdminVideoEnded()
	{
		adminVideo.Stop ();
		adminVideo.UnLoad ();
		adminVideo.OnEnd -= onAdminVideoEnded;
		mainUI.SetActive (true);
		physicalMissionScreen.SetActive (true);

		pauseUI.SetActive (false);

		//playAdminVideo3 ();
		mainUI.SetActive (true);
		missionSFX.Play ();
	}

	void onAdminVideo2Ended()
	{
		if (adminVideo2.m_strFileName == "putitback.mp4") {
			mainUI.SetActive (true);
			enableNextBtn ();

		} else {
			adminVideo2.Stop ();
			adminVideo2.UnLoad ();
			//adminVideo2.gameObject.SetActive (false);
			adminVideo2.OnEnd -= onAdminVideo2Ended;

			mainUI.SetActive (true);

			adminVideo2.Load ("putitback.mp4");

			pauseUI.SetActive (false);

			photoController.GetImageFromCamera ();
		}


	}


	void onAdminVideo3Ended()
	{
		adminVideo3.Stop ();
		adminVideo3.UnLoad ();
		adminVideo3.OnEnd -= onAdminVideo3Ended;
		adminVideo3.gameObject.SetActive (false);

		pauseUI.SetActive (false);

		mainUI.SetActive (true);
		missionSFX.Play ();
		photoController.GetImageFromCamera ();
	}

	void disableAdminVideo()
	{
		adminVideo.gameObject.SetActive (false);
	}

	void playInputVideo()
	{
		adminVideo2.OnEnd += enableNextBtn;
		mainUI.SetActive (false);
		adminVideo2.Play ();
	}

	void playStopChapter()
	{
		adminVideo.gameObject.SetActive (false);
		StartCoroutine ("onPlayPause");
	}

	IEnumerator onPlayPause()
	{
		chapterVideo.Play ();
		yield return new WaitWhile (() =>  chapterVideo.GetSeekPosition() < 100);
		//while(chapterVideo.GetCurrentState() != MediaPlayerCtrl.MEDIAPLAYER_STATE.PLAYING)
		//yield return null;

		chapterVideo.Pause ();
	}

	void disableAdminVideo2()
	{
		adminVideo2.gameObject.SetActive (false);
	}

	public void playAdminVideo3()
	{
		mainUI.SetActive (false);
		adminVideo3.gameObject.SetActive (true);
		//disableAdminVideo ();
		adminVideo3.Play ();

		pauseUI.SetActive (true);
		pauseUI.GetComponent <PauseController> ().currentVideo = adminVideo3;
	}



	void playAdminVideo2()
	{
		missionSFX.Stop ();
		mainUI.SetActive (false);
		adminVideo.gameObject.SetActive (false);
		adminVideo2.gameObject.SetActive (true);
		disableAdminVideo ();
		adminVideo2.Play ();

		pauseUI.SetActive (true);
		pauseUI.GetComponent <PauseController> ().currentVideo = adminVideo2;
	}

	void playChapterVideo()
	{
		adminVideo2.gameObject.SetActive (false);
		chapterVideo.gameObject.SetActive (true);
		disableAdminVideo ();
		chapterVideo.Play ();

		pauseUI.SetActive (true);
		pauseUI.GetComponent <PauseController> ().currentVideo = chapterVideo;
	}

	void enableNextBtn()
	{
		mainUI.SetActive (true);
		physicalMissionScreen.SendMessage ("enableNextBtn");
		physicalMissionScreen.SendMessage ("disableCameraIcon");
	}

	public void onChapterVideoEnded()
	{
		chapterVideo.OnEnd -= onChapterVideoEnded;

		Resources.UnloadUnusedAssets ();
		System.GC.Collect ();

		SceneManager.LoadScene ("Adventure1_5");
	}

	// Update is called once per frame
	void Update () {
		
	}
}
