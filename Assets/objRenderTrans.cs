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
			print(elementos.Length);
			int i;
			float[] renderingMode = new float[elementos.Length];

			for (i = 0; i < elementos.Length; i++) {
				
				renderingMode [i] = elementos [i].gameObject.GetComponent<Renderer> ().material.GetFloat ("_Mode");
				print (renderingMode [i]);
			}


			foreach (lerptrasnparente actual in elementos) {
				//actual.gameObject.GetComponent<Renderer> ().material.shader = shader1;
				//actual.gameObject.GetComponent<Renderer> ().material.SetColor ("_Color", Color.white);
				//actual.gameObject.GetComponent<MeshRenderer> ().material.SetFloat ("_Mode", 2.0f);
				//print (actual.name);
				//actual.gameObject.GetComponent<MeshRenderer> ().material.SetFloat("_Mode", 3.0f);
				//actual.gameObject.GetComponent<MeshRenderer> ().material.SetFloat("_EmissionMap", 0.5f);
				//actual.GetComponent<lerptrasnparente> ().lerpingON ();
				//actual.gameObject.GetComponent<Renderer> ().material.SetFloat("_Mode", 2);
				actual.gameObject.GetComponent<Renderer> ().material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
				actual.gameObject.GetComponent<Renderer> ().material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
				actual.gameObject.GetComponent<Renderer> ().material.SetInt("_ZWrite", 0);
				actual.gameObject.GetComponent<Renderer> ().material.DisableKeyword("_ALPHATEST_ON");
				actual.gameObject.GetComponent<Renderer> ().material.EnableKeyword("_ALPHABLEND_ON");
				actual.gameObject.GetComponent<Renderer> ().material.DisableKeyword("_ALPHAPREMULTIPLY_ON");
				actual.gameObject.GetComponent<Renderer> ().material.renderQueue = 3000;
				actual.gameObject.GetComponent<lerptrasnparente> ().lerpingON ();

				if (actual.gameObject.GetComponent<Renderer> ().material.GetFloat ("_Mode") == 2)
					print (actual.name);

			}
		}
	

			if (Input.GetKeyUp (KeyCode.O)) {

				lerptrasnparente[] elementos2 = FindObjectsOfType (typeof(lerptrasnparente)) as lerptrasnparente[];



					
				
			foreach (lerptrasnparente actual in elementos2) {

				StartCoroutine (restaura ());
			

			}


		

		}



	}

	IEnumerator restaura(){
		lerptrasnparente[] elementos2 = FindObjectsOfType (typeof(lerptrasnparente)) as lerptrasnparente[];

		foreach (lerptrasnparente actual in elementos2)
			actual.gameObject.GetComponent<lerptrasnparente> ().lerpingOFF ();

		yield return new WaitForSeconds(0.5f);

		foreach (lerptrasnparente actual in elementos2){
			if (actual.tag != "no") {
				actual.gameObject.GetComponent<Renderer> ().material.SetFloat ("_Mode", 0);
				actual.gameObject.GetComponent<Renderer> ().material.SetInt ("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);
				actual.gameObject.GetComponent<Renderer> ().material.SetInt ("_DstBlend", (int)UnityEngine.Rendering.BlendMode.Zero);
				actual.gameObject.GetComponent<Renderer> ().material.SetInt ("_ZWrite", 1);
				actual.gameObject.GetComponent<Renderer> ().material.DisableKeyword ("_ALPHATEST_ON");
				actual.gameObject.GetComponent<Renderer> ().material.DisableKeyword ("_ALPHABLEND_ON");
				actual.gameObject.GetComponent<Renderer> ().material.DisableKeyword ("_ALPHAPREMULTIPLY_ON");
				actual.gameObject.GetComponent<Renderer> ().material.renderQueue = -1;
			}
		}

	}
}
