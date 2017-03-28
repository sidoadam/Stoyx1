using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChromaColorApply : MonoBehaviour {

	public bool loadFromHolder = false;

	UITexture t;

	bool isSet = false;
	// Use this for initialization
	void Start () {
		t = gameObject.GetComponent <UITexture>();

		if (loadFromHolder) {
			t.mainTexture = MainDataHolder.instanse.avatar;
			t.uvRect = MainDataHolder.instanse.current_rect;
		}
	}

	void OnEnable()
	{
		if (MainDataHolder.instanse != null) t.uvRect = MainDataHolder.instanse.current_rect;
	}
	
	// Update is called once per frame
	void Update () {
		/*if (t.drawCall != null) {
			if (!isSet) {
				//isSet = true;
				t.drawCall.dynamicMaterial.SetColor ("_KeyColor",MainDataHolder.currentChromaColor);
			}
		}*/
	}
}
