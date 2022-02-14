using UnityEngine;
using System.Collections;


public abstract class AbstractWorldController : MonoBehaviour
{

	public abstract void Awake();

	private bool isRunning = true;

	// update callback (called each frame)
	void Update ()
	{
		// session started
		if(DataHolder.gIsRunning & !isRunning)
		{
			print("running");
			RenderSettings.skybox = WorldController.instance.skybox;

			//destroy intro
			DestroyImmediate(GameObject.Find ("Intro"));
			isRunning = true;

			//Create scene camera
			GameObject mainCamera = CameraController.instance.createCamera(WorldController.instance.cameraMode);
			mainCamera.name = "Main Camera";
			ResetWorld();
		}
		// session stopped
		else if(!DataHolder.gIsRunning & isRunning)
		{	
			print("not running");
			RenderSettings.skybox = null;
			ResetWorld();

			// destroy scene main camera object
			GameObject.DestroyImmediate(GameObject.Find("Main Camera"));

			// istantiate intro
			GameObject intro = CameraController.instance.createIntroCamera(WorldController.instance.cameraMode);
			intro.name = "Intro";

			isRunning = false;

		}

		if (!DataHolder.gIsRunning)
			return;

		// process scheduled stage begins / ends 
		foreach (int stage in scheduledStagesToEnd)
			EndStage (stage);
		scheduledStagesToEnd.Clear ();
		foreach (int stage in scheduledStagesToBegin)
			BeginStage (stage);
		scheduledStagesToBegin.Clear ();

		// update stage objects
		UpdateStage ( StageController.instance.GetReachedStage() );
	}

	public abstract void UpdateStage (int stage);
	public abstract void ResetWorld ();
	public abstract void BeginStage (int stage);
	public abstract void EndStage (int stage);

	public abstract void BeginSwitchStage(int fromStage, int toStage);
	public abstract void EndSwitchStage(int fromStage, int toStage);
	public abstract void UpdateSwitchStage(int fromStage, int toStage);

	private ArrayList scheduledStagesToBegin = new ArrayList();
	private ArrayList scheduledStagesToEnd = new ArrayList();

	public void ScheduleBeginStage (int stage) 		{ scheduledStagesToBegin.Add (stage); }
	public void ScheduleEndStage (int stage) 		{ scheduledStagesToEnd.Add (stage); }
}