using UnityEngine;
using System.Collections;

public static class MathCore {

	//*****************************************************
	// Helper Functions : GUI helper functions
	//*****************************************************	

	// Returns size of meshes, gameobject must have childs with mesh renderer or skinned mesh renderer
	public static Vector2 GetScreenSizeInPixels(GameObject go, Camera camera)
	{	
		float minX = Mathf.Infinity;
		float minY = Mathf.Infinity;
		float maxX = -Mathf.Infinity;
		float maxY = -Mathf.Infinity;
		
		Transform t = go.transform;
		Bounds bounds = new Bounds();
		foreach(SkinnedMeshRenderer r in go.GetComponentsInChildren<SkinnedMeshRenderer>())
			bounds.Encapsulate(r.bounds);
		foreach(MeshRenderer r in go.GetComponentsInChildren<MeshRenderer>())
			bounds.Encapsulate(r.bounds);
		
		Vector3 v3Center = bounds.center;
		Vector3 v3Extents = bounds.extents;
		
		Vector3[] corners = new Vector3[8];	
		corners[0]  = new Vector3(v3Center.x - v3Extents.x, v3Center.y + v3Extents.y, v3Center.z - v3Extents.z);  // Front top left corner
		corners[1]  = new Vector3(v3Center.x + v3Extents.x, v3Center.y + v3Extents.y, v3Center.z - v3Extents.z);  // Front top right corner
		corners[2]  = new Vector3(v3Center.x - v3Extents.x, v3Center.y - v3Extents.y, v3Center.z - v3Extents.z);  // Front bottom left corner
		corners[3]  = new Vector3(v3Center.x + v3Extents.x, v3Center.y - v3Extents.y, v3Center.z - v3Extents.z);  // Front bottom right corner
		corners[4]  = new Vector3(v3Center.x - v3Extents.x, v3Center.y + v3Extents.y, v3Center.z + v3Extents.z);  // Back top left corner
		corners[5]  = new Vector3(v3Center.x + v3Extents.x, v3Center.y + v3Extents.y, v3Center.z + v3Extents.z);  // Back top right corner
		corners[6]  = new Vector3(v3Center.x - v3Extents.x, v3Center.y - v3Extents.y, v3Center.z + v3Extents.z);  // Back bottom left corner
		corners[7]  = new Vector3(v3Center.x + v3Extents.x, v3Center.y - v3Extents.y, v3Center.z + v3Extents.z);  // Back bottom right corner
		
		for (var i = 0; i < corners.Length; i++) {
			var corner = t.TransformPoint(corners[i]);
			corner = camera.WorldToScreenPoint(corner);
			if (corner.x > maxX) maxX = corner.x;
			if (corner.x < minX) minX = corner.x;
			if (corner.y > maxY) maxY = corner.y;
			if (corner.y < minY) minY = corner.y;
		}
		return new Vector2(maxX - minX, maxY - minY);
	}


	//*****************************************************
	// Helper Functions : Conversion
	//*****************************************************	

	// clamp function
	public static float Clamp(float value, float min, float max)
	{
		return (value < min) ? min : (value > max) ? max : value;
	}

	// returns value dependend on given range
	public static float SimpleLinearConversion(float x, float minInput, float maxInput, float minOutput, float maxOutput)
	{
		// get the input value and the input and output ranges as a float, convert if needed
		// clamp it so that the value is in the valid input range
		if (x < minInput)
			x = minInput;
		if (x > maxInput)
			x = maxInput;
		
		// apply the simple linear conversion
		float result;
		if (Mathf.Abs(maxInput - minInput) > 0.00001f)
			result = ( (x - minInput) / (maxInput - minInput) ) * (maxOutput - minOutput) + minOutput;
		else
			result = minOutput;
		
		return result;
	}

	//*****************************************************
	// Helper Functions : Speed Interpolation FadeIn/FadeOut
	//*****************************************************	

	// Sinus Ease In Out
	public static float SinusEaseInOut(float percentage)
	{
		return Mathf.Sin(percentage * Mathf.PI);
	}
	
	// Sinus Ease In
	public static float SinusEaseIn(float percentage)
	{
		return Mathf.Sin(percentage * (Mathf.PI/2.0f));
	}
	// Sinus Ease Out
	public static float SinusEaseOut(float percentage)
	{
		return Mathf.Sin((1.0f - percentage) * (Mathf.PI/2.0f));
	}
	// Sinus Interpolation, but separated in x parts. First and Last part uses sinus ease in and ease out	
	public static float SinusSplittedEaseInOut(float percentage, float parts)
	{
		if( percentage <= (1.0f/parts))
			return SinusEaseIn(percentage*(1.0f/parts));
		else if( percentage >= (1.0f - (1.0f/parts))) 
			return SinusEaseOut(percentage*(1.0f/parts));
		else 
			return 1.0f;
	}
	// Linear Ease In
	public static float LinearEaseIn(float percentage)
	{ 
		return percentage;
	}
	// Linear Ease Out
	public static float LinearEaseOut(float percentage)
	{ 
		return 1.0f - percentage;
	}
	// Quadratic Ease In
	public static float QuadEaseIn(float percentage)
	{ 
		return percentage * percentage;
	}
	// Quadratic Ease In
	public static float QuadEaseOut(float percentage)
	{ 
		return percentage*(2.0f - percentage);
	}
	// Quadratic Ease In Out
	public static float QuadEaseInOut(float percentage)
	{ 
		if(percentage < 0.5f) return QuadEaseIn(percentage / 0.5f);
		else return QuadEaseOut(percentage / 0.5f); 
	}

}
