using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextureLoaderController : MonoBehaviour {

	// Use this for initialization
	public PhotoType currentPhotoTYpe;

	public int textureID = 0;

	private UITexture mainTex;

	void Start () {
		mainTex = gameObject.GetComponent <UITexture>();
		loadTexture ();
	}

	void loadTexture()
	{
		if (currentPhotoTYpe == PhotoType.PhotoTYpeB) {
			mainTex.mainTexture = MainDataHolder.instanse.B_PhotoHolder[textureID] as Texture;
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}


}
