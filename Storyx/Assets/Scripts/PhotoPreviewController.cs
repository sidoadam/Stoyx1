using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhotoPreviewController : MonoBehaviour {

	public UITexture targetTexture;
	private Rect defaultRect;
	private Vector2 additionalPos;
	public bool canScale = true;

	public bool canDrag = false;

	public bool changeRect = false;
	// Use this for initialization
	void Start () {
		
	}

	void OnEnable()
	{
		if (changeRect) {
			defaultRect = new Rect ();
			defaultRect.x = 0.3f;
			defaultRect.y = 0;
			defaultRect.height = 1;
			defaultRect.width = 0.4f;
			additionalPos = Vector2.zero;

			targetTexture.uvRect = defaultRect;
		}


	}

	public void updateBGColor()
	{
		Debug.Log ("update");
		if (targetTexture.drawCall != null)
		targetTexture.drawCall.dynamicMaterial.SetColor ("_KeyColor",UIColorPicker.current.value);
	}

	public void startDrag()
	{
		Debug.Log ("start drag");
		canDrag = true;
	}

	public void endDrag()
	{
		Debug.Log ("start drag");
		canDrag = false;
	}

	// Update is called once per frame
	void Update () {

		if (!canScale || !canDrag)
			return;

		if (Input.touchCount == 2) {
			// Store both touches.
			Touch touchZero = Input.GetTouch (0);
			Touch touchOne = Input.GetTouch (1);

			// Find the position in the previous frame of each touch.
			Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
			Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

			// Find the magnitude of the vector (the distance) between the touches in each frame.
			float prevTouchDeltaMag = (touchZeroPrevPos - touchOnePrevPos).magnitude;
			float touchDeltaMag = (touchZero.position - touchOne.position).magnitude;

			// Find the difference in the distances between each frame.
			float deltaMagnitudeDiff = (prevTouchDeltaMag - touchDeltaMag) / 100f;



		
			defaultRect.width += deltaMagnitudeDiff;
			defaultRect.height += deltaMagnitudeDiff;

			if (defaultRect.width > 2f) {
				defaultRect.width = defaultRect.height = 2;
			}

			if (defaultRect.width < 0.5f) {
				defaultRect.width = defaultRect.height = 0.5f;
			}

			defaultRect.x = defaultRect.y = (1 - defaultRect.width) / 2f;

			defaultRect.position += additionalPos;

			targetTexture.uvRect = defaultRect;
		} 
		else if (Input.touchCount == 1) 
		{
			Touch touchZero = Input.GetTouch (0);

			additionalPos = touchZero.deltaPosition;

			Rect r = targetTexture.uvRect;
			r.position -= additionalPos / 100f;
			targetTexture.uvRect = r;
		}
			
	}
}
