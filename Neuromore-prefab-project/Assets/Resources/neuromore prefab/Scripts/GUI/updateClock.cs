using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class updateClock : MonoBehaviour {

	// Update is called once per frame
	void Update () {
		if(DataHolder.gFeedbackValues.ContainsKey(OSCMapping.SESSION_TIME)){
			if(DataHolder.gFeedbackValues[OSCMapping.SESSION_TIME] > -1){
				this.transform.GetComponent<Image>().enabled = true;
			}
			else{
				this.transform.GetComponent<Image>().enabled = false;
			}
		}
	}
}
