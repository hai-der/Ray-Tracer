// Haider Tiwana
// CS 465
// Assignment #2

using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;
using System.Text.RegularExpressions;

public class Record {
	public Color color;
	public double t;
}

// Creating a sphere of Radius R with center and ambient color
public class Sphere {

	public double radius;
	public Vector3 position;
	public Color ambient;

	public Sphere(double r, Vector3 pos, Color amb) {
		position = pos;
		radius = r;
		ambient = amb;
	}

	public bool sphereHit(Ray origin, double t0, double t1, Record record) {
		double A = Vector3.Dot (origin.direction, origin.direction);
		double B = 2 * (Vector3.Dot (origin.direction, (origin.origin - position)));
		double C = Vector3.Dot (origin.origin - position, origin.origin - position) - (radius * radius);
		double discriminant = B * B - (4 * A * C);

		if (discriminant > 0) {
			double sqrtD = Math.Sqrt (discriminant);
			double t = (-B - sqrtD) / (2 * A);
			// negative is always closer
			if (t < t0)
				t = (-B + sqrtD) / (2 * A);
			if (t < t0 || t > t1)
				return false;
			record.t = t;
			record.color = ambient;
			return true;
		}
		return false;
	}
}

public class NewBehaviourScript : MonoBehaviour {

	public InputField userInput;
	public InputField userWrite;
	private Texture2D myPicture;
	public int screenwidth, screenheight;
	public float bgx, bgy, bgz;
	ArrayList mySpheres = new ArrayList();
	Color[,] myColors = new Color[0,0];

	string[] all2;

	// Use this for initialization
	void Start () {
		//ReadFromFile ("myfile.txt");
		//Render ();
		//setScreen ();
	}

	void OnGUI() {
		GUI.DrawTexture(new Rect(0, 0, screenwidth, screenheight), myPicture);   
	}
		
	// Update is called once per frame
	void Update () {

	}

	public void drawPicture() {
		for (int x = 0; x < myPicture.width; x++) {            
			for (int y = 0; y < myPicture.height; y++) {
				myPicture.SetPixel (x, y, myColors [x, y]);
			}
		}  
		//We also need to apply the changes we have made to the image (or texture)    
		//This is a part that can cause much pain and frustration if forgotten    
		//So don't forget ;)        
		myPicture.Apply ();
	}

	public void Render() {
		//Debug.Log (myColors.GetLength (0)+" "+ myColors.GetLength (1));
		//Debug.Log (screenwidth + " " + screenheight);
		for(int i = 0; i < screenwidth; i++) {
			for (int j = 0; j < screenheight; j++) {
				Vector3 origin = new Vector3 (i, j, 0);
				Vector3 direction = new Vector3 (0, 0, -1);
				myColors [i, j] = rTrace (origin, direction);
			}
		}
		drawPicture ();
	}

	public Color rTrace (Vector3 origin, Vector3 direction) {
		double t0 = 0.0001;
		double t1 = 100000;
		Record smallest = new Record ();
		smallest.color = new Color (bgx, bgy, bgz);
		smallest.t = t1;
		for(int i = 0; i < mySpheres.Count; i++) {
			Record current = new Record ();
			if ((((Sphere)mySpheres [i]).sphereHit (new Ray (origin, direction), t0, t1, current))) {
				if (current.t < smallest.t)
					smallest = current;
			}
		}
		return smallest.color;
	}

