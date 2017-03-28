using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakePhotoController : MonoBehaviour {

	// Use this for initialization
	public int photoCount = 4;

	public UITexture[] previewPhotos;

	public GameObject takePhotoPreviewContainer;

	public UITexture takePhotoPreviewImage;

	public UITexture pictureTypeB;
	public UITexture pictureTypeB2;

	public GameObject nextBtn;

	public UITexture[] interactivedTextures;

	private int currentPhoto = 0;

	private bool photosIsReady = false;

	public GameObject scenarioObject;

	public GameObject WebCamObject;

	public Scenario currentScenario;

	public PhotoType currentPhotoTYpe;

	void Start () {
		//takePhotoPreviewContainer.SetActive (false);
		//disableNextBtn ();
		if (currentPhotoTYpe == PhotoType.PhotoTypeA) {
			AndroidCamera.Instance.OnImagePicked += OnImagePicked;
		}

		if (WebCamObject != null) {
			WebCamObject.SetActive (false);
		}
		if (pictureTypeB != null) {
			pictureTypeB.gameObject.transform.parent.gameObject.SetActive (false);
		}
		if (pictureTypeB2 != null) {
			pictureTypeB.gameObject.transform.parent.gameObject.SetActive (false);
		}

		//GetImageFromCamera ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnDestroy()
	{
		try
		{
			//AndroidCamera.Instance.OnImagePicked -= OnImagePicked;
		}
		catch (System.Exception err) {

		}

	}

	public void GetImageFromCamera() {

		if (currentPhotoTYpe == PhotoType.PhotoTypeA) {
			//AndroidCamera.Instance.GetImageFromCamera();
			openWebCam ();
		} else if (currentPhotoTYpe == PhotoType.PhotoTYpeB) {
			openWebCam ();	
		}

	}

	public void openWebCam()
	{
		WebCamObject.SetActive (true);
	}

	public void submitPhoto()
	{
		//previewPhotos [currentPhoto].mainTexture = takePhotoPreviewImage.mainTexture;
		currentPhoto++;
		takePhotoPreviewContainer.SetActive(false);
		if (currentPhoto < photoCount) {
			if (currentScenario == Scenario.Scenario1_4) {
				pictureTypeB.mainTexture = takePhotoPreviewImage.mainTexture;
				scenarioObject.SendMessage ("playAdminVideo2");
			} else {
				GetImageFromCamera ();
			}
				

		} else {
			if (currentScenario == Scenario.Scenario1_1) {
				foreach (UITexture t in interactivedTextures) {
					t.uvRect = takePhotoPreviewImage.uvRect;
					t.mainTexture = takePhotoPreviewImage.mainTexture;
					t.material = takePhotoPreviewImage.material;

					MainDataHolder.instanse.avatar = takePhotoPreviewImage.mainTexture;
					MainDataHolder.instanse.current_rect = takePhotoPreviewImage.uvRect;
				}
				scenarioObject.SendMessage ("playAdminVideo2");
			}
			if (currentScenario == Scenario.Scenario1_2 || currentScenario == Scenario.Scenario1_3 || currentScenario == Scenario.Scenario1_5 || currentScenario == Scenario.Scenario1_6 || currentScenario == Scenario.Scenario1_7) {
				//scenarioObject.SendMessage ("enableNextBtn");
				scenarioObject.SendMessage ("playChapterVideo");
			}
			if (currentScenario == Scenario.Scenario1_4 ) {
				pictureTypeB2.mainTexture = takePhotoPreviewImage.mainTexture;
				scenarioObject.SendMessage ("playInputVideo");
			}
		}

		if (currentPhotoTYpe == PhotoType.PhotoTYpeB) {
			MainDataHolder.instanse.AddBTexture (takePhotoPreviewImage.mainTexture);
		}
	}

	void enableNextBtn()
	{
		nextBtn.GetComponent <UISprite>().color = Color.green;
		nextBtn.GetComponent <UISprite> ().alpha = 1;
		photosIsReady = true;
	}

	void disableNextBtn()
	{
		nextBtn.GetComponent <UISprite>().color = Color.grey;
		nextBtn.GetComponent <UISprite> ().alpha = 0.7f;
		photosIsReady = false;
	}

	public void retryPhoto()
	{
		takePhotoPreviewContainer.SetActive(false);
		GetImageFromCamera ();
	}

	private void OnImagePicked(AndroidImagePickResult result) {
		if (result.IsSucceeded) {
			//photoTexture = result.Image;
			applePhoto(result.Image);

		} else {
			AN_PoupsProxy.showMessage ("Image Pick Rsult", "Failed");
		}
	}

	void applePhoto(Texture2D tex)
	{
		
		takePhotoPreviewContainer.SetActive(true);
		takePhotoPreviewImage.mainTexture = tex;
		//takePhotoPreviewImage.MakePixelPerfect ();
	}

	public void onGetPictureFromWebCam(Texture2D tex)
	{
		applePhoto (tex);
		WebCamObject.SetActive (false);
	}

	public void onNext()
	{

	}

	public void MissionScreenNext()
	{
		if (currentScenario == Scenario.Scenario1_2 || currentScenario == Scenario.Scenario1_3 || currentScenario == Scenario.Scenario1_5 || currentScenario == Scenario.Scenario1_6 || currentScenario == Scenario.Scenario1_7) {
			pictureTypeB.gameObject.transform.parent.gameObject.SetActive (true);
			pictureTypeB.mainTexture = takePhotoPreviewImage.mainTexture;

			scenarioObject.SendMessage ("playStopChapter");

			Invoke ("hidePictureTypeB",3f);
		}
		if (currentScenario == Scenario.Scenario1_4) {
			pictureTypeB.gameObject.transform.parent.gameObject.SetActive (true);
			scenarioObject.SendMessage ("disableAdminVideo2");

			scenarioObject.SendMessage ("playStopChapter");

			Invoke ("hidePictureTypeB",3f);
		}
	}

	void hidePictureTypeB()
	{
		pictureTypeB.gameObject.transform.parent.gameObject.SetActive (false);
		if (currentScenario == Scenario.Scenario1_2 || currentScenario == Scenario.Scenario1_3 || currentScenario == Scenario.Scenario1_5 || currentScenario == Scenario.Scenario1_6 || currentScenario == Scenario.Scenario1_7) {
			scenarioObject.SendMessage ("playChapterVideo");
		}
		if (currentScenario == Scenario.Scenario1_4) {
			pictureTypeB2.gameObject.transform.parent.gameObject.SetActive (true);
			Invoke ("hidePictureTypeB2",3f);
		}
	}

	void hidePictureTypeB2()
	{
		pictureTypeB2.gameObject.transform.parent.gameObject.SetActive (false);
		if (currentScenario == Scenario.Scenario1_4) {
			scenarioObject.SendMessage ("playChapterVideo");
		}
	}
}



public enum Scenario{

	Scenario1_1,
	Scenario1_2,
	Scenario1_3,
	Scenario1_4,
	Scenario1_5,
	Scenario1_6,
	Scenario1_7,
	Scenario1_8,
}

public enum PhotoType{

	PhotoTypeA,
	PhotoTYpeB,
}