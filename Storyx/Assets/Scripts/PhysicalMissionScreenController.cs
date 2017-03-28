using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicalMissionScreenController : MonoBehaviour {

	public GameObject cameraBtn;
	public GameObject nextBtn;
	private bool NextBtnIsActive = false;
	// Use this for initialization
	void Start () {
		disableNextBtn ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnEnable()
	{
		startCameraIcon ();
	}


	public void takePhoto()
	{
		TakePhotoController manager = FindObjectOfType <TakePhotoController> ();
		manager.GetImageFromCamera ();
	}

	void startCameraIcon()
	{
		cameraBtn.GetComponent <UITexture>().alpha = 0.3f;
		cameraBtn.GetComponent <BoxCollider> ().enabled = false;

		Invoke ("enableCameraIcon",20f);
	}

	void enableCameraIcon()
	{
		cameraBtn.GetComponent <UITexture>().alpha = 1f;
		cameraBtn.GetComponent <BoxCollider> ().enabled = true;
	}


	void disableCameraIcon()
	{
		cameraBtn.SetActive (false);
	}

	void enableNextBtn()
	{
		NextBtnIsActive = true;
		nextBtn.GetComponent <UISprite>().color = Color.green;
		nextBtn.GetComponent <UISprite> ().alpha = 1;

		//disableNextBtn ();
	}

	void disableNextBtn()
	{
		NextBtnIsActive = false;
		nextBtn.GetComponent <UISprite>().color = Color.grey;
		nextBtn.GetComponent <UISprite> ().alpha = 0.7f;
	}

	public void onNext()
	{
		if (NextBtnIsActive) {
			TakePhotoController manager = FindObjectOfType <TakePhotoController> ();
			manager.MissionScreenNext ();
			gameObject.SetActive (false);
		}

	}
}
