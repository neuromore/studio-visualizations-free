using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class FadeIn : MonoBehaviour {

	private RawImage rawImage;

	private float timer = 0f; // duration in seconds transition is running
	public bool startFade= false;
	private bool  fadingRunning = false;
	public float fadingDuration; // duration in seconds
	private Color fadingFrom;
	private Color fadingTo;

	// Use this for initialization
	void Start ()
	{	
		rawImage = transform.GetComponent<RawImage>();
		rawImage.color = Color.black;
		rawImage.uvRect = new Rect(0f, 0f, Screen.width, Screen.height);
	}

	void Awake()
	{
		rawImage = transform.GetComponent<RawImage>();
		rawImage.color = Color.black;
		rawImage.uvRect = new Rect(0f, 0f, Screen.width, Screen.height);
		startFading(Color.black, Color.clear);
	}

	// Update is called once per frame
	void Update ()
	{
		if(fadingRunning)
		{
			fading();
		}
		if(startFade)
			startFading(Color.black, Color.clear);
	}

	public void startFading(Color a, Color b)
	{
		startFade = false;
		// Set the texture so that it is the the size of the screen and covers it.
		this.fadingFrom = a;
		this.fadingTo = b;
		rawImage.color = this.fadingFrom;
		this.fadingRunning = true;
	}

	private void fading ()
	{
		this.timer += Time.deltaTime;
		if(this.timer > this.fadingDuration)
		{
			this.timer = 0;
			this.rawImage.color = this.fadingTo;
			this.fadingRunning = false;
			DestroyImmediate(this.gameObject.transform.parent.gameObject);
			return;
		}
		rawImage.color = Color.Lerp(this.fadingFrom, this.fadingTo, (this.timer/this.fadingDuration));
	}
}
