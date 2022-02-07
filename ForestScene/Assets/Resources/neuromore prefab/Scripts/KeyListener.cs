using UnityEngine;
using System.Collections;

public class KeyListener : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void OnGUI () {

		// exit on ESC key
		if (Input.GetKey(KeyCode.Escape)) 
			Application.Quit();

		// Take Screenshots with print key
		if (Event.current.type == EventType.KeyUp && Event.current.keyCode == KeyCode.SysReq)
		{
			string path = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Desktop);
			string[] s = Application.dataPath.Split('/');
			string projectName = s[s.Length - 2];
			string filePathName = path+"\\"+projectName+"_"+System.DateTime.Now.ToString().Replace("/","-").Replace(":","-").Replace(" ","_")+".png";
			ScreenCapture.CaptureScreenshot(filePathName);
			Debug.Log("Saved Screenshot: "+filePathName);
		}
	}
}
