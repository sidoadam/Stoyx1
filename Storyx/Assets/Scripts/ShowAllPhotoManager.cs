using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowAllPhotoManager : MonoBehaviour {

	// Use this for initialization
	public int photoCount = 1;
	public GameObject target;
	public GameObject scenarioObject;

	public GameObject[] list;

	private int currentSection = 0;

	void Start () {

		foreach (GameObject go in list) {
			go.SetActive (false);
		}
		Invoke ("animateContainer",1);
	}

	void animateContainer()
	{
		//TweenPosition.Begin (target, 1, new Vector3(-2100*(currentSection+1),0)).delay =  3;
		list[currentSection].SetActive(true);
		TweenAlpha.Begin(list[currentSection],0.3f,0);

		if (currentSection + 1 < list.Length) {
			list[currentSection+1].SetActive(true);
			list [currentSection + 1].GetComponent <UITexture>().alpha = 0;
			TweenAlpha.Begin(list[currentSection+1],0.3f,1);
		}

		Invoke ("finishAnimate",2f);
	}

	void finishAnimate()
	{
		if (currentSection < photoCount - 2) {
			currentSection++;
			animateContainer ();
		} else {
			//scenarioObject.SendMessage ("onPlayChapter");
		}
	}

	// Update is called once per frame
	void Update () {
		
	}
}
