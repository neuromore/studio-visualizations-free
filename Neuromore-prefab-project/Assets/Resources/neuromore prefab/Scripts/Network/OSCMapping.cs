using UnityEngine;
using System.Collections;

public static class OSCMapping {

	public const int NEUROCORE_SERVER_OSC_PORT = 45554;

	// STUDIO IP ADDRESS
	public const string CONF_IP 			= "/studio/ip";

	// SESSION INFORMATION
	public const string SESSION_TIME 		= "/session/time";
	public const string SESSION_RUNNING 	= "/session/running";
	public const string SESSION_POINTS 		= "/session/points";
	public const string SESSION_PROGRESS	= "/session/progress";
	public const string SESSION_LEVEL		= "/session/level";

	// SPEED MAIN OBJECT IN FOCUS
	public const string MOVEMENT_SPEED 		= "/movement-speed";

	// AUDIO
	public const string AUDIO_VOLUME 		= "/audio/volume";

	// CONTROL PARTICLES AND LIGHT 
	public const string EXHAUST_SMOKE 		= "/exhaust-smoke";
	public const string EXHAUST_RAIN 		= "/exhaust-rain";
	public const string EXHAUST_SUN 		= "/exhaust-sun";
	public const string EXHAUST_WATER 		= "/exhaust-water";

	// CAMERA FEEDBACK
	public const string CAMERA_FADE_COLOR_A	= "/camera/fade-color/a";
	public const string CAMERA_FADE_COLOR_R = "/camera/fade-color/r";
	public const string CAMERA_FADE_COLOR_G = "/camera/fade-color/g";
	public const string CAMERA_FADE_COLOR_B = "/camera/fade-color/b";
	public const string CAMERA_BLUR 		= "/camera/blur";
	public const string CAMERA_VIGNETTING 	= "/camera/vignetting";
	public const string CAMERA_SEPIA 		= "/camera/sepia";
	public const string CAMERA_GRAYSCALE	= "/camera/grayscale";
	public const string CAMERA_WATER_FREEZE = "/camera/water/freeze";
	public const string CAMERA_WATER_AMOUNT = "/camera/water/amount";

	// TUNNEL SPECIFIC
	public const string TUNNEL_SIZE 		= "/tunnel-size";
	public const string TUNNEL_COLOR_A 		= "/tunnel-color/a";
	public const string TUNNEL_COLOR_R 		= "/tunnel-color/r";
	public const string TUNNEL_COLOR_G 		= "/tunnel-color/g";
	public const string TUNNEL_COLOR_B 		= "/tunnel-color/b";

	// SPECIFIC BODY FEEDBACK: TODO:
	public const string BODYFEEDBACK_HR		= "/heartrate";
	public const string BODYFEEDBACK_HRV	= "/hrv";
	public const string BODYFEEDBACK_BR		= "/breathingrate";
}
