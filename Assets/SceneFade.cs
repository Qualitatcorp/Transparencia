using UnityEngine;
using System.Collections;

public class SceneFade : MonoBehaviour {

	public enum State{onScene,inProgress,offScene};
	private enum Fade{IN,OUT};

	[Range(0,100)]public float tiempo= 1f;
	public Texture textura = null;

	public static State state=State.onScene;
	private Fade fade;
	private Color fadeColor;

	public void fadeIn(){
		fadeColor = Color.black;
		fade = Fade.IN;
		state = State.inProgress;
	}
	public void fadeOut(){
		fadeColor = Color.clear;
		fade = Fade.OUT;
		state = State.inProgress;
	}

	void OnGUI () {
		if (state!=State.onScene) {
			if (fade == Fade.IN) {
				fadeColor=Color.Lerp (fadeColor, Color.clear, Time.deltaTime/tiempo);
			} else {
				fadeColor=Color.Lerp (fadeColor, Color.black, Time.deltaTime/tiempo);
			}
			GUI.color = fadeColor;
			GUI.DrawTexture (new Rect (0, 0, Screen.width, Screen.height), textura);

			//Liberacion de Estados
			if (fade==Fade.OUT&&fadeColor.a > 0.95f) {
				fadeColor = Color.black;
				state = State.offScene;
			}
			if (fade==Fade.IN&&fadeColor.a < 0.05f) {
				fadeColor = Color.black;
				state = State.onScene;
			}
		}
	}
}
