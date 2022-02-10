using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;


public class updateStageInfo : MonoBehaviour {

	// Update is called once per frame
	void Update () {

		if(DataHolder.gFeedbackValues.ContainsKey(OSCMapping.SESSION_LEVEL))
			if(DataHolder.gFeedbackValues[OSCMapping.SESSION_LEVEL] > -1)
				this.GetComponent<Text>().text = "level:"+Math.Round(DataHolder.gFeedbackValues[OSCMapping.SESSION_LEVEL],0).ToString();
	}
}
