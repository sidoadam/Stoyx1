using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SelectPhotoManager : MonoBehaviour {

	public MediaPlayerCtrl videoManager;

	public GameObject take_photo_btn;
	public GameObject restart_btn;

	public Texture photoTexture;
	public UITexture avatar;
	public UILabel currentSeekPositionLablel;

	public GameObject image;

	public float [] photoBrakePoint;

	public bool bVisiblePhoto = false;

	private bool canCheckVideo = false;
	private int currentTimeGoal = 0;
	private float videoTime = 0;


	// Use this for initialization
	void Start () 
	{
		take_photo_btn.SetActive (false);
		restart_btn.SetActive (false);
		videoManager.OnReady += onVideoReady;
		videoManager.OnEnd += onVideoEnd;

		avatar.gameObject.SetActive (bVisiblePhoto);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void onVideoReady()
	{
		take_photo_btn.SetActive (true);
		Debug.Log ("onVideoReady");
	}

	void onVideoEnd()
	{
		restart_btn.SetActive (true);
	}

	public void onRestartGame()
	{
		Scene scene = SceneManager.GetActiveScene();
		SceneManager.LoadScene(scene.name);
	}

	public void GetImageFromCamera() {
		AndroidCamera.Instance.OnImagePicked += OnImagePicked;
		AndroidCamera.Instance.GetImageFromCamera();
	}

	private void OnImagePicked(AndroidImagePickResult result) {
		Debug.Log("OnImagePicked");
		if (result.IsSucceeded) {
			//AN_PoupsProxy.showMessage ("Image Pick Rsult", "Succeeded, path: " + result.ImagePath);
			//image.GetComponent<Renderer> ().material.mainTexture = result.Image;
			photoTexture = result.Image;
			avatar.mainTexture = result.Image;
			take_photo_btn.SetActive (false);
			startPlayVideo ();
			startCheckPhoto ();
			//StartCoroutine ("checkPhotoVisible");
		} else {
			//AN_PoupsProxy.showMessage ("Image Pick Rsult", "Failed");
		}

		AndroidCamera.Instance.OnImagePicked -= OnImagePicked;
	}

	void startPlayVideo()
	{
		videoManager.Play ();
		videoTime = Time.time;
	}

	IEnumerator checkPhotoVisible()
	{
		float sTime = Time.time;
		for (int i = 0; i < photoBrakePoint.Length; i++) {
			yield return new WaitForSeconds(photoBrakePoint[i]-(Time.time - sTime));
			Debug.Log ("ops="+(Time.time - sTime));
			bVisiblePhoto = !bVisiblePhoto;
			avatar.gameObject.SetActive (bVisiblePhoto);
		}
	}

	void startCheckPhoto()
	{
		currentTimeGoal = 0;
		canCheckVideo = true;
	}

	void stopCheckPhoto()
	{
		currentTimeGoal = 0;
		canCheckVideo = false;
	}

	void FixedUpdate()
	{
		//currentSeekPositionLablel.text = (Time.time - videoTime).ToString ();
		if (canCheckVideo) {
			if (currentTimeGoal >= photoBrakePoint.Length) {
				stopCheckPhoto ();
			} else {
				//if ((Time.time - videoTime) >= photoBrakePoint [currentTimeGoal])
				if ((float)(videoManager.GetSeekPosition () / 1000f) >= photoBrakePoint [currentTimeGoal])
				{
					bVisiblePhoto = !bVisiblePhoto;
					avatar.gameObject.SetActive (bVisiblePhoto);
					currentTimeGoal++;
				}
			}
		}
	}
}
