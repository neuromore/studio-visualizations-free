using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Text;

public class DataHolder : MonoBehaviour
{
	// Singleton instance
	public static DataHolder instance = null;

	// session information
	public  static bool			gIsRunning					= false;	// is session running? updated by studio

	public static float			gHeartRate					= -1.0f;
	public static float			gHRV						= -1.0f;
	public static float			gBreathingRate				= -1.0f;

	// use new paths as hashmap
	public static Dictionary<string, float> gFeedbackValues = new Dictionary<string, float>();

	/////////////////////////////////////////////////////////////////////////////////////////////////////////////

	void Awake()
	{
		// Singleton instantiation
		if (instance != null)
			throw new UnityException ("Duplicate allocation of singleton class!");
		instance = this;

		// don't allow pause rendering when loosing window focus
		Application.runInBackground = true;
        Cursor.visible = false;

        // initialize globals
        Init();
	}
	
	void Start()
	{
		//reset data session not yet started
		Init();
	}

	void OnDestroy() {}


    public void Init ()
    {
        // reset all prefab globals
		gIsRunning 			= false;
		DataHolder.gFeedbackValues.Clear();

		DataHolder.gFeedbackValues.Add(OSCMapping.SESSION_TIME, -1);
		DataHolder.gFeedbackValues.Add(OSCMapping.SESSION_POINTS, -1);
		DataHolder.gFeedbackValues.Add(OSCMapping.SESSION_PROGRESS, 0);
		DataHolder.gFeedbackValues.Add(OSCMapping.SESSION_LEVEL, -1);

		DataHolder.gFeedbackValues.Add(OSCMapping.MOVEMENT_SPEED, 0.01f);

		// DataHolder.gFeedbackValues.Add(OSCMapping.TUNNEL_SIZE, 0);
		// DataHolder.gFeedbackValues.Add(OSCMapping.TUNNEL_COLOR_R, 0);
		// DataHolder.gFeedbackValues.Add(OSCMapping.TUNNEL_COLOR_G, 0.62353f);
		// DataHolder.gFeedbackValues.Add(OSCMapping.TUNNEL_COLOR_B, 0.8902f);
		// DataHolder.gFeedbackValues.Add(OSCMapping.TUNNEL_COLOR_A, 1);
		
		gFeedbackValues.Add(OSCMapping.WEATHER_SUN,0.6f);
		gFeedbackValues.Add(OSCMapping.WEATHER_FOG,0.1f);
		gFeedbackValues.Add(OSCMapping.WEATHER_CLOUDS,0f);
		gFeedbackValues.Add(OSCMapping.WEATHER_RAIN,0);

		DataHolder.gFeedbackValues.Add(OSCMapping.CAMERA_FADE_COLOR_R, 0);
		DataHolder.gFeedbackValues.Add(OSCMapping.CAMERA_FADE_COLOR_G, 0);
		DataHolder.gFeedbackValues.Add(OSCMapping.CAMERA_FADE_COLOR_B, 0);
		DataHolder.gFeedbackValues.Add(OSCMapping.CAMERA_FADE_COLOR_A, 0);
		DataHolder.gFeedbackValues.Add(OSCMapping.CAMERA_BLUR, 0);
		DataHolder.gFeedbackValues.Add(OSCMapping.CAMERA_VIGNETTING, 0);
		DataHolder.gFeedbackValues.Add(OSCMapping.CAMERA_SEPIA, 0);
		DataHolder.gFeedbackValues.Add(OSCMapping.CAMERA_GRAYSCALE, 0);
		DataHolder.gFeedbackValues.Add(OSCMapping.CAMERA_WATER_FREEZE, 0);
		DataHolder.gFeedbackValues.Add(OSCMapping.CAMERA_WATER_AMOUNT, 0);

		DataHolder.gFeedbackValues.Add(OSCMapping.AUDIO_VOLUME, 0);

		// DataHolder.gFeedbackValues.Add(OSCMapping.EXHAUST_SMOKE, 0);
		// DataHolder.gFeedbackValues.Add(OSCMapping.EXHAUST_RAIN, 0);
		// DataHolder.gFeedbackValues.Add(OSCMapping.EXHAUST_SUN, 1);
		// DataHolder.gFeedbackValues.Add(OSCMapping.EXHAUST_WATER, 0);


		// some special cases
		gHeartRate			= -1.0f;
		gHRV				= -1.0f;
		gBreathingRate		= -1.0f;
    }
}
