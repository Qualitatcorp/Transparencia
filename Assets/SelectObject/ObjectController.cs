using UnityEngine;
using System.Collections;

public class ObjectController : MonoBehaviour {
	
	private GameObject corona;
	private float alturaCorona = 1.0f;
	private Quaternion root = Quaternion.identity;
	private GameObject nuevo = null;

	public GameObject aurora;
	private bool visible = false;
	private Behaviour halo;
	private bool seleccionado = false;
	private TextMesh textcolor;
	private bool textcolorbool = false;
	private float transparencia = 0.3f;
	private bool textdesicion = false;

	private bool parpadea = false;
	private float al=0;
	private float velColor=1;

	public Color GlowColor = Color.white;
	public Color GlowColor2 = Color.green;
	public Shader transparente ;
	public GameObject otro;

	private string[] OriginalTextura = new string[50];
	private Color[] OriginalColor = new Color[50];
	private Color[] OriginalGlow = new Color[50];
	private Color[] backupColor = new Color[50]; 
	private Color[] backupGlow = new Color[50];
	private string[] backuptextura = new string[50];
	private string[] backuptextura2 = new string[50];



	public Transform mover_a;
	private Vector3 posicion_original;
	private Quaternion rotacion_original;
	int up = 0;
	void Start () {
		BackupOriginal ();

		posicion_original = this.transform.position;
		rotacion_original = this.transform.rotation;
	
		if(aurora != null)
			aurora.SetActive (false);
		if(this.GetComponent("Halo")!=null)
			halo = (Behaviour)this.GetComponent("Halo");
		//if(this.GetComponent<TextMesh>() != null) 
			//textcolor = this.GetComponent<TextMesh>();

	}
	public void tomar()
			{if (otro == null)
				Fire1 ();
				else {
					otro.SetActive (true);
					this.gameObject.SetActive (false);
				}
			}
	public void Fire1()
	{
		SeleccionColor ();
	}
	public void colgar(){
		seleccionado = true;
		if (mover_a != null) {
			print ("colgar");
			this.transform.position = mover_a.position;
			this.transform.rotation = mover_a.rotation;

		} else
			SeleccionColor2 ();// SeleccionTransparente ();
	}
	public void descolgar(){
		seleccionado = false;
		if (mover_a != null) {
			print ("devolver");

			this.transform.position = posicion_original;
			this.transform.rotation = rotacion_original;

		} else
			desSeleccionColor2 ();// desSeleccionTransparente ();
	}


	void Update () {

	
		if (Input.GetKeyDown (KeyCode.C)) {
			SeleccionColor ();
		}
		if (Input.GetKeyDown (KeyCode.V)) {
			desSeleccionColor ();
		}

		if(parpadea)
			TransparenteParpadeanteAccion ();


		if (visible && aurora != null)
			aurora.SetActive (true);
		else if(aurora != null)
			aurora.SetActive (false);
	
		if (textcolorbool && this.GetComponent<TextMesh> () != null) {
			desicionVerde ();
			print ("color true");
		} else if (this.GetComponent<TextMesh> () != null && !textdesicion) {
			print ("color false");
			desicionBlanca ();
			print (textcolorbool);
		} else if ((this.GetComponent<TextMesh> () != null && textdesicion))
			desicionRoja ();

		visible = false;
		textcolorbool = false;
	}

	public void seleccionHalo()
	{

		visible = true;
	
	}

	public void seleccionAurora(){
		visible = true;
	}

	//###################### Transparente ####################################(Mejorar: guardar el shader y volver a colocar el antiguo)
	public void SeleccionTransparente()
	{

		print ("trasparentado");
		Renderer[] renderers = GetComponentsInChildren<Renderer>();
		int i = 0;
		foreach (Renderer r in renderers)
		{
			string nombre = r.material.shader.name;
			if(backuptextura[i]==null)
				backuptextura[i] = nombre;

			r.material.shader = transparente;  //Shader.Find("Marmoset/Transparent/Bumped Diffuse IBL");
			Color alpha = r.material.color;
			alpha.a = transparencia;
			r.material.color = alpha;
			i++;
		}

	}
	public void desSeleccionTransparente()
	{

		print ("destrasparentado");
		Renderer[] renderers = GetComponentsInChildren<Renderer>();
		int i = 0;
		foreach (Renderer r in renderers)
		{
			if(backuptextura[i]!=null)
				r.material.shader = Shader.Find(backuptextura[i]);
			Color alpha = r.material.color;
			alpha.a = 1f;
			r.material.color = alpha;
			print (r.material.color.a);
			i++;
		}
	}
	public bool bolseleccion()
	{
		return seleccionado;
	}
	public void alldesSeleccionTransparente()
	{
		ObjectController[] hinges = FindObjectsOfType(typeof(ObjectController)) as ObjectController[];
		foreach (ObjectController hinge in hinges) {
			hinge.desSeleccionTransparente();
		}
	}
	public void TransparenteParpadeante()
	{
		parpadea = true;
	}
	public void noTransparenteParpadeante()
	{
		parpadea = false;
	}

