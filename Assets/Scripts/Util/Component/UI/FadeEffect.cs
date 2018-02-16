using UnityEngine;

public class FadeEffect : MonoBehaviour {

	public float In()
	{
		alpha = 1;
		direction = FadeDirection.In;
		return speed;
	}

	public float Out()
	{
		alpha = 0;
		direction = FadeDirection.Out;
		return speed;
	}
	
	void OnGUI()
	{
		// fade out/in the alpha value using a direction, a speed and Time.deltaTime to convert the operation to seconds
		alpha += (int)direction * speed * Time.deltaTime;

		// force (clamp) the number to be between 0 and 1 because GUI.color uses Alpha values between 0 and 1
		alpha = Mathf.Clamp01(alpha);

		// set color of our GUI (in this case our texture). All color values remain the same & the Alpha is set to the alpha variable
		GUI.color = new Color (GUI.color.r, GUI.color.g, GUI.color.b, alpha);
		GUI.depth = drawDepth;																// make the black texture render on top (drawn last)
		GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), fadeOutTexture);		// draw the texture to fit the entire screen area
	}


	enum FadeDirection { In = -1, Out = 1}

	#region Attributes

	[SerializeField] Texture2D fadeOutTexture;	// the texture that will overlay the screen. This can be a black image or a loading graphic

	[SerializeField] float speed = 0.8f;		// the fading speed

	int drawDepth = -1000;		// the texture's order in the draw hierarchy: a low number means it renders on top
	
	float alpha;			// the texture's alpha value between 0 and 1
	
	FadeDirection direction = FadeDirection.In;

	#endregion
}
