using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class motionImage : MonoBehaviour {
	enum Fade{IN,OUT}
	private Fade fade;
	//Imagen precargargada
	private Sprite imagenLoad;

	public float Velocidad = 2f;
	[Range(0f,1f)]public float Transparencia=0.95f;
	public Image image;

	private static bool dibuja=false,loadDraw=false;

	void Update () {
		if (Input.GetKeyDown (KeyCode.Z)) {
			fadeIn ();
		}
		if (Input.GetKeyDown (KeyCode.X)) {
			fadeOut ();
		}
	}

	public void fadeIn(){
		fade = Fade.IN;
		dibuja = true;
		image.gameObject.SetActive (true);
		StartCoroutine (draw());
	}

	public void loadImage(Sprite load){
		imagenLoad = load;
		if (dibuja) {
			imagenLoad = load;
			loadDraw = true;
			fade = Fade.OUT;
		} else {
			image.sprite = load;
			fadeIn ();
		}
	}

	public void fadeOut(){
		fade = Fade.OUT;
		dibuja = true;
		StartCoroutine (draw());
	}
	IEnumerator draw(){
		while (dibuja) {
			//Switch se encarga de dibujar la imagen
			switch (fade) {
			case Fade.IN:
				image.color = Color.Lerp (image.color, Color.white, Time.deltaTime * Velocidad);
				if (image.color.a > Transparencia)
					dibuja = false;
				break;
			case Fade.OUT:
				image.color = Color.Lerp (image.color, Color.clear, Time.deltaTime * Velocidad);				
				if (image.color.a < 0.05f) {
					if (loadDraw) {
						image.sprite=imagenLoad;
						loadDraw = false;
						fade = Fade.IN;
					} else {
						image.gameObject.SetActive (false);
						dibuja = false;
					}
				}
				break;
			}
			yield return new WaitForSeconds (Time.deltaTime);
		}
	}

}
