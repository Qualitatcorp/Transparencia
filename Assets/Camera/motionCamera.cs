using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Scripting;
public class motionCamera : MonoBehaviour {
	public enum Estado{Libre,Movimiento,Liberando};
	public Estado estado;
	public int Estacion;
	public Transform[] Lugares;
	public float speed;
	public GameObject DiveCamara;
	private Vector3 OrigenPosition;
	private Quaternion OrigenRotation;
	public MonoBehaviour[] ScriptBloqueos;
	void Start () {
		OrigenPosition = DiveCamara.transform.position;
		OrigenRotation = DiveCamara.transform.rotation;
	}
	void FixedUpdate () {
		//Movimientos de Estados
		switch (estado) {
		case Estado.Libre:
			break;
		case Estado.Movimiento:
			if (Estacion >= 0 && Estacion < Lugares.Length) {
				//Translacion
				DiveCamara.gameObject.transform.position = Vector3.Lerp (DiveCamara.gameObject.transform.position, Lugares [Estacion].position, speed * Time.deltaTime);
				//Rotacion
				DiveCamara.gameObject.transform.rotation = Quaternion.Lerp (DiveCamara.gameObject.transform.rotation, Lugares [Estacion].rotation, speed * Time.deltaTime);
			} else {
				estado = Estado.Liberando;
				print ("Estacion Invalida");
			}
			break;
		case Estado.Liberando:
			//Devuelve a la posicion original
			DiveCamara.gameObject.transform.position = Vector3.Lerp (DiveCamara.gameObject.transform.position, OrigenPosition, (speed * Time.deltaTime));
			DiveCamara.gameObject.transform.rotation = Quaternion.Lerp (DiveCamara.gameObject.transform.rotation,OrigenRotation, speed * Time.deltaTime);
			//Setea la posicion al acercarse y lo libera
			if (Vector3.Distance (OrigenPosition, DiveCamara.gameObject.transform.position) < 0.5F) {
				DiveCamara.gameObject.transform.position = OrigenPosition;
				DiveCamara.gameObject.transform.rotation = OrigenRotation;
				estado = Estado.Libre;
			}
			break;
		}
		//Define Bloqueos de control
		if (estado != Estado.Libre) {
			//Bloquea Script de control
			foreach (MonoBehaviour c in ScriptBloqueos) {
				c.enabled = false;
			}
		} else {
			//Libera Bloqueos
			foreach (MonoBehaviour c in ScriptBloqueos) {
				c.enabled = true;
			}
		}
	}
}
