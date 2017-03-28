using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Adventure1_2 : MonoBehaviour {

	public MediaPlayerCtrl adminVideo;
	public MediaPlayerCtrl chapterVideo;
	public TakePhotoController photoController;
	public GameObject mainUI;
	public GameObject physicalMissionScreen;

	public GameObject pauseUI;
	// Use this for initialization
	void Start () {
		physicalMissionScreen.SetActive (false);
		adminVideo.OnEnd += onAdminVideoEnded;
		chapterVideo.OnEnd += onChapterVideoEnded;
		//adminVideo.OnReady += onInitAdminVideo;
		chapterVideo.Load ("putitback.mp4");
		//Invoke ("onChapterVideoEnded",2);
		pauseUI.GetComponent <PauseController> ().currentVideo = adminVideo;
	}

	void onInitAdminVideo()
	{
		adminVideo.Play ();
	}

	void onAdminVideoEnded()
	{
		adminVideo.OnEnd -= onAdminVideoEnded;
		adminVideo.Stop ();
		adminVideo.UnLoad ();
		mainUI.SetActive (true);
		physicalMissionScreen.SetActive (true);
		pauseUI.SetActive (false);
	}

	void disableAdminVideo()
	{
		adminVideo.gameObject.SetActive (false);
	}

	void playStopChapter()
	{
		adminVideo.gameObject.SetActive (false);
		StartCoroutine ("onPlayPause");
	}

	IEnumerator onPlayPause()
	{
		chapterVideo.Play ();
		yield return new WaitWhile (() =>  chapterVideo.GetSeekPosition() < 50);
		//while(chapterVideo.GetSeekPosition() > 1000)
			//yield return null;

		chapterVideo.Pause ();
	}

	void playChapterVideo()
	{
		mainUI.SetActive (false);
		adminVideo.gameObject.SetActive (false);
		chapterVideo.Play ();
		pauseUI.SetActive (true);
		pauseUI.GetComponent <PauseController> ().currentVideo = chapterVideo;
	}

	void enableNextBtn()
	{
		physicalMissionScreen.SendMessage ("enableNextBtn");
	}

	public void onChapterVideoEnded()
	{
		StartCoroutine (CheckInputVideo());
	}

	IEnumerator CheckInputVideo()
	{
		if (chapterVideo.m_strFileName == "putitback.mp4") {
			chapterVideo.Stop ();
			chapterVideo.UnLoad ();
			chapterVideo.Load ("Chapter02-HD.mp4");
			pauseUI.SetActive (false);
			yield return new WaitForSeconds(1f);
			mainUI.SetActive (true);
			enableNextBtn ();
			//chapterVideo.Play();
		} else {
			chapterVideo.OnEnd -= onChapterVideoEnded;
			chapterVideo.Stop ();
			chapterVideo.UnLoad ();

			Resources.UnloadUnusedAssets ();
			System.GC.Collect ();

			SceneManager.LoadScene ("Adventure1_3");
		}
	}

	// Update is called once per frame
	void Update () {
		
	}
}
