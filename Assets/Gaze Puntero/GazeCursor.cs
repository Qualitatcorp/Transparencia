using UnityEngine;

public class GazeCursor : MonoBehaviour{
	public GameObject cursor;
	public bool showCursor = true;
	public bool scaleCursorSize = true;
	public Camera camara;
	bool b= true;
	bool v = false;
	void Update(){
		PlaceCursor ();
	}
	
	private void PlaceCursor() {
		if (cursor == null) {
			return;
		}
		//var go = pointerData.pointerCurrentRaycast.gameObject;
		Camera cam = camara;// pointerData.enterEventCamera;  // Will be null for overlay hits.
		RaycastHit Rhit;
		Ray ray = cam.ViewportPointToRay(new Vector3(0.5F, 0.5F, 0));
		
		cursor.SetActive( cam != null && showCursor);
		if (cursor.activeInHierarchy && Physics.Raycast(ray, out Rhit) ) {
			if (Rhit.transform.GetComponent<ObjectController>() ) {
				if (b) {//optimizacion
					cambiarColor (Color.blue);
					v = true; b = false;
				}
			} else {//optimizacion
				if(v){
					cambiarColor(Color.yellow);
					b= true; v=false;
				}
			}
				
			// Note: rays through screen start at near clipping plane.
			float dist = Rhit.distance + cam.nearClipPlane;
			cursor.transform.position = cam.transform.position + cam.transform.forward * dist;
			if (scaleCursorSize) {
				cursor.transform.localScale = Vector3.one * dist;
			}
		}
	}
	void cambiarColor(Color emision )
	{
		Renderer[] renderers = GetComponentsInChildren<Renderer> ();
		int i = 0;
		foreach (Renderer r in renderers) {
			Material[] materials = r.materials;
			foreach (Material m in materials) {
				m.color = emision;
				m.SetColor ("_Emission", emision);
			}
		}
	}
}