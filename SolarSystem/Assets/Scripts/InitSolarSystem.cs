using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class InitSolarSystem : MonoBehaviour {
	class PlanetInfo {
		public int diameter {
			get;
			private set;
		}
		public int distance {
			get;
			private set;
		}
		public float speed {
			get;
			private set;
		}

		public PlanetInfo(int diameter, int distance, float speed) {
			this.diameter = diameter;
			this.distance = distance;
			this.speed = speed;
		}
	}

	private Dictionary<string, PlanetInfo> planets = new Dictionary<string, PlanetInfo>();

	public Transform sun;

	private void Start () {
		planets.Add ("Mercury", new PlanetInfo (4880, 58, 47.87f));
		planets.Add ("Venus", new PlanetInfo (12140, 108, 35f));
		planets.Add ("Earth", new PlanetInfo (12756, 150, 29.78f));
		planets.Add ("Mars", new PlanetInfo (6787, 228, 24.13f));
		planets.Add ("Jupiter", new PlanetInfo (142800, 778, 13f));
		planets.Add ("Saturn", new PlanetInfo (120660, 1400, 9.69f));
		planets.Add ("Uranus", new PlanetInfo (51118, 2900, 6.81f));
		planets.Add ("Neptune", new PlanetInfo (49528, 4500, 5.43f));
		planets.Add ("Pluto", new PlanetInfo (2300, 5900, 4.67f));


		loadPlanets ();
	}

	private void loadPlanets() {
		long sunDiameter = 1391400;
		var diameterCoef = 100f;
		var distanceCoef = 10f;

		foreach (var record in planets) {
			var planet = GameObject.Instantiate(Resources.Load (record.Key, typeof(GameObject))) as GameObject;

			var info = record.Value;

			var relatedDiameter = (float)info.diameter / sunDiameter * diameterCoef;
			planet.transform.localScale = new Vector3 (relatedDiameter, relatedDiameter, relatedDiameter);

			var relatedDistance = (float)info.distance / distanceCoef;
			planet.transform.position = new Vector3 (relatedDistance, 0f, 0f);
			planet.transform.RotateAround(sun.position,sun.up,Random.Range(0f, 360f));

			planet.AddComponent<BoxCollider> ();
			planet.GetComponent<RotateAround> ().enabled = false;
			var rotateAround = planet.AddComponent<RotateAround> ();
			rotateAround.speed = info.speed;
			rotateAround.target = sun;
		}
	}
}
