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

		StartCoroutine(FadeTo(0.3f, 1.0f));

	}
	public void lerpingOFF (){

		StartCoroutine(FadeTo(1.0f, 1.0f));

	}
}
