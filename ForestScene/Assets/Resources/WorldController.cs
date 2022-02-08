using UnityEngine;
using PathCreation;
using PathCreation.Examples;

public class WorldController : AbstractWorldController
{
	// singleton instance	
	public static WorldController instance = null;


	private PathFollower pathFollower;

	public GameObject denseClouds;
	public GameObject rain;
	
	public PathCreator pathCreater;

	// skybox, remove while intro is running
	public Material skybox;
	public Light environmetLight;

	// select camera mode
	public string cameraMode = "NORMAL";
	GameObject cam;


	// Initialisation
	override public void Awake()
	{
		if (instance != null)
			throw new UnityException("Duplicate allocation of singleton class!");
		instance = this;
	}

	// Called on session end or connection to studio lost
	override public void ResetWorld()
	{
		Debug.Log("Reset World");
		DataHolder.instance.Init();
		//animator.speed = 0.01f;
		//animator.passedTime = 0;

		//splineRenderer.material.SetColor("_Color", Color.clear);

		//spaceship.SetActive(false);
		//spaceship.transform.position = animator.transform.position;

		foreach (GameObject guiText in GameObject.FindGameObjectsWithTag("GameController"))
			DestroyImmediate(guiText);

		//splineMesh.xyScale.x = 10f;
		//splineMesh.xyScale.y = 10f;

		if (GameObject.Find("Main Camera"))
		{
			cam = GameObject.Find("Main Camera");
			pathFollower = cam.AddComponent<PathFollower>();
			pathFollower.pathCreator = pathCreater;
			cam.GetComponentsInChildren<Camera>()[1].farClipPlane = 3000f;
		}
	}

	float CalculateEnvironmentLight(float value)
	{
		const float minLight = 1f;
		const float maxLight = 4f;
		if (value == 0)
		{
			environmetLight.shadows = LightShadows.None;
			denseClouds.SetActive(true);
		}
		else
		{
			denseClouds.SetActive(false);
			environmetLight.shadows = LightShadows.Soft;

		}

		return minLight + (maxLight - minLight) * value;
	}

	float CalculateEnvironmentFog(float value)
	{
		const float minFog = 0.001f;
		const float maxFog = 0.02f;
		return minFog + (maxFog - minFog) * value;
	}

	float CalculateCameraSpeed(float value)
	{
		const float minSpeed = 2f;
		const float maxSpeed = 5f;
		return minSpeed + (maxSpeed - minSpeed) * value;
	}

	void ControlClouds(float value)
	{
		if (value == 1)
		{
			denseClouds.SetActive(true);
		}
		else
		{
			denseClouds.SetActive(false);
		}
	}

	void ControlRain(float value)
	{
		if (value == 1)
		{
			rain.SetActive(true);
		}
		else
		{
			rain.SetActive(false);
		}
	}

	// Update is called once per frame
	override public void UpdateStage (int stageIndex)
	{
		RenderSettings.fogDensity = CalculateEnvironmentFog(DataHolder.gFeedbackValues[OSCMapping.WEATHER_FOG]);
		
		if (cam != null)
			pathFollower.speed = CalculateCameraSpeed(DataHolder.gFeedbackValues[OSCMapping.MOVEMENT_SPEED]);
		environmetLight.intensity = CalculateEnvironmentLight(DataHolder.gFeedbackValues[OSCMapping.WEATHER_SUN]);
		ControlClouds(DataHolder.gFeedbackValues[OSCMapping.WEATHER_CLOUDS]);
		ControlRain(DataHolder.gFeedbackValues[OSCMapping.WEATHER_RAIN]);
		
		//
		// Updates per stage :
		//
		switch (stageIndex)
		{
		case 1:
			break;
					
		case 2:
			//spaceship.SetActive(true);
			Vector3 pos = cam.transform.position + cam.transform.forward * (5 + (15 * DataHolder.gFeedbackValues[OSCMapping.MOVEMENT_SPEED]));
			break;
		}
	}


	// Code is executed once when a stage begins
	override public void BeginStage (int stageIndex)
	{
		Instantiate(Resources.Load("neuromore prefab/Prefabs/FadeIn"));
		RenderSettings.skybox = skybox;
		ResetWorld();

		//splineRenderer.material.SetColor("_Color",Color.clear);

		switch (stageIndex)
		{
			// stage 1:
			case 1:
				break;

			// stage 2:
			case 2:
				break;
		}
	}


	// Code is executed once when a stage ends
	override public void EndStage (int stageIndex)
	{
		RenderSettings.skybox = null;
		switch (stageIndex)
		{
			case 1:
				// stop scripts, deallocate objects ...
				// ...
				break;

			case 2:
				//spaceship.SetActive(false);
				break;
		}
	}


	// Code is executed while stage switch is happening
	override public void UpdateSwitchStage(int fromStage, int toStage)
	{
		// do something while switching, might feedback smiley for last stage
	}


	// Code is executed once when starting to switch to new stage
	override public void BeginSwitchStage(int fromStage, int toStage)
	{
	}


	// Code is executed once when ending to switch to new stage
	override public void EndSwitchStage(int fromStage, int toStage)
	{
	}
}
