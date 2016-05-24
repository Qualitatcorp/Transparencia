using UnityEngine;
using System.Collections;

public class activador : MonoBehaviour {

	private motionCamera mC;
	private int contador = 0;
	private int numeroEstaciones;
	private bool paso;
	public Transform otrosTransparentes;
	public bool normalidad = false;
	public bool finGrupo = false;



	void Start () {

		mC = this.gameObject.GetComponent<motionCamera> ();
		numeroEstaciones = this.gameObject.GetComponent<motionCamera> ().Lugares.Length;
		//print (numeroEstaciones);

	}



	void Update(){


		if (Input.GetKeyUp (KeyCode.N) && paso) {//si presiono boton n pasaa la siguiente estacion y hace facelerping



			mC.Estacion = contador;
			mC.estado = motionCamera.Estado.Movimiento;
			contador++;

			if (contador == 1) {//en la primera iteracion hace trasnparente todos los demas elementos
				foreach (Transform joint in otrosTransparentes) {


//					joint.gameObject.GetComponent<lerptrasnparente> ().RenderTransON(joint);
//
//
//					joint.gameObject.GetComponent<lerptrasnparente> ().lerpingON ();
					//print(joint.gameObject.name);
				}
			}

			paso = false;
			//print (contador);
			StartCoroutine (nextGrupo ());




			if (contador==numeroEstaciones+1) {//cuando termina de pasar por todas las estaciones vuelven a su estado normal todos los demas elementos

				print ("ENTRO FINALIZANDO");
				this.GetComponent<FadeLerping>().finalizando(otrosTransparentes);
				//StartCoroutine (restaura ());
				foreach (Transform joint in otrosTransparentes) {
//
					//joint.gameObject.GetComponent<lerptrasnparente> ().RenderTransOFF(joint);
//
//					joint.gameObject.GetComponent<Renderer> ().material.SetFloat ("_Mode", 0);
//					joint.gameObject.GetComponent<Renderer> ().material.SetInt ("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);
//					joint.gameObject.GetComponent<Renderer> ().material.SetInt ("_DstBlend", (int)UnityEngine.Rendering.BlendMode.Zero);
//					joint.gameObject.GetComponent<Renderer> ().material.SetInt ("_ZWrite", 1);
//					joint.gameObject.GetComponent<Renderer> ().material.DisableKeyword ("_ALPHATEST_ON");
//					joint.gameObject.GetComponent<Renderer> ().material.DisableKeyword ("_ALPHABLEND_ON");
//					joint.gameObject.GetComponent<Renderer> ().material.DisableKeyword ("_ALPHAPREMULTIPLY_ON");
//					joint.gameObject.GetComponent<Renderer> ().material.renderQueue = -1;
//
				//	joint.gameObject.GetComponent<lerptrasnparente> ().lerpingOFF ();
//					//				
//					//				print (normalidad);
//
				}
				normalidad = true;
				this.GetComponent<FadeLerping> ().todosGruposFadeLerping ();

			}


		} 


		if (Input.GetKeyUp (KeyCode.M) && paso) {//si presiono boton n pasaa la siguiente estacion y hace facelerping


			if (contador == 0) {//en la primera iteracion hace trasnparente todos los demas elementos
				foreach (Transform joint in otrosTransparentes) {

//					joint.gameObject.GetComponent<MeshRenderer> ().material.SetFloat("_Mode", 3.0f);
//					joint.gameObject.GetComponent<lerptrasnparente> ().RenderTransON(joint);
//
//					joint.gameObject.GetComponent<Renderer> ().material.SetFloat ("_Mode", 2);
//					joint.gameObject.GetComponent<Renderer> ().material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
//					joint.gameObject.GetComponent<Renderer> ().material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
//					joint.gameObject.GetComponent<Renderer> ().material.SetInt("_ZWrite", 0);
//					joint.gameObject.GetComponent<Renderer> ().material.DisableKeyword("_ALPHATEST_ON");
//					joint.gameObject.GetComponent<Renderer> ().material.EnableKeyword("_ALPHABLEND_ON");
//					joint.gameObject.GetComponent<Renderer> ().material.DisableKeyword("_ALPHAPREMULTIPLY_ON");
//					joint.gameObject.GetComponent<Renderer> ().material.renderQueue = 3000;
//
//					joint.gameObject.GetComponent<lerptrasnparente> ().lerpingON ();
					//print(joint.gameObject.name);
				}
				//contador = 1;
			}
			finGrupo = this.gameObject.GetComponent<FadeLerping> ().finGrupo;
			if (finGrupo == true) {
				contador++;
			}

			mC.Estacion = contador;
			mC.estado = motionCamera.Estado.Movimiento;
			StartCoroutine (nextHijo ());

			print (contador);


			//print (contador);
		if (contador==numeroEstaciones) {//cuando termina de pasar por todas las estaciones vuelven a su estado normal todos los demas elementos
				
				print ("ENTRO FINALIZANDO");
				this.GetComponent<FadeLerping> ().finalizando (otrosTransparentes);
				//StartCoroutine (restaura ());
//				foreach (Transform joint in otrosTransparentes) {
//
//					//joint.gameObject.GetComponent<lerptrasnparente> ().RenderTransOFF(joint);
//
////					joint.gameObject.GetComponent<Renderer> ().material.SetFloat ("_Mode", 0);
////					joint.gameObject.GetComponent<Renderer> ().material.SetInt ("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);
////					joint.gameObject.GetComponent<Renderer> ().material.SetInt ("_DstBlend", (int)UnityEngine.Rendering.BlendMode.Zero);
////					joint.gameObject.GetComponent<Renderer> ().material.SetInt ("_ZWrite", 1);
////					joint.gameObject.GetComponent<Renderer> ().material.DisableKeyword ("_ALPHATEST_ON");
////					joint.gameObject.GetComponent<Renderer> ().material.DisableKeyword ("_ALPHABLEND_ON");
////					joint.gameObject.GetComponent<Renderer> ().material.DisableKeyword ("_ALPHAPREMULTIPLY_ON");
////					joint.gameObject.GetComponent<Renderer> ().material.renderQueue = -1;
//
//				//	joint.gameObject.GetComponent<lerptrasnparente> ().lerpingOFF ();
//					//				
//					//				print (normalidad);
//
//				}
				normalidad = true;
				print ("normalidadaaaa");
				this.GetComponent<FadeLerping> ().todosHijosFadeLerping ();

			}

		//	paso = false;
		} 

		paso = true;
	
	}



