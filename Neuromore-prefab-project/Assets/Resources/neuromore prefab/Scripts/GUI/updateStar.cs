using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class updateStar : MonoBehaviour {

	// Update is called once per frame
	void Update () {
		if(DataHolder.gFeedbackValues.ContainsKey(OSCMapping.SESSION_POINTS)){
			if(DataHolder.gFeedbackValues[OSCMapping.SESSION_POINTS] > -1){
				this.transform.GetComponent<Image>().enabled = true;
			}
			else{
				this.transform.GetComponent<Image>().enabled = false;
			}
		}
	}
}
