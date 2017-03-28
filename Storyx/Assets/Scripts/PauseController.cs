using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseController : MonoBehaviour {

	// Use this for initialization
	private bool isPause = false;

	public Texture t_play;
	public Texture t_stop;

	public MediaPlayerCtrl currentVideo;

	public UITexture currentTex;

	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void onClick()
	{
		isPause = !isPause;
		if (isPause) {
			currentTex.mainTexture = t_play;
			Time.timeScale = 0;
			currentVideo.Pause ();
		} else {
			currentTex.mainTexture = t_stop;
			Time.timeScale = 1;
			currentVideo.Play ();
		}
	}

}
