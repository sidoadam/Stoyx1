using UnityEngine;
using UnityEngine.UI;

#if UNITY_EDITOR

[RequireComponent(typeof(RawImage))]
public class VideoControllerRawImage : MonoBehaviour {

	// Use this for initialization
	void Start () {
		var rawImage = GetComponent<RawImage>();

		var texture = rawImage.texture;
		var material = rawImage.material;
		
		if (material != rawImage.defaultMaterial) {
			if(texture != null) {
				if (!PlayTexture(texture)) {
					Debug.LogError("VideoControllerRawImage: RawImage texture is not MovieTexture");
				}
			} else if (material.mainTexture != null) {
				texture = material.mainTexture;
				if (!PlayTexture(texture)) {
					Debug.LogError("VideoControllerRawImage: RawImage material.mainTexture is not MovieTexture");
				}
			} else {
				Debug.LogError("VideoControllerRawImage: RawImage material requires mainTexture");
			}
		} else if (texture != null) {
			if (!PlayTexture(texture)) {
				Debug.LogError("VideoControllerRawImage: RawImage texture is not MovieTexture");
			}
		} else {
			Debug.LogError("VideoControllerRawImage: RawImage requires texture OR material");
		}
	}

	private bool PlayTexture(Texture texture) {
		bool result = false;
		if (texture is MovieTexture) {
			MovieTexture movieTexture = texture as MovieTexture;
			movieTexture.loop = true;
			movieTexture.Play();

			result = true;
		}
		return result;
	}

}

#endif
