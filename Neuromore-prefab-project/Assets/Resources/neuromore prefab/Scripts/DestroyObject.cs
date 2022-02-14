using UnityEngine;
using System.Collections;

public class DestroyObject : MonoBehaviour {

	public float timeToLive = 1f;
	private float timer = 0f;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		timer += Time.deltaTime;
		if(timer > timeToLive)
			DestroyImmediate(this.gameObject);
	}
}
