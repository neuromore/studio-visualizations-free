using UnityEngine;

public class GUIText {

	private Color color = Color.white;

	private float size = 1f;
	private string text = "";

	public GUIText(Color color, float size, string text){
		this.color = color;
		this.size = size;
		this.text = text;
	}
	
	public string getText(){return this.text;}
	public Color getColor(){return this.color;}
	public float getSize(){return this.size;}
}