	IEnumerator nextGrupo (){
		

		//GameObject.Find ("prueba nueva").GetComponentInChildren<lerptrasnparente> ().lerpingON ();
		this.GetComponent<FadeLerping> ().todosGruposFadeLerping ();
		yield return new WaitForSeconds(1);
	}
	IEnumerator nextHijo (){
		
	

		//GameObject.Find ("prueba nueva").GetComponentInChildren<lerptrasnparente> ().lerpingON ();
		this.GetComponent<FadeLerping> ().todosHijosFadeLerping ();
		yield return new WaitForSeconds(1);
	}

//	IEnumerator restaura(){
//		
//
//		foreach (Transform actual in otrosTransparentes)
//			actual.gameObject.GetComponent<lerptrasnparente> ().lerpingOFF ();
//
//		yield return new WaitForSeconds(1.5f);
//
//		foreach (Transform actual in otrosTransparentes){
//			if (actual.tag != "no") {
//				actual.gameObject.GetComponent<Renderer> ().material.SetFloat ("_Mode", 0);
//				actual.gameObject.GetComponent<Renderer> ().material.SetInt ("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);
//				actual.gameObject.GetComponent<Renderer> ().material.SetInt ("_DstBlend", (int)UnityEngine.Rendering.BlendMode.Zero);
//				actual.gameObject.GetComponent<Renderer> ().material.SetInt ("_ZWrite", 1);
//				actual.gameObject.GetComponent<Renderer> ().material.DisableKeyword ("_ALPHATEST_ON");
//				actual.gameObject.GetComponent<Renderer> ().material.DisableKeyword ("_ALPHABLEND_ON");
//				actual.gameObject.GetComponent<Renderer> ().material.DisableKeyword ("_ALPHAPREMULTIPLY_ON");
//				actual.gameObject.GetComponent<Renderer> ().material.renderQueue = -1;
//			}
//		}
//		print ("a");
//	}
}

