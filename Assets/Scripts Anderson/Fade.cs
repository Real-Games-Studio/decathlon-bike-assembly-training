using UnityEngine;
using System.Collections;

public class Fade : MonoBehaviour {

	public Texture2D textura;
	public float tempoFade = 0.0f;

	//private int nextLevel = 1;

	void OnGUI(){

		GUI.depth = 4;
		Color color = Color.white;
		color.a = Mathf.Lerp (1.0f, 0.0f, (Time.time - tempoFade));
		GUI.color = color;
		GUI.DrawTexture (new Rect(0,0,Screen.width, Screen.height), textura);
	}

	void OnLevelWasLoaded()
	{
		tempoFade = Time.time;
	}

}
