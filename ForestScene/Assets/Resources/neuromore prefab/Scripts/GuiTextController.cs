using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Text;
using System.Reflection;
//using TextFx;

// Animates Text with TextFX shown on UI layer for GUI
public class GuiTextController : MonoBehaviour
{
	public static GuiTextController instance = null;

	private ArrayList TextList = new ArrayList();

	// Use this for initialization
	void Awake()
	{
		// Singleton instantiation
		if (instance != null)
			throw new UnityException ("Duplicate allocation of singleton class!");
		instance = this;
	}
	
	void Update()
	{
		if(TextList.Count > 0)
		{
			ShowText (TextList[0] as GUIText);
			TextList.RemoveAt(0);
		}

	}


	// shows TextFX animation with given text and display time after animation
	public void ShowText(GUIText guitext)
	{
		GameObject textObject = (GameObject) Instantiate(Resources.Load("neuromore prefab/Prefabs/GuiText"));
		textObject.name = "GuiText";
		textObject.transform.SetParent(GameObject.Find("GUI").transform);
		//TextFxNative textFxNative = textObject.GetComponent<TextFxNative>();

		textObject.transform.localScale = new Vector3(1f,1f,1f);
		textObject.transform.localRotation = Quaternion.Euler(0f,0f,0f);

		//textFxNative.m_character_size = 5f * guitext.getSize();

		if(WorldController.instance.cameraMode == "OVR")
			textObject.transform.localPosition = new Vector3(0f,-350f,200f);
		else if(WorldController.instance.cameraMode == "CARDBOARD")
			textObject.transform.localPosition = new Vector3(0f,-100f,200f);
		else
			textObject.transform.localPosition = new Vector3(0f,0f,200f);

		textObject.GetComponent<DestroyObject>().timeToLive = 5f;
		//textFxNative.SetText(guitext.getText());
		//textFxNative.m_textColour = guitext.getColor();
		//textFxNative.m_override_font_baseline = true;
		//textFxNative.AnimationManager.PrepareAnimationData();
		//textFxNative.AnimationManager.PlayAnimation();

	}
	
	public void SetText (GUIText guitext)
	{
		TextList.Add(guitext);
;	}
}
