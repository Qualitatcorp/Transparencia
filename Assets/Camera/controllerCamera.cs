using UnityEngine;
using System.Collections;

public class controllerCamera : MonoBehaviour {
	//Variables de control
	public bool inicia;
	public int circuito;
	//Define circuitos
	public Transform[] Circuito1;
	public Transform[] Circuito2;
	public Transform[] Circuito3;
	//Define Tiempos de recorrer circuitos
	public float[] tiempo1;
	public float[] tiempo2;
	public float[] tiempo3;
	//Define informacion de Control
	private Transform[] Circuito;
	private float[] tiempo;
	static private bool bloqueo;

	private motionCamera mC;
	void Start () {
		bloqueo = false;
		mC = this.gameObject.GetComponent<motionCamera> ();
	}


	void FixedUpdate(){
		if (inicia&&!bloqueo) {
			switch (circuito) {
			case 1:
				Circuito = Circuito1;
				tiempo = tiempo1;
				break;
			case 2:
				Circuito = Circuito2;
				tiempo = tiempo2;
				break;
			case 3:
				Circuito = Circuito3;
				tiempo = tiempo3;
				break;
			}
			StartCoroutine (Recorre ());
		}
	}
	IEnumerator Recorre (){
		bloqueo = true;
		mC.Lugares = Circuito;
		mC.estado = motionCamera.Estado.Movimiento;
		for (int i = 0; i < Circuito.Length; i++) {
			mC.Estacion = i;
			yield return new WaitForSeconds (tiempo[i]);
		}
		mC.estado = motionCamera.Estado.Liberando;
		yield return new WaitForSeconds (1);
		bloqueo = false;
		inicia = false;
	}
}
