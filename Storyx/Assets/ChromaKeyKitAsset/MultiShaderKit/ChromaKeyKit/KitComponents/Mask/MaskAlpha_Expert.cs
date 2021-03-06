﻿using UnityEngine;

[RequireComponent(typeof(KitControllerBase))]
public class MaskAlpha_Expert : KitComponentBase
{
	[Range(0, 1)]
	public float alphaEdge = 0.0f;
	[Range(0, 1)]
	public float alphaPow = 1.0f;

	[Tooltip("Update shader properties in play mode")]
	public bool isUpdateProperties = true;

	[Tooltip("MaskAlpha_Expert.shader")]
	public Shader maskShader = null;

	private float _alphaEdge = -1;
	private float _alphaPow = -1;
	
	private RenderTexture _rtM; //render texture Mask
	private Material _shaderMat;

	void Awake() {
		if (maskShader == null) {
			maskShader = Shader.Find("ChromaKeyKit/Mask/MaskAlpha_Expert");
		}
	}

	// Use this for initialization
	void Start() {
		_shaderMat = createMaterial(maskShader);
		updateShaderProperties();
    }

	private void updateShaderProperties() {
		if (_alphaEdge != alphaEdge) {
			_alphaEdge = alphaEdge;
			_shaderMat.SetFloat("_AlphaEdge", _alphaEdge);
		}
		if (_alphaPow != alphaPow) {
			_alphaPow = alphaPow;
			_shaderMat.SetFloat("_AlphaPow", _alphaPow);
		}
	}

	override public RenderTexture getRender(RenderTexture rt_src) {
		if (_rtM == null) {
			_rtM = new RenderTexture(_textureWidth, _textureHeight, 0);
		}
		if (isUpdateProperties) {
			updateShaderProperties();
		}
		_shaderMat.SetTexture("_MaskTex", rt_src);
		_rtM.DiscardContents();
		Graphics.Blit(_tA, _rtM);
		Graphics.Blit(_rtS, _rtM, _shaderMat);
		return _rtM;
	}
}