	public void ReadFromFile(string fileName) {

		string all = File.ReadAllText (fileName);
		string[] all2;
		all2 = all.Split (new string[] { " ", "\n" }, System.StringSplitOptions.RemoveEmptyEntries);
		screenwidth = int.Parse (all2 [1]); // 300
		screenheight = int.Parse (all2 [2]); // 300
		myColors = new Color[screenwidth, screenheight];
		//Debug.Log ("width is" + screenwidth + " and " + screenheight);

		myPicture = new Texture2D ((int)screenwidth, (int)screenheight);

		bgx = float.Parse (all2 [4]);
		bgy = float.Parse (all2 [5]);
		bgz = float.Parse (all2 [6]);
		Sphere sphere1 = new Sphere(double.Parse(all2[8]), new Vector3(float.Parse(all2[9]),float.Parse(all2[10]),float.Parse(all2[11])), new Color (float.Parse(all2[12]),float.Parse(all2[13]),float.Parse(all2[14])));
		Sphere sphere2 = new Sphere(double.Parse(all2[19]), new Vector3(float.Parse(all2[20]),float.Parse(all2[21]),float.Parse(all2[22])), new Color(float.Parse(all2[23]),float.Parse(all2[24]),float.Parse(all2[25])));
		Sphere sphere3 = new Sphere(double.Parse(all2[30]), new Vector3(float.Parse(all2[31]),float.Parse(all2[32]),float.Parse(all2[33])), new Color(float.Parse(all2[34]),float.Parse(all2[35]),float.Parse(all2[36])));
		Sphere sphere4 = new Sphere(double.Parse(all2[41]), new Vector3(float.Parse(all2[42]),float.Parse(all2[43]),float.Parse(all2[44])), new Color(float.Parse(all2[45]),float.Parse(all2[46]),float.Parse(all2[47])));
		Sphere sphere5 = new Sphere(double.Parse(all2[52]), new Vector3(float.Parse(all2[53]),float.Parse(all2[54]),float.Parse(all2[55])), new Color(float.Parse(all2[56]),float.Parse(all2[57]),float.Parse(all2[58])));
		Sphere sphere6 = new Sphere(double.Parse(all2[63]), new Vector3(float.Parse(all2[64]),float.Parse(all2[65]),float.Parse(all2[66])), new Color(float.Parse(all2[67]),float.Parse(all2[68]),float.Parse(all2[69])));
		Sphere sphere7 = new Sphere(double.Parse(all2[74]), new Vector3(float.Parse(all2[75]),float.Parse(all2[76]),float.Parse(all2[77])), new Color(float.Parse(all2[78]),float.Parse(all2[79]),float.Parse(all2[80])));
		//Debug.Log ("Sphere1 is " + sphere1.radius + " " + sphere1.cx + " " + sphere1.cy + " " + sphere1.cz + " " +sphere1.ar + " " +sphere1.ag + " " + sphere1.ab);
		//Debug.Log ("Sphere2 is " + sphere2.radius + " " + sphere2.cx + " " + sphere2.cy + " " + sphere2.cz + " " +sphere2.ar + " " +sphere2.ag + " " + sphere2.ab);
		//Debug.Log ("Sphere3 is " + sphere3.radius + " " + sphere3.cx + " " + sphere3.cy + " " + sphere3.cz + " " +sphere3.ar + " " +sphere3.ag + " " + sphere3.ab);
		//Debug.Log ("Sphere4 is " + sphere4.radius + " " + sphere4.cx + " " + sphere4.cy + " " + sphere4.cz + " " +sphere4.ar + " " +sphere4.ag + " " + sphere4.ab);
		//Debug.Log ("Sphere5 is " + sphere5.radius + " " + sphere5.cx + " " + sphere5.cy + " " + sphere5.cz + " " +sphere5.ar + " " +sphere5.ag + " " + sphere5.ab);
		//Debug.Log ("Sphere6 is " + sphere6.radius + " " + sphere6.cx + " " + sphere6.cy + " " + sphere6.cz + " " +sphere6.ar + " " +sphere6.ag + " " + sphere6.ab);
		//Debug.Log ("Sphere7 is " + sphere7.radius + " " + sphere7.cx + " " + sphere7.cy + " " + sphere7.cz + " " +sphere7.ar + " " +sphere7.ag + " " + sphere7.ab);
		mySpheres.Add (sphere1);
		mySpheres.Add (sphere2);
		mySpheres.Add (sphere3);
		mySpheres.Add (sphere4);
		mySpheres.Add (sphere5);
		mySpheres.Add (sphere6);
		mySpheres.Add (sphere7);
		Render ();

		/*for (int i = 0; i < all2.Length; i++)
			Debug.Log (all2 [i]);
		//Debug.Log (screenwidth + " " + screenheight + " " + bgx + " " + bgy + " " + bgz);*/
		}
}