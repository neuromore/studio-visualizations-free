using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class updatePoints : MonoBehaviour {

	// Update is called once per frame
	void Update () {
		if(DataHolder.gFeedbackValues.ContainsKey(OSCMapping.SESSION_POINTS))
			if(DataHolder.gFeedbackValues[OSCMapping.SESSION_POINTS] > -1)
				this.GetComponent<Text>().text = Math.Round(DataHolder.gFeedbackValues[OSCMapping.SESSION_POINTS],0).ToString();
	}
}
