using UnityEngine;

#if UNITY_EDITOR

[RequireComponent(typeof(Renderer))]
public class VideoControllerRenderer : MonoBehaviour {

	// Use this for initialization
	void Start () {
		var material = GetComponent<Renderer>().material;
		var texture = material.mainTexture;
		if (texture != null) {
			if (texture is MovieTexture) {
				MovieTexture movieTexture = texture as MovieTexture;
				movieTexture.loop = true;
				movieTexture.Play();
			} else {
				Debug.LogError("VideoControllerRenderer: Texture is not MovieTexture");
			}
		} else {
			if (material.name == "Default-Material (Instance)") {
				Debug.LogWarning("VideoControllerRenderer: Create other material instead Default-Material");
			}
			Debug.LogError("VideoControllerRenderer: Material requires mainTexture");
		}
    }

}

#endif
