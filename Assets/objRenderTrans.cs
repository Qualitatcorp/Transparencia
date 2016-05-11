using UnityEngine;
using System.Collections;

public class objRenderTrans : MonoBehaviour {

	public Transform padre;

	void Awake(){
		//print ("Trasparencia otros hijos");
		
		foreach (Transform hijo in padre) {


		//	hijo.gameObject.GetComponent<MeshRenderer> ().material.SetFloat("_Mode", 3.0f);


		}
		


	}
}