	void TransparenteParpadeanteAccion()
	{

		al += Time.deltaTime%1*velColor;
		if (al > 1 || al <transparencia) {
			velColor = velColor*-1;
		}

			Renderer[] renderers = GetComponentsInChildren<Renderer>();
			foreach (Renderer r in renderers)
			{
				Color alpha = r.material.color;
				alpha.a = al;
				r.material.color = alpha;
			}
		
		
	}
	//##################################### Seleccion de textMesh ##############################
	public void SeleccionDesicion()
	{
		textcolorbool = true;
		textcolor = this.GetComponent<TextMesh>();
		textcolor.color = Color.green;
	}
	public void desSeleccionDesicion()
	{
		textcolorbool = false;
	}
	void desicionVerde()
	{
		textcolor = this.GetComponent<TextMesh>();
		textcolor.color = Color.green;
	}
	void desicionBlanca()
	{print ("blanco");
		TextMesh textcolo = this.GetComponent<TextMesh>();
		textcolo.color = Color.white;

	}
	public void SeleccionDesicionRoja()
	{
		textdesicion = true;
	}
	public void desSeleccionDesicionRoja()
	{
		textdesicion = false;
		TextMesh textcolo = this.GetComponent<TextMesh>();
		textcolo.color = Color.white;
	}
	void desicionRoja()
	{print ("roja");
		TextMesh textcolo = this.GetComponent<TextMesh>();
		textcolo.color = Color.red;
		
	}
	//#################################### Seleccion Color ###############################################
	public void SeleccionColor()
	{


		Renderer[] renderers = GetComponentsInChildren<Renderer>();
		int i = 0;
		foreach (Renderer r in renderers)
		{
			Material[] materials = r.materials;
			foreach (Material m in materials){
				if (transparente.name == "Standard (Vertex Color)") {
					print (transparente.name);
					//m.shader = transparente;
					backupGlow [i] = m.GetColor ("_EmissionColor");
					m.SetFloat ("_EmissionScaleUI", 0.6f);
					m.EnableKeyword ("_EMISSION");
					m.SetColor ("_EmissionColor", GlowColor);

				}
				else
					if(m.shader.name !="Unlit/Transparent Cutout"){
					if (backuptextura [i] == null)
						backuptextura [i] = m.shader.name;
					if (backupColor [i] == null && m.shader.name != "Mobile/Unlit (Supports Lightmap)")
						backupColor [i] = m.color;
					else
						backupColor [i] = Color.white;
					if (backupGlow [i] == null)
						backupGlow [i] = m.GetColor ("_Emission");
			
					m.shader = transparente;//Shader.Find("Legacy Shaders/VertexLit");
					m.SetColor ("_Color", GlowColor);
					m.SetColor ("_Emission", GlowColor);
					m.SetColor ("_EmissionColor", GlowColor);
					if (backuptextura [i] == "Particles/Additive")
						m.shader = Shader.Find ("Particles/Additive");
				}
			i++;
			}
		}
	}
	public void SeleccionColor2()
	{
		

		Renderer[] renderers = GetComponentsInChildren<Renderer>();
		int i = 0;
		foreach (Renderer r in renderers)
		{
			Material[] materials = r.materials;
			foreach (Material m in materials){
//				if(backuptextura2[i]==null)
//					backuptextura[i] = backuptextura[i];			
				if (transparente.name == "Standard (Vertex Color)") {
					print (transparente.name);
					//m.shader = transparente;
					backupGlow [i] = GlowColor2;
					m.SetFloat ("_EmissionScaleUI", 1f);
					m.EnableKeyword ("_EMISSION");
					m.SetColor ("_EmissionColor", GlowColor2);

				} else 
					if(m.shader.name !="Unlit/Transparent Cutout"){
					m.shader = transparente;//Shader.Find("Legacy Shaders/VertexLit");
					m.SetColor ("_Emission", GlowColor2);
					m.SetColor ("_EmissionColor", GlowColor2);
					backuptextura [i] = transparente.name;//"Legacy Shaders/VertexLit";
					backupGlow [i] = GlowColor2;
					if (m.shader.name != "Mobile/Unlit (Supports Lightmap)")
						backupColor [i] = m.color;
					else
						backupColor [i] = Color.white;
				}
				i++;
			}
		}
	}
	public void desSeleccionColor2()
	{
		
		print ("No Color");
		Renderer[] renderers = GetComponentsInChildren<Renderer>();
		int i = 0;
		foreach (Renderer r in renderers)
		{
			Material[] materials = r.materials;
			foreach (Material m in materials){
				if (transparente.name == "Standard (Vertex Color)") {
					backupGlow [i] = OriginalGlow[i];
					m.EnableKeyword ("_EMISSION");
					m.SetFloat ("_EmissionScaleUI", 0f);
					m.SetColor ("_EmissionColor", Color.black);
				} else 
					if(m.shader.name !="Unlit/Transparent Cutout"){
				if (backuptextura2 [i] != null && !seleccionado && m.shader.name != "Particles/Additive") {
					m.shader = Shader.Find (OriginalTextura[i]);
				}
//				if(seleccionado)
					m.shader = Shader.Find (OriginalTextura [i]);

					if (m.shader.name != "Mobile/Unlit (Supports Lightmap)")
						m.color = OriginalColor [i];
					else
						m.color = Color.white;
					m.EnableKeyword ("_EMISSION");
					m.SetColor ("_Emission", OriginalGlow [i]);
					m.SetColor ("_EmissionColor", OriginalGlow [i]);
					backupGlow [i] = OriginalGlow [i];

				}
				i++;
			}
		}
		
	}
	public void desSeleccionColor()
	{


		Renderer[] renderers = GetComponentsInChildren<Renderer>();
		int i = 0;
		foreach (Renderer r in renderers)
		{
			Material[] materials = r.materials;
			foreach (Material m in materials){
				if (transparente.name == "Standard (Vertex Color)") {
					m.EnableKeyword ("_EMISSION");
					m.SetFloat ("_EmissionScaleUI", 0f);
					m.SetColor ("_EmissionColor", backupGlow[i]);
				} else 
					if(m.shader.name !="Unlit/Transparent Cutout"){
					if (backuptextura [i] != null && !seleccionado && m.shader.name != "Particles/Additive")
						m.shader = Shader.Find (OriginalTextura [i]);
					if (seleccionado)
						m.shader = Shader.Find (backuptextura [i]);
					m.color = backupColor [i];

					m.EnableKeyword ("_EMISSION");
					m.SetColor ("_Color", backupColor [i]);
					m.SetColor ("_Emission", backupGlow [i]);
					m.SetColor ("_EmissionColor", backupGlow [i]);
					m.SetFloat ("_Mode", 3);

				}
				i++;
			}
		}
			
	}
	public void alldesSeleccionColor()
	{
		ObjectController[] hinges = FindObjectsOfType(typeof(ObjectController)) as ObjectController[];
		foreach (ObjectController hinge in hinges) {
			hinge.desSeleccionColor();
		}
	}

	public void Marcado(){
		
		//if(marcado==true){
		SeleccionColor2(); 
		up++;
		if (up < 10) {
			AudioSource moneda = GameObject.Find ("Dive FPS Player/Dive_Camera/palo").GetComponent<AudioSource> ();
			moneda.GetComponent<AudioSource>().Play ();
		} else {
			AudioSource moneda = GameObject.Find ("Iluminacion").GetComponent<AudioSource> ();
			moneda.GetComponent<AudioSource>().Play ();
			up=0;
		}
		
		//	marcado = false;
		
	}

	public void BackupOriginal()
	{

	
		Renderer[] renderers = GetComponentsInChildren<Renderer>();
		int i = 0;
		foreach (Renderer r in renderers)
		{
			Material[] materials = r.materials;
			foreach (Material m in materials){
				if(OriginalTextura[i]==null)
					OriginalTextura[i] = m.shader.name;
				OriginalColor [i] = Color.white;
				if (OriginalColor [i] == null)
					OriginalColor[i] = m.color;
				if (OriginalGlow [i] == null)
					OriginalGlow[i] = m.GetColor ("_Emission");
				if (OriginalGlow [i] == null && transparente.name == "Standard (Vertex Color)")
					OriginalGlow[i] = m.GetColor ("_EmissionColor");
				if(backuptextura[i]==null)
					backuptextura[i] = m.shader.name;
				backupColor [i] = Color.white;

				if (m.shader.name !="Mobile/Unlit (Supports Lightmap)" && m.shader.name !="Double Sided/Emissive/Diffuse" )
					backupColor[i] = m.GetColor ("_Color");
				if (backupGlow [i] == null )
					backupGlow[i] = m.GetColor ("_Emission");
				
				if(backuptextura[i] =="Particles/Additive")
					m.shader = Shader.Find( "Particles/Additive");
				if(OriginalTextura[i] =="Particles/Additive")
					m.shader = Shader.Find( "Particles/Additive");
				i++;
			}
		}
	}


	public string MyName="";

	public string myname{
		get{
			return MyName;
		}
		set{
			MyName = value;
		}
	}
}
