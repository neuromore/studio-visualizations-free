using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class updateTime : MonoBehaviour {
	
	// Update is called once per frame
	void Update () {
		if(DataHolder.gFeedbackValues.ContainsKey(OSCMapping.SESSION_TIME))
		{
			System.TimeSpan timeSpan = System.TimeSpan.FromSeconds(DataHolder.gFeedbackValues[OSCMapping.SESSION_TIME]);
			if(timeSpan.Hours > 0)
				this.GetComponent<Text>().text = string.Format("{0:D2}:{1:D2}:{2:D2}", timeSpan.Hours, timeSpan.Minutes, timeSpan.Seconds);
			else
				this.GetComponent<Text>().text = string.Format("{1:D2}:{2:D2}",timeSpan.Hours, timeSpan.Minutes, timeSpan.Seconds);
		}
	}
}
