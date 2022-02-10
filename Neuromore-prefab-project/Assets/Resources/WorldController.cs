using UnityEngine;

public class WorldController : AbstractWorldController
{
	// singleton instance	
	public static WorldController instance = null;

	// skybox, remove while intro is running
	public Material skybox;

	// select camera mode
	public string cameraMode = "NORMAL";
	GameObject cam;
	

	// Initialisation
	override public void Awake ()
	{
		if (instance != null)
			throw new UnityException ("Duplicate allocation of singleton class!");
		instance = this;
	}

	// Called on session end or connection to studio lost
	override public void ResetWorld ()
	{
		Debug.Log("Reset World");
		DataHolder.instance.Init();

		foreach(GameObject guiText in GameObject.FindGameObjectsWithTag("GameController"))
			DestroyImmediate(guiText);
		
	}


	// Update is called once per frame
	override public void UpdateStage (int stageIndex)
	{
		//
		// Updates for all stages :
		//


		// control tunnel size if not virutal reality supported
		if(cameraMode == "NORMAL"){
		}

		//
		// Updates per stage :
		//
		switch (stageIndex)
		{
		case 1:
			break;
					
		case 2:

			break;
		}
	}


	// Code is executed once when a stage begins
	override public void BeginStage (int stageIndex)
	{
		Instantiate(Resources.Load("neuromore prefab/Prefabs/FadeIn"));
		RenderSettings.skybox = skybox;
		ResetWorld();
		

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
