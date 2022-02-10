using UnityEngine;
using System.Collections;
using UnityStandardAssets.ImageEffects;
using UnityEngine.UI;

public class CameraEffectController : MonoBehaviour
{

	public GameObject CameraObject;
	public GameObject GUICameraObject;

	public Image FadingComponent;
	public Grayscale GrayScaleComponent;
	public VignetteAndChromaticAberration VignettingComponent;
	public SepiaTone SepiaComponent;
	public FrostEffect FrostComponent;
	public MotionBlur MotionBlurComponent;

		
	// Update is called once per frame
	void Update ()
	{

		if (DataHolder.gIsRunning) {


			// Color Fading
			Color color = FadingComponent.color;
			color.a = DataHolder.gFeedbackValues [OSCMapping.CAMERA_FADE_COLOR_A];
			color.r = DataHolder.gFeedbackValues [OSCMapping.CAMERA_FADE_COLOR_R];
			color.g = DataHolder.gFeedbackValues [OSCMapping.CAMERA_FADE_COLOR_G];
			color.b = DataHolder.gFeedbackValues [OSCMapping.CAMERA_FADE_COLOR_B];
			FadingComponent.color = color;

			// Motion Blur
			if(MotionBlurComponent != null){
				if (DataHolder.gFeedbackValues [OSCMapping.CAMERA_BLUR] > 0f) {
					MotionBlurComponent.enabled = true;
					MotionBlurComponent.blurAmount = DataHolder.gFeedbackValues [OSCMapping.CAMERA_BLUR];
				} else {
					MotionBlurComponent.enabled = false;
				}
			}

			// Gray scaling if > 0.5
			if(GrayScaleComponent != null){
				GrayScaleComponent.enabled = DataHolder.gFeedbackValues [OSCMapping.CAMERA_GRAYSCALE] >= 0.5;
			}

			// Sepia
			if(SepiaComponent != null){
				SepiaComponent.enabled = DataHolder.gFeedbackValues [OSCMapping.CAMERA_SEPIA] >= 0.5;
			}

			// Vignetting
			if(VignettingComponent != null){
				if (DataHolder.gFeedbackValues [OSCMapping.CAMERA_VIGNETTING] > 0f) {
					VignettingComponent.enabled = true;
					VignettingComponent.intensity = DataHolder.gFeedbackValues [OSCMapping.CAMERA_VIGNETTING];
				} else {
					VignettingComponent.enabled = false;
				}
			}
			// Water / Ice Effect
			if(FrostComponent != null){
				if (DataHolder.gFeedbackValues [OSCMapping.CAMERA_WATER_AMOUNT] > 0f) {
					FrostComponent.enabled = true;
					FrostComponent.FrostAmount = DataHolder.gFeedbackValues [OSCMapping.CAMERA_WATER_AMOUNT];
					FrostComponent.seethroughness = DataHolder.gFeedbackValues [OSCMapping.CAMERA_WATER_FREEZE];
				} else {
					FrostComponent.enabled = false;
				}
			}
		}
	}

	public void Reset ()
	{
		GUICameraObject.GetComponent<Camera> ().enabled = false;
		GUICameraObject.GetComponent<Camera> ().enabled = true;
	}
}
