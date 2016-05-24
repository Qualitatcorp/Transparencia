using UnityEngine;
using System.Collections;

public class lerptrasnparente : MonoBehaviour {
	void Update ()
	{
		if(Input.GetKeyUp(KeyCode.T))
		{
			StartCoroutine(FadeTo(0.5f, 1.0f));
		}
		if(Input.GetKeyUp(KeyCode.F))
		{
			StartCoroutine(FadeTo(1.0f, 1.0f));
		}
	}

	IEnumerator FadeTo(float aValue, float aTime)
	{
		float alpha = transform.GetComponent<Renderer>().material.color.a;
		for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / aTime)
		{
			Color newColor = new Color(1, 1, 1, Mathf.Lerp(alpha,aValue,t));
			transform.GetComponent<Renderer>().material.color = newColor;
			yield return null;
		}
	}




	public void lerpingON (){

		StartCoroutine(FadeTo(0.5f, 1.0f));
		//StartCoroutine(FadeToIN(0.3f, 1.0f));

	}
	public void lerpingOFF (){

		StartCoroutine(FadeTo(1.0f, 1.0f));
		//StartCoroutine(FadeToOUT(0.3f, 1.0f));

	}

	public void RenderTransON(Transform joint){
		joint.gameObject.GetComponent<Renderer> ().material.SetFloat ("_Mode", 2);
		joint.gameObject.GetComponent<Renderer> ().material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
		joint.gameObject.GetComponent<Renderer> ().material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
		joint.gameObject.GetComponent<Renderer> ().material.SetInt("_ZWrite", 0);
		joint.gameObject.GetComponent<Renderer> ().material.DisableKeyword("_ALPHATEST_ON");
		joint.gameObject.GetComponent<Renderer> ().material.EnableKeyword("_ALPHABLEND_ON");
		joint.gameObject.GetComponent<Renderer> ().material.DisableKeyword("_ALPHAPREMULTIPLY_ON");
		joint.gameObject.GetComponent<Renderer> ().material.renderQueue = 3000;

	}

	public void RenderTransOFF(Transform joint){

		joint.gameObject.GetComponent<Renderer> ().material.SetFloat ("_Mode", 0);
		joint.gameObject.GetComponent<Renderer> ().material.SetInt ("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);
		joint.gameObject.GetComponent<Renderer> ().material.SetInt ("_DstBlend", (int)UnityEngine.Rendering.BlendMode.Zero);
		joint.gameObject.GetComponent<Renderer> ().material.SetInt ("_ZWrite", 1);
		joint.gameObject.GetComponent<Renderer> ().material.DisableKeyword ("_ALPHATEST_ON");
		joint.gameObject.GetComponent<Renderer> ().material.DisableKeyword ("_ALPHABLEND_ON");
		joint.gameObject.GetComponent<Renderer> ().material.DisableKeyword ("_ALPHAPREMULTIPLY_ON");
		joint.gameObject.GetComponent<Renderer> ().material.renderQueue = -1;
	}

	public void RenderTransON(lerptrasnparente joint){
		joint.gameObject.GetComponent<Renderer> ().material.SetFloat ("_Mode", 2);
		joint.gameObject.GetComponent<Renderer> ().material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
		joint.gameObject.GetComponent<Renderer> ().material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
		joint.gameObject.GetComponent<Renderer> ().material.SetInt("_ZWrite", 0);
		joint.gameObject.GetComponent<Renderer> ().material.DisableKeyword("_ALPHATEST_ON");
		joint.gameObject.GetComponent<Renderer> ().material.EnableKeyword("_ALPHABLEND_ON");
		joint.gameObject.GetComponent<Renderer> ().material.DisableKeyword("_ALPHAPREMULTIPLY_ON");
		joint.gameObject.GetComponent<Renderer> ().material.renderQueue = 3000;

	}

	public void RenderTransOFF(lerptrasnparente joint){

		joint.gameObject.GetComponent<Renderer> ().material.SetFloat ("_Mode", 0);
		joint.gameObject.GetComponent<Renderer> ().material.SetInt ("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);
		joint.gameObject.GetComponent<Renderer> ().material.SetInt ("_DstBlend", (int)UnityEngine.Rendering.BlendMode.Zero);
		joint.gameObject.GetComponent<Renderer> ().material.SetInt ("_ZWrite", 1);
		joint.gameObject.GetComponent<Renderer> ().material.DisableKeyword ("_ALPHATEST_ON");
		joint.gameObject.GetComponent<Renderer> ().material.DisableKeyword ("_ALPHABLEND_ON");
		joint.gameObject.GetComponent<Renderer> ().material.DisableKeyword ("_ALPHAPREMULTIPLY_ON");
		joint.gameObject.GetComponent<Renderer> ().material.renderQueue = -1;
	}


}
