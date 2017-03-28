using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainDataHolder : MonoBehaviour {

	public ArrayList B_PhotoHolder = new ArrayList();

	public static MainDataHolder instanse;

	public Texture avatar;

	public Rect current_rect;

	public static Color currentChromaColor;
	// Use this for initialization

	void Awake()
	{
		instanse = this;
		DontDestroyOnLoad (gameObject);
	}

	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void AddBTexture(Texture tex)
	{
		B_PhotoHolder.Add (tex);
	}
}
