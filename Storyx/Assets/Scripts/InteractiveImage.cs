using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class Data{
	public float timeKey = 0;
	public bool visible = false;
	public bool isSetPosition = false;
	public Vector3 position = Vector3.zero;
	public bool isTweenPosition = false;
	public Vector3 desPosition = Vector3.zero;
	public float tweenPositionTime = 0;
	public bool isSetRotaion = false;
	public Vector3 rotation = Vector3.zero;
	public bool isTweenRotation = false;
	public Vector3 desRotaion = Vector3.zero;
	public float tweenRotaionTime = 0;
	public bool isSetScale = false;
	public Vector3 scale = Vector3.zero;
	public bool isTweenScale = false;
	public Vector3 scaleDes = Vector3.zero;
	public float tweenScaleTime = 0;
}


public class InteractiveImage : MonoBehaviour {

	public MediaPlayerCtrl player;

	public GameObject target;

	public Data[] BreakPoints;

	public bool startVisible = false;

	private int currentTimeGoal = 0;

	private SelectPhotoManager manager;

	private UITexture targetTexture;

	// Use this for initialization
	void Start () {
		target.SetActive (startVisible);
		manager = FindObjectOfType <SelectPhotoManager>();
		targetTexture = target.GetComponent <UITexture>();
	}
	
	void FixedUpdate()
	{
		if (targetTexture != null) {
			if (targetTexture.mainTexture == null) {
				if (manager.photoTexture != null) {
					targetTexture.mainTexture = manager.photoTexture;
				}
			}
		}

		if (BreakPoints.Length > 0) {
			if (currentTimeGoal < BreakPoints.Length) {
				if ((float)(player.GetSeekPosition () / 1000f) >= BreakPoints [currentTimeGoal].timeKey)
				{
					TweenPosition tp = target.GetComponent <TweenPosition>();
					if (tp != null) {
						tp.enabled = false;
					}

					TweenRotation tr = target.GetComponent <TweenRotation>();
					if (tr != null) {
						tr.enabled = false;
					}

					TweenScale ts = target.GetComponent <TweenScale>();
					if (ts != null) {
						ts.enabled = false;
					}

					Data d = BreakPoints [currentTimeGoal];
					target.SetActive (d.visible);
					if (d.isSetPosition) {
						target.transform.localPosition = d.position;
					}
					if (d.isTweenPosition)
						TweenPosition.Begin (target, d.tweenPositionTime, d.desPosition).ignoreTimeScale = false;
					if (d.isSetRotaion)
						target.transform.localEulerAngles = d.rotation;
					if (d.isTweenRotation) {
						TweenRotation.Begin (target, d.tweenRotaionTime,Quaternion.Euler(d.desRotaion)).ignoreTimeScale = false;
					}
					if (d.isSetScale) {
						target.transform.localScale = d.scale;
					}
					if (d.isTweenScale) {
						TweenScale.Begin (target, d.tweenScaleTime, d.scaleDes).ignoreTimeScale = false;
					}
					currentTimeGoal++;
				}
			} 
		}
	}
}
