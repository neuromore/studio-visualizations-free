using UnityEngine;
using UnityEngine.XR;
using System.Collections;

public class CameraController : MonoBehaviour {

	// singleton instance	
	public static CameraController instance = null;

	private GameObject camObject;

	// Initialisation
	void Awake ()
	{
		if (instance != null)
			throw new UnityException ("Duplicate allocation of singleton class!");
		instance = this;
	}

	void Update ()
	{
	}

	public GameObject createCamera(string mode)
	{
		Instantiate(Resources.Load("neuromore prefab/Prefabs/FadeIn"));
		UnityEngine.XR.XRSettings.enabled = false;
		if(mode == "OVR")
		{
			UnityEngine.XR.XRSettings.enabled = true;
			camObject = (GameObject) Instantiate(Resources.Load("neuromore prefab/Prefabs/OVRCameraRig"));
		}
		else if(mode == "CARDBOARD")
		{
			camObject = (GameObject) Instantiate(Resources.Load("neuromore prefab/Prefabs/CameraCB"));
		}
		else if(mode == "NORMAL")
		{
			camObject = (GameObject) Instantiate(Resources.Load("neuromore prefab/Prefabs/Camera"));
		}
		return camObject;
	}

	public GameObject createIntroCamera(string mode)
	{
		Instantiate(Resources.Load("neuromore prefab/Prefabs/FadeIn"));
		UnityEngine.XR.XRSettings.enabled = false;
		if(mode == "OVR")
		{
			UnityEngine.XR.XRSettings.enabled = true;
			camObject =  (GameObject) Instantiate(Resources.Load("neuromore prefab/Prefabs/IntroOVR"));
		}
		else if(mode == "CARDBOARD")
		{
			camObject = (GameObject) Instantiate(Resources.Load("neuromore prefab/Prefabs/IntroCB"));
		}
		else if(mode == "NORMAL")
		{
			camObject = (GameObject) Instantiate(Resources.Load("neuromore prefab/Prefabs/Intro"));
		}
		return camObject;
	}
}
