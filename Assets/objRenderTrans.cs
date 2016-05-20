using UnityEngine;
using System.Collections;

public class objRenderTrans : MonoBehaviour {

	//public Transform padre;
	public Shader shader1;
	public Shader shader2;
	//public Shader shader2;

	void Awake(){
	
	 
	}

	void Update(){
		//print ("Trasparencia otros hijos");


		if (Input.GetKeyUp (KeyCode.P)) {

			lerptrasnparente[] elementos = FindObjectsOfType (typeof(lerptrasnparente)) as lerptrasnparente[];


			foreach (lerptrasnparente actual in elementos) {
				actual.gameObject.GetComponent<Renderer> ().material.shader = shader1;
				//actual.gameObject.GetComponent<Renderer> ().material.SetColor ("_Color", Color.white);
				//actual.gameObject.GetComponent<MeshRenderer> ().material.SetFloat ("_Mode", 2.0f);
				//print (actual.name);
				//actual.gameObject.GetComponent<MeshRenderer> ().material.SetFloat("_Mode", 3.0f);
				//actual.gameObject.GetComponent<MeshRenderer> ().material.SetFloat("_EmissionMap", 0.5f);
				//actual.GetComponent<lerptrasnparente> ().lerpingON ();
				actual.gameObject.GetComponent<Renderer> ().material.SetFloat("_Mode", 2);
				actual.gameObject.GetComponent<Renderer> ().material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
				actual.gameObject.GetComponent<Renderer> ().material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
				actual.gameObject.GetComponent<Renderer> ().material.SetInt("_ZWrite", 0);
				actual.gameObject.GetComponent<Renderer> ().material.DisableKeyword("_ALPHATEST_ON");
				actual.gameObject.GetComponent<Renderer> ().material.EnableKeyword("_ALPHABLEND_ON");
				actual.gameObject.GetComponent<Renderer> ().material.DisableKeyword("_ALPHAPREMULTIPLY_ON");
				actual.gameObject.GetComponent<Renderer> ().material.renderQueue = 3000;
				actual.gameObject.GetComponent<lerptrasnparente> ().lerpingON ();
			}
		}
	

			if (Input.GetKeyUp (KeyCode.O)) {

				lerptrasnparente[] elementos2 = FindObjectsOfType (typeof(lerptrasnparente)) as lerptrasnparente[];



					
				
			foreach (lerptrasnparente actual in elementos2) {
				actual.gameObject.GetComponent<lerptrasnparente> ().lerpingOFF ();
				//actual.gameObject.GetComponent<Renderer> ().material.shader = shader2;
				//actual.gameObject.GetComponent<Renderer> ().material.SetColor ("_Color", Color.white);
				//actual.gameObject.GetComponent<MeshRenderer> ().material.SetFloat ("_Mode", 3.0f);
				//print (actual.name);
				//actual.gameObject.GetComponent<MeshRenderer> ().material.SetFloat("_Mode", 3.0f);

			}
		}




		


	}
}
