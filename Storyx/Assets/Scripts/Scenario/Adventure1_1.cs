using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Adventure1_1 : MonoBehaviour {

	// Use this for initialization

	public MediaPlayerCtrl adminVideo;
	public MediaPlayerCtrl adminVideo2;
	public MediaPlayerCtrl chapterVideo;
	public TakePhotoController photoController;
	public GameObject mainUI;
	public GameObject pauseUI;

	void Start () {
		adminVideo.OnEnd += onAdminVideoEnded;
		adminVideo2.OnEnd += onAdminVideoEnded2;
		chapterVideo.OnEnd += onChapterVideoEnded;
		mainUI.SetActive (false);
		chapterVideo.gameObject.SetActive (false);

		adminVideo2.Load ("putitback.mp4");

		pauseUI.GetComponent <PauseController> ().currentVideo = adminVideo;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void onAdminVideoEnded()
	{
		adminVideo.OnEnd -= onAdminVideoEnded;
		adminVideo.Stop ();
		adminVideo.UnLoad ();

		adminVideo.gameObject.SetActive (false);
		mainUI.SetActive (true);
		photoController.GetImageFromCamera ();

		pauseUI.SetActive (false);
	}

	void onAdminVideoEnded2()
	{
		StartCoroutine (CheckInputVideo());
	}

	IEnumerator CheckInputVideo()
	{
		if (adminVideo2.m_strFileName == "putitback.mp4") {
			adminVideo2.Stop ();
			adminVideo2.UnLoad ();
			adminVideo2.Load ("admin/opening.mp4");
			yield return new WaitForSeconds(1f);
			adminVideo2.Play();
		} else {
			adminVideo2.OnEnd -= onAdminVideoEnded2;
			adminVideo2.Stop ();
			adminVideo2.UnLoad ();
			adminVideo2.gameObject.SetActive (false);

			chapterVideo.gameObject.SetActive (true);

			pauseUI.SetActive (false);

			playChapterVideo ();
		}
	}

	void playAdminVideo2()
	{
		
		adminVideo.gameObject.SetActive (false);
		mainUI.SetActive (false);
		adminVideo2.gameObject.SetActive (true);
		adminVideo2.Play ();

		pauseUI.SetActive (true);

		pauseUI.GetComponent <PauseController> ().currentVideo = adminVideo2;
	}

	void playChapterVideo()
	{
		adminVideo.gameObject.SetActive (false);
		mainUI.SetActive (false);
		chapterVideo.Play ();

		pauseUI.SetActive (true);

		pauseUI.GetComponent <PauseController> ().currentVideo = chapterVideo;
	}

	void onChapterVideoEnded()
	{
		chapterVideo.OnEnd -= onChapterVideoEnded;
		chapterVideo.Stop ();
		chapterVideo.UnLoad ();

		pauseUI.SetActive (false);

		Resources.UnloadUnusedAssets ();
		System.GC.Collect ();

		SceneManager.LoadScene ("Adventure1_2");
	}
}
