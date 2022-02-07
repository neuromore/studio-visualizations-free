using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.IO;
using MiniJSON;

public class StageController : MonoBehaviour
{
	// Singleton instance
	public static StageController instance = null;

	// Stage stuff
	private IList 		stagesEvents;							// stages config

	private int			nextStage					= 0;		// stage to switch next
	private int			currentStage				= 0;		// stage that is currently running or in progress of beeing switched to
	private int			numberStages				= 0;    	// number of given stages in json

	public  int 		GetReachedStage()			{ return nextStage;	}		// Returns the number of already completed stages
	public  int 		GetNumberOfStages()			{ return numberStages;}		// Returns the number of all stages in this level
	public 	int 		GetStage ()					{ return currentStage;}

	public  bool		isStageSwitching			= false;	// if switching e.g. animation is currently playing 
	public float 		pastSwitchingTime			= 0f;		// time counter if stages are switching
	public float		switchingTime				= 0f;


	// Initialization
	void Awake ()
	{
		// Singleton instantiation
		if (instance != null)
			throw new UnityException ("Duplicate allocation of singleton class!");
		instance = this;
		// get information from json file
		readJsonConfig(Resources.Load("Config") as TextAsset);
		Init ();
	}


	// After initialization of all prefabs and game objects
	void Start()
	{
	}


	// Reset stage controller
	public void Init ()
	{
		nextStage = 1;
		currentStage = 0;
	}


	// Update is called once per frame
	void Update ()
	{
		// if session is not running return
		if(! DataHolder.gIsRunning) 
			return;


		// stage is switching
		if(isStageSwitching)
		{
		}
		else if (!isStageSwitching)
		{
		}


		//**************************
		// end current stage and begin stage switching
		if (nextStage != currentStage && numberStages > 0 && !isStageSwitching)
		{
			// End current stage
			WorldController.instance.EndStage (currentStage );			
			// Send switchStage command to the studio
			Networking.instance.SendSwitchStageEvent (nextStage - 1); 		   	// is 0-indexed
			// Begin Stage Switching
			WorldController.instance.BeginSwitchStage(currentStage, nextStage);
			isStageSwitching = true;
		}
		//**************************
		// update switching
		else if (isStageSwitching && nextStage != currentStage)
		{
			// Use Time for Switching
			if(switchingTime >= 0.1f)
			{
				pastSwitchingTime += Time.deltaTime;
				if(pastSwitchingTime >= switchingTime)
					currentStage = nextStage; // ends the switching
			}
			// directly jump to new stage
			else
			{
				currentStage = nextStage; // ends the switching
			}
			WorldController.instance.UpdateSwitchStage(currentStage, nextStage);
		}


		//**************************
		// then end switching happens when we set current stage to next stage
		else if (isStageSwitching && nextStage == currentStage)
		{
			// End switching
			WorldController.instance.EndSwitchStage(nextStage, nextStage);
			isStageSwitching = false;
			pastSwitchingTime = 0f;
			
			// start next stage 
			WorldController.instance.BeginStage ( nextStage );
		}
	}

	
	// JSON Parsing
	// Read stages information out of json file
	private void readJsonConfig(TextAsset jsonTextAsset)
	{
		// file exists?
		if(jsonTextAsset == null)
			return;
		
		//read file
		StringReader streamReader = new StringReader(jsonTextAsset.text);
		string jsonFileString = streamReader.ReadToEnd();
		streamReader.Close();
		
		// convert the data to a string
		var json = Json.Deserialize(jsonFileString) as Dictionary<string, object>;
		
		// parse it	
		if(json.ContainsKey("stages"))
		{
			stagesEvents = (IList) json["stages"];
			numberStages = stagesEvents.Count;
		}
	}
	
	// Switch to specific stage (1-indexed)
	public void GoToStage (int index)
	{
		if (index <= numberStages) 
			nextStage = index;
		currentStage = -1;
	}


}
