using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FadeLerping : MonoBehaviour {
	public Transform[] padres;
	private int hijos_restantes;
	private bool parar = true;
	int index;
	int siguiente_hijo=0;
	public string grupo=null;
	public bool continuo;
	private int next=0;
	private int contador;
	int i=0;
	private bool normal = false;
	public bool finGrupo = false;



	void Awake(){


		//print ("hijos: "+hijos_restantes+" del grupo: "+grupo);

		string [] nombre_grupos = new string[padres.Length];
		nombre_grupos = grupos_nombre (padres);

//		foreach (string a in nombre_grupos)
//			print (a);

		//print (nombres_grupos);
	//	hijosTotal(padres);
		

	}

	void Update(){



		if (Input.GetKeyUp (KeyCode.Alpha1)) {

//			hijos_restantes = indexPadre (grupo);
//			LerpingHijos (grupo);
			todosHijosFadeLerping();
		
		}
//si no selecciono continuo y se define un grupo hace FaceLerping por cada hijo del grupo definido correspondiente al grupo del padre
		if (Input.GetKeyUp (KeyCode.Alpha2)) {


			LerpingHijos (grupo);
		}


		if (Input.GetKeyUp (KeyCode.Alpha3)) {


			todosGruposFadeLerping ();


		}
//si no seleccino continuo y se define un grupo hara FaceLerping al grupo completo

		if (Input.GetKeyUp (KeyCode.Alpha4)) {


			//if(grupo!=null)
					LerpingPadres (grupo);

		}
	}


	public string[] grupos_nombre(Transform[] padres){
		string [] grupos = new string[padres.Length];
		for (i = 0; i < padres.Length; i++) {
			grupos[i]=padres [i].tag;
			//print (cantidad [i]);
		}
		return grupos;
	}

	public int[] numHijos(Transform[] padres){//cantidad de hijos x Padres
		int [] cantidad_hijosXpadre = new int[padres.Length];
		for (i = 0; i < padres.Length; i++) {
			cantidad_hijosXpadre[i]=padres [i].transform.childCount;
			//print (cantidad [i]);
		}
		return cantidad_hijosXpadre;
	}

	public int indexPadre(string grupo){

		int cant_hijos_tag=0;

		//print (grupo);
		foreach (Transform padre in padres) {
			foreach (Transform hijo in padre){
				if (hijo.tag == grupo)
					cant_hijos_tag++;

			}

				

		}
	//	print (cant_hijos_tag);
		return cant_hijos_tag;
	}

//	public int hijosTotal(Transform padres){
//		int cant_hijos;
//		foreach (Transform padre in padres) {
//			foreach (Transform hijo in padre){
//
//					cant_hijos++;
//
//			}
//
//	}
//		return cant_hijos;
//	}


	void LerpingHijos(string grupo){
		

		List<GameObject> children = new List<GameObject>();//crea lista de hijos
		int[] cantidad_hijos = numHijos (padres);

		foreach (Transform padre in padres) {//añade todos los hijos de cada padre a la lista anterior
			foreach (Transform hijo in padre){
			children.Add (hijo.gameObject);
						    
			}
		}

		parar = false;

		for (i = siguiente_hijo; i < children.Count; i++) {//revisa cada hijo en la lista
			
//			if(children [i].tag != "grupo1" && children [i].transform.root.name != "padre1")
//				print ("no lo muestra y tiene index: " +i);
				

			index = i;

			if (children [i].tag == grupo) {//consulta si el tag del hijo en la lista corresponde al grupo buscado
													

			//	if (hijos_restantes != 0 && parar == false) {
				if (parar == false) {
				//	print ("ELEMENTO ACTIVADO: " + children [index].name);
					//children [index].GetComponent<lerptrasnparente> ().enabled== false;

					children [index].gameObject.GetComponent<Renderer> ().material.SetFloat ("_Mode", 0.0f);
					children [index].gameObject.GetComponent<Renderer> ().material.SetInt ("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);
					children [index].gameObject.GetComponent<Renderer> ().material.SetInt ("_DstBlend", (int)UnityEngine.Rendering.BlendMode.Zero);
					children [index].gameObject.GetComponent<Renderer> ().material.SetInt ("_ZWrite", 1);
					children [index].gameObject.GetComponent<Renderer> ().material.DisableKeyword ("_ALPHATEST_ON");
					children [index].gameObject.GetComponent<Renderer> ().material.DisableKeyword ("_ALPHABLEND_ON");
					children [index].gameObject.GetComponent<Renderer> ().material.DisableKeyword ("_ALPHAPREMULTIPLY_ON");
					children [index].gameObject.GetComponent<Renderer> ().material.renderQueue = -1;

					children [index].gameObject.GetComponent<lerptrasnparente> ().lerpingOFF ();//si la condicion del grupo es correcta aplica lerpingOFF
					parar = true;
					//hijos_restantes--;
					contador++;
					//print (contador);

					//print ("hijos restantes 2: "+hijos_restantes);
					siguiente_hijo = index + 1;
					print ("hijos : "+hijos_restantes+" contador: "+contador);
					if (contador == hijos_restantes) {//si el contador de hijos en el padre n es igual a los hijos restantes revisados del padre n
						                              //concluye el ciclo de revision para el grupo n en el padre n
						next++;
						contador = 0;// si la condicion se cumple se resetea el contador para seguir con los hijos del siguiente padre n

						print ("termino la secuencia del "+grupo);
						finGrupo = true;


					}

					//print (children [i].transform.root.name);
				} 
			}	
		}

		for (i = 0; i <siguiente_hijo-1; i++) {
					
			//	print ("antes-> "+children [i].name);joint.gameObject.GetComponent<Renderer> ().material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
			children[i].gameObject.GetComponent<Renderer> ().material.SetFloat ("_Mode", 2);
			children[i].gameObject.GetComponent<Renderer> ().material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
			children[i].gameObject.GetComponent<Renderer> ().material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
			children[i].gameObject.GetComponent<Renderer> ().material.SetInt("_ZWrite", 0);
			children[i].gameObject.GetComponent<Renderer> ().material.DisableKeyword("_ALPHATEST_ON");
			children[i].gameObject.GetComponent<Renderer> ().material.EnableKeyword("_ALPHABLEND_ON");
			children[i].gameObject.GetComponent<Renderer> ().material.DisableKeyword("_ALPHAPREMULTIPLY_ON");
			children[i].gameObject.GetComponent<Renderer> ().material.renderQueue = 3000;
			children[i].gameObject.GetComponent<lerptrasnparente> ().lerpingON();
					}

		for (i = siguiente_hijo; i < children.Count; i++) {
			//print ("no lo muestra y tiene index: " + i);
			//print ("despues-> "+children [i].name);
			children[i].gameObject.GetComponent<Renderer> ().material.SetFloat ("_Mode", 2);
			children[i].gameObject.GetComponent<Renderer> ().material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
			children[i].gameObject.GetComponent<Renderer> ().material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
			children[i].gameObject.GetComponent<Renderer> ().material.SetInt("_ZWrite", 0);
			children[i].gameObject.GetComponent<Renderer> ().material.DisableKeyword("_ALPHATEST_ON");
			children[i].gameObject.GetComponent<Renderer> ().material.EnableKeyword("_ALPHABLEND_ON");
			children[i].gameObject.GetComponent<Renderer> ().material.DisableKeyword("_ALPHAPREMULTIPLY_ON");
			children[i].gameObject.GetComponent<Renderer> ().material.renderQueue = 3000;
			children [i].gameObject.GetComponent<lerptrasnparente> ().lerpingON();
		}

	
	}

public void LerpingPadres (string grupo)
	{

		StartCoroutine (lerpingPadres (padres,grupo));

//		foreach (Transform padre in padres) {
//			foreach (Transform hijo in padre) {
//
//				if (hijo.gameObject.tag == grupo) {
//					
//						
////					print (hijo.name);
////					hijo.gameObject.GetComponent<lerptrasnparente> ().RenderTransOFF (hijo);
////					hijo.gameObject.GetComponent<lerptrasnparente> ().lerpingOFF ();
//				} else {
////					hijo.gameObject.GetComponent<lerptrasnparente> ().RenderTransON(hijo);
////					hijo.gameObject.GetComponent<lerptrasnparente> ().lerpingON ();
//				}
//			}
//		}

//		lerptrasnparente[] elementos = FindObjectsOfType (typeof(lerptrasnparente)) as lerptrasnparente[];
//
//
//		foreach (lerptrasnparente actual in elementos) {
//
//			if (actual.gameObject.tag == grupo) {
//				//print(actual.GetType ());
////				actual.gameObject.GetComponent<Renderer> ().material.SetFloat ("_Mode", 0);
////				actual.gameObject.GetComponent<Renderer> ().material.SetInt ("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);
////				actual.gameObject.GetComponent<Renderer> ().material.SetInt ("_DstBlend", (int)UnityEngine.Rendering.BlendMode.Zero);
////				actual.gameObject.GetComponent<Renderer> ().material.SetInt ("_ZWrite", 1);
////				actual.gameObject.GetComponent<Renderer> ().material.DisableKeyword ("_ALPHATEST_ON");
////				actual.gameObject.GetComponent<Renderer> ().material.DisableKeyword ("_ALPHABLEND_ON");
////				actual.gameObject.GetComponent<Renderer> ().material.DisableKeyword ("_ALPHAPREMULTIPLY_ON");
////				actual.gameObject.GetComponent<Renderer> ().material.renderQueue = -1;
//				actual.gameObject.GetComponent<lerptrasnparente> ().RenderTransOFF(actual);
//				actual.lerpingOFF ();
//			}
//			else {
////				actual.gameObject.GetComponent<Renderer> ().material.SetFloat ("_Mode", 2);
////				actual.gameObject.GetComponent<Renderer> ().material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
////				actual.gameObject.GetComponent<Renderer> ().material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
////				actual.gameObject.GetComponent<Renderer> ().material.SetInt("_ZWrite", 0);
////				actual.gameObject.GetComponent<Renderer> ().material.DisableKeyword("_ALPHATEST_ON");
////				actual.gameObject.GetComponent<Renderer> ().material.EnableKeyword("_ALPHABLEND_ON");
////				actual.gameObject.GetComponent<Renderer> ().material.DisableKeyword("_ALPHAPREMULTIPLY_ON");
////				actual.gameObject.GetComponent<Renderer> ().material.renderQueue = 3000;
//				actual.gameObject.GetComponent<lerptrasnparente> ().RenderTransON(actual);
//				actual.lerpingON ();
//			}
//		}
	} 

public void todosHijosFadeLerping(){
		//si selecciono continuo hace FaceLerping por cada hijo de todos los padres que tengan hijos con componente lerpingtransparente
		//es un FaceLerping Continuo a todos los elementos
		normal = this.gameObject.GetComponent<activador> ().normalidad;
		finGrupo = false;
		if (continuo == true && next < padres.Length) {
			grupo = padres [next].tag;//se define el grupo a trabajar en primera instancia
			//print (grupo);
			hijos_restantes = indexPadre (grupo);//indica la cantidad de hijos pertenecientes a un grupo determinado
			LerpingHijos (grupo);//realiza facelerping a los hijos de un padre determinado

		}

		if(normal){

		//	StartCoroutine (terminaFade ());
			finalizando(padres);


		}

	}

public void todosGruposFadeLerping(){
	
		normal = this.gameObject.GetComponent<activador> ().normalidad;
		print (normal);
		//si selecciono continuo hara FaceLerping entre todos los padres grupos
		if (continuo == true && next<padres.Length) {

			grupo = padres [next].tag;

			LerpingPadres (grupo);
			next++;
			print ("next: "+next);
		}
	


		if(normal){
			
			//StartCoroutine (terminaFade ());
			finalizando(padres);

	
			}
}

	public void finalizando(Transform[] elementos){
	
	
		StartCoroutine (terminaFade (elementos));
	}


	IEnumerator terminaFade (Transform[] elementos){

		//yield return new WaitForSeconds (1.5f);
		yield return new WaitForSeconds (0.8f);
		foreach (Transform padre in elementos) {
			foreach (Transform normalidad in padre) {
				
				normalidad.gameObject.GetComponent<lerptrasnparente> ().lerpingOFF ();
				//print(normalidad.name);
			}




		}
		yield return new WaitForSeconds (0.8f);

		foreach (Transform padre in elementos) {
			foreach (Transform normalidad in padre) {

				if (normalidad.tag != "no") {
					normalidad.gameObject.GetComponent<Renderer> ().material.SetFloat ("_Mode", 0);
					normalidad.gameObject.GetComponent<Renderer> ().material.SetInt ("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);
					normalidad.gameObject.GetComponent<Renderer> ().material.SetInt ("_DstBlend", (int)UnityEngine.Rendering.BlendMode.Zero);
					normalidad.gameObject.GetComponent<Renderer> ().material.SetInt ("_ZWrite", 1);
					normalidad.gameObject.GetComponent<Renderer> ().material.DisableKeyword ("_ALPHATEST_ON");
					normalidad.gameObject.GetComponent<Renderer> ().material.DisableKeyword ("_ALPHABLEND_ON");
					normalidad.gameObject.GetComponent<Renderer> ().material.DisableKeyword ("_ALPHAPREMULTIPLY_ON");
					normalidad.gameObject.GetComponent<Renderer> ().material.renderQueue = -1;
				}
			}


		}

	}


	public void finalizando(Transform elemento){


		StartCoroutine (terminaFade (elemento));
	}


	IEnumerator terminaFade (Transform elemento){

		//yield return new WaitForSeconds (1.5f);
		yield return new WaitForSeconds (0.8f);
		foreach (Transform padre in elemento) {


			padre.gameObject.GetComponent<lerptrasnparente> ().lerpingOFF ();
				//print(normalidad.name);
			}





		yield return new WaitForSeconds (0.8f);

		foreach (Transform padre in elemento) {
			

			if (padre.tag != "no") {
				padre.gameObject.GetComponent<Renderer> ().material.SetFloat ("_Mode", 0.0f);
				padre.gameObject.GetComponent<Renderer> ().material.SetInt ("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);
				padre.gameObject.GetComponent<Renderer> ().material.SetInt ("_DstBlend", (int)UnityEngine.Rendering.BlendMode.Zero);
				padre.gameObject.GetComponent<Renderer> ().material.SetInt ("_ZWrite", 1);
				padre.gameObject.GetComponent<Renderer> ().material.DisableKeyword ("_ALPHATEST_ON");
				padre.gameObject.GetComponent<Renderer> ().material.DisableKeyword ("_ALPHABLEND_ON");
				padre.gameObject.GetComponent<Renderer> ().material.DisableKeyword ("_ALPHAPREMULTIPLY_ON");
				padre.gameObject.GetComponent<Renderer> ().material.renderQueue = -1;

			}


		}
		print ("b");
	}


	IEnumerator lerpingPadres (Transform[] elemento, string grupo){

		//yield return new WaitForSeconds (1.5f);
		yield return new WaitForSeconds (0.1f);




		foreach (Transform padre in elemento) {
			foreach (Transform hijo in padre) {

				if (hijo.gameObject.tag == grupo) {


					//					print (hijo.name);
					//					hijo.gameObject.GetComponent<lerptrasnparente> ().RenderTransOFF (hijo);
					hijo.gameObject.GetComponent<lerptrasnparente> ().lerpingOFF ();
				} else {
					hijo.gameObject.GetComponent<lerptrasnparente> ().lerpingON ();
				}
			}
		}



		yield return new WaitForSeconds (0.1f);

		foreach (Transform padre in elemento) {
			foreach (Transform hijo in padre) {

				if (hijo.gameObject.tag == grupo) {


					//					print (hijo.name);
										hijo.gameObject.GetComponent<lerptrasnparente> ().RenderTransOFF (hijo);
					//hijo.gameObject.GetComponent<lerptrasnparente> ().lerpingOFF ();
				}else{
					hijo.gameObject.GetComponent<lerptrasnparente> ().RenderTransON (hijo);
				} 
			}
		}

		print ("b");
	}

//	IEnumerator atras(List<GameObject> children, int i){
//		children[i].gameObject.GetComponent<lerptrasnparente> ().lerpingON();
//	
//		yield return new WaitForSeconds (0.1);
//		children[i].gameObject.GetComponent<Renderer> ().material.SetFloat ("_Mode", 2);
//		children[i].gameObject.GetComponent<Renderer> ().material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
//		children[i].gameObject.GetComponent<Renderer> ().material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
//		children[i].gameObject.GetComponent<Renderer> ().material.SetInt("_ZWrite", 0);
//		children[i].gameObject.GetComponent<Renderer> ().material.DisableKeyword("_ALPHATEST_ON");
//		children[i].gameObject.GetComponent<Renderer> ().material.EnableKeyword("_ALPHABLEND_ON");
//		children[i].gameObject.GetComponent<Renderer> ().material.DisableKeyword("_ALPHAPREMULTIPLY_ON");
//		children[i].gameObject.GetComponent<Renderer> ().material.renderQueue = 3000;
//		children[i].gameObject.GetComponent<lerptrasnparente> ().lerpingON();
//	}
//
//	IEnumerator adelante(List<GameObject> children, int i){
//		children[i].gameObject.GetComponent<lerptrasnparente> ().lerpingOFF();
//
//		yield return new WaitForSeconds (0.1);
//		children[i].gameObject.GetComponent<Renderer> ().material.SetFloat ("_Mode", 2);
//		children[i].gameObject.GetComponent<Renderer> ().material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
//		children[i].gameObject.GetComponent<Renderer> ().material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
//		children[i].gameObject.GetComponent<Renderer> ().material.SetInt("_ZWrite", 0);
//		children[i].gameObject.GetComponent<Renderer> ().material.DisableKeyword("_ALPHATEST_ON");
//		children[i].gameObject.GetComponent<Renderer> ().material.EnableKeyword("_ALPHABLEND_ON");
//		children[i].gameObject.GetComponent<Renderer> ().material.DisableKeyword("_ALPHAPREMULTIPLY_ON");
//		children[i].gameObject.GetComponent<Renderer> ().material.renderQueue = 3000;
//		children[i].gameObject.GetComponent<lerptrasnparente> ().lerpingON();
//	}


}