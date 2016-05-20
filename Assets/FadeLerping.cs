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
					children [index].gameObject.GetComponent<lerptrasnparente> ().lerpingOFF ();//si la confdicion del grupo es correcta aplica lerpingOFF
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
					
						//	print ("antes-> "+children [i].name);
			children [i].gameObject.GetComponent<lerptrasnparente> ().lerpingON();
					}

		for (i = siguiente_hijo; i < children.Count; i++) {
			//print ("no lo muestra y tiene index: " + i);
			//print ("despues-> "+children [i].name);
			children [i].gameObject.GetComponent<lerptrasnparente> ().lerpingON();
		}

	
	}

public void LerpingPadres (string grupo)
	{

		lerptrasnparente[] elementos = FindObjectsOfType (typeof(lerptrasnparente)) as lerptrasnparente[];


		foreach (lerptrasnparente actual in elementos) {

			if (actual.gameObject.tag == grupo)
				//print(actual.GetType ());
				actual.lerpingOFF ();

			else
				actual.lerpingON ();

		}
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

			StartCoroutine (terminaFade ());


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
			
			StartCoroutine (terminaFade ());

	
			}
}

	IEnumerator terminaFade (){

		yield return new WaitForSeconds(2);
		foreach(Transform padre in padres){
			foreach(Transform normalidad in padre )	{
				normalidad.gameObject.GetComponent<lerptrasnparente>().lerpingOFF();
				//print(normalidad.name);
			}
		}

	}
}