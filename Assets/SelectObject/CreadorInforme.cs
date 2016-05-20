using UnityEngine;
using System.Collections;
using System.IO;
using System.Text;
public class CreadorInforme : MonoBehaviour {

	// Use this for initialization
	void Start () {
		string[] decidibles = {"GRUA HORQUILLA","TABLERO 2","TORNO","TRANSPORTADOR DE CARGA","CILINDRO 2","LAMINADOR1","PALLETS","SOLDADOR","EXTINTOR 2","PUENTE GRUA","ANDAMIO","CILINDRO 1","LAMINADOR2","ADVERTENCIA 1","SOLDADOR 1","RESIDUOS 2","LAMINADOR3","CINTA AMARILLA"};;
		string respuesta;
		string path = @"D:\metso.txt";
		string ultimo_id = (PlayerPrefs.GetInt ("ultimo_id")-1).ToString();
		// This text is added only once to the file.
		if (!File.Exists(path))
		{
			// Create a file to write to.
			string createText = "Layout\t \tRespuesta"+ 
				System.Environment.NewLine;
			File.WriteAllText(path, createText);
		}
		// This text is always added, making the file longer over time
		// if it is not deleted.
		int i = 0;
		File.AppendAllText (path,System.Environment.NewLine + System.Environment.NewLine+ System.DateTime.Now);
		foreach (string decidible in decidibles) {
			respuesta = PlayerPrefs.GetString (ultimo_id + "_respuesta_" + i.ToString ());
			string appendText = decidible + "\t\t, " + respuesta + " ,\t\t" + System.DateTime.Now + System.Environment.NewLine;
			File.AppendAllText (path, appendText);
			i++;
		}

		// Open the file to read from.
		string readText = File.ReadAllText(path);

		print (readText);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
