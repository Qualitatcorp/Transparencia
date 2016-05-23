using UnityEngine;
using System.Collections;

public class imageCollider : MonoBehaviour {

	public GameObject imageController;
	public Sprite imagen;
	public SphereCollider colicion;
	private motionImage mI;

	void Start () {
		mI = imageController.GetComponent<motionImage> ();
//		colicion.enabled = true;
	}

//	void Update () {
//	
//	}
	void OnTriggerEnter(Collider other){
//		mI.fadeIn ();
		mI.loadImage(imagen);

		colicion.enabled = true;
		print ("Entro"+other.gameObject.name);
	}
	void OnTriggerExit(Collider other){
		mI.fadeOut ();
		print ("Salio"+other.gameObject.name);
	}
}
