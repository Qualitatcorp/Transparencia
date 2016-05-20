using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[RequireComponent(typeof(Camera))]
public class CamaraTargert : MonoBehaviour {
	private Camera cam;
	public LayerMask targetingLayerMask = -1;
	private float targetingRayLength = Mathf.Infinity;

	private ObjectController obAnterior;
	private bool banderaAnterior;

	public GameObject titulos;
	// Use this for initialization

	void Start () {
		cam = GetComponent<Camera>();
		if(titulos!=null)
			titulos.SetActive (false);
		
		if (Application.loadedLevelName == "Facil") {
			decidibles = decidiblesFacil;
		}
		if (Application.loadedLevelName == "Intermedio") {
			decidibles = decidiblesIntermedio;
		}
		if (Application.loadedLevelName == "Dificil") {
			decidibles = decidiblesDificil;
		}
		inicioRespuesta ();

		if (PlayerPrefs.HasKey ("ultimo_id"))
			PlayerPrefs.SetInt ("ultimo_id",0);
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.P)){
			printRespuestas();
		}
		if(Input.GetKeyDown(KeyCode.G)){
			guardarRestpuestas();
		}
		///////////////////////////////////////////// target raycast
		Transform targetTransform = null;
		if (cam != null) {
			RaycastHit hitInfo;
			Ray ray = cam.ViewportPointToRay(new Vector3(0.5F, 0.5F, 0));
			if (Physics.Raycast(ray, out hitInfo, targetingRayLength, targetingLayerMask.value))
			{
				// Cache what we've hit
				targetTransform = hitInfo.collider.transform;
//				print (targetTransform.name);
			}
		////////////////////////////////////////////////

			if(targetTransform !=null && targetTransform.GetComponent<ObjectController>() !=null)
			{
				
				ObjectController ob = targetTransform.GetComponentInParent<ObjectController>();
				if(obAnterior == null || obAnterior != ob)
				{
//					ObjectController[] correct = FindObjectsOfType(typeof(ObjectController)) as ObjectController[];
//					foreach (ObjectController hinge in correct) {
//						hinge.desSeleccionColor();
//					}
					obAnterior = ob;
					ob.SeleccionColor();
					banderaAnterior = true;
					if(titulos!=null){
						titulos.SetActive(true);
						titulos.GetComponentInChildren<Text>().text = ob.myname;
					}
				}
				 
					
					if(Input.GetButtonDown("Fire1"))
					{

					if(!ob.bolseleccion())// aqui agregar script para acciones al momento de seleccionar
						{
						
						//ob.colgar();
						ob.colgar();
						if(ob.tag == "incorrecto")
							reconocer (ob.name);
						if (ob.tag == "correcto")
							desreconocer (ob.name);
						print("fire a "+ob.name+ " "+ob.tag);

						}
					else // aqui agregar script para acciones al momento de desseleccionar
						{
							//ob.descolgar();
						ob.descolgar();
						if(ob.tag == "incorrecto")
							desreconocer (ob.name);
						if (ob.tag == "correcto")
							reconocer (ob.name);
						}
					}//fin if fire1
				  
			}//fin if target no null
			else {
				if(banderaAnterior){ 
					obAnterior.desSeleccionColor(); 
					banderaAnterior = false; 
					obAnterior= null;
					if(titulos!=null)
						titulos.SetActive(false);
				}
			}

		}
	}
	string[] decidibles;
	string[] decidiblesFacil = {"GRUA HORQUILLA","TABLERO 2","TORNO","TRANSPORTADOR DE CARGA","CILINDRO 2","LAMINADOR1","PALLETS","SOLDADOR","EXTINTOR 2","PUENTE GRUA","ANDAMIO","CILINDRO 1","LAMINADOR2","ADVERTENCIA 1","SOLDADOR 1","RESIDUOS 2","LAMINADOR3","CINTA AMARILLA"};
	string[] decidiblesIntermedio = {"GRUA HORQUILLA","TABLERO 2","OPERADOR DE ESMERIL ANGULAR","TRANSPORTADOR DE CARGA","SOLDADOR","OPERADOR DE ESMERIL ANGULAR2","ANDAMIO1","MESA REVESTIMIENTO 1","PUENTE GRUA","ANDAMIO2","MESA HIDRAULICA","CILINDRO 1","LAMINADOR","PRENSA","MESA REVESTIMIENTO 2","MESA REVESTIMIENTO 3","LAMINADOR2","RESIDUOS 1","SOLDADOR2","PUENTE GRUA2" };
	string[] decidiblesDificil = {"MESA REVESTIMIENTO 1","LAMINADOR","MESA REVESTIMIENTO 3","OPERADOR DE ESMERIL ANGULAR","RESIDUOS 2","PRENSA 1","PALLETS","PUENTE GRUA 1","ANDAMIO","MESA HIDRAULICA","PALLET 2","TRANSPORTADOR DE CARGA","MESA REVESTIMIENTO 2","PUENTE GRUA 2","GRUA HORQUILLA","PALLETS 3","RESIDUOS 1","CINTA AMARILLA","EXTINTOR 1","MESA REVESTIMIENTO 2","SOLDADOR 2" };
	string[] respuestas;
	void inicioRespuesta(){// Start cargarga los decidibles dependiento el nivel los llena con no al inicio
		if (decidibles == null)
			print ("No detecto el string de decidibles");
		
		respuestas = new string[decidibles.Length];
		GameObject[] tags = GameObject.FindGameObjectsWithTag ("correcto");
		for (int i=0;i<respuestas.Length;i++) {
			respuestas[i] = "no";
			foreach (GameObject t in tags) {
				if(t.name ==decidibles[i])
					respuestas[i]="si";
			}
		}
	}
	void reconocer(string objeto)// reconose si el objeto es si existe entre los decidibles cambia la respuesta  reconocido
	{
		
		for (int i=0;i<decidibles.Length;i++){
			if (decidibles[i] == objeto) {
				respuestas[i] = "si";
				return;
			}
		}
		print ("No existe el objeto '" + objeto + "'");
	}
	void desreconocer(string objeto)// reconose si el objeto es si existe entre los decidibles cambia la respuesta  no reconocido
	{

		for (int i=0;i<decidibles.Length;i++){
			if (decidibles[i] == objeto) {
				respuestas[i] = "no";
				return;
			}
		}
		print ("No existe el objeto '" + objeto + "'");
	}
	void printRespuestas(){// Imprime las respuestas por consola
		int i = 0;
		foreach (string r in respuestas) {
			print (decidibles[i]+r + ", ");
			i++;
		}
		print("Numero respuestas = "+i);
	}
	void guardarRestpuestas(){// Guarda los datos en un player pref
		int i = 0;
		int ultimo_id = PlayerPrefs.GetInt ("ultimo_id");
		foreach (string r in respuestas) {
			PlayerPrefs.SetString (ultimo_id.ToString()+"_respuesta_" + i.ToString(), respuestas [i]);
			i++;
		}
		PlayerPrefs.SetInt ("ultimo_id", ultimo_id + 1);
		print ("guardado");
	}
	void vestirUna(string tags){// 
		GameObject[] correct = GameObject.FindGameObjectsWithTag(tags);
		foreach (GameObject hinge in correct) {
			if(hinge.GetComponent<ObjectController>().bolseleccion()) 
				hinge.GetComponent<ObjectController>().descolgar();
		}
	}
}
