using UnityEngine;
using System.Collections;

public class SceneController : MonoBehaviour {

	public GameObject planetPrefab;
	public GameObject shipPrefab;

	//Quick creator making ships and planets;
	T Create<T>(GameObject prefab) where T : MonoBehaviour {

		var instance = Instantiate (prefab) as GameObject;

		var script = instance.GetComponent<T>();

		return script;

	}

	void CreateRandomPlanets(int num){

		for (var i = 0; i < num; i++) {

			var planet = Create<Planet>(planetPrefab);
			planet.diameter = Random.Range (0.25f, 2f);

			float placementExtent = Camera.main.orthographicSize;

			Vector3 randomPosition = new Vector3(
										Random.Range (-placementExtent, placementExtent),
										Random.Range (-placementExtent, placementExtent),
										0f
								  	 );

			planet.transform.position = randomPosition;
		}

	}

	void CreateShipAtCentermostPlanet() {

		var ship = Create<Ship>(shipPrefab);

		Planet[] planets = GameObject.FindObjectsOfType<Planet>();
		Planet closestPlanet = null;

		foreach(var planet in planets) {

			if (closestPlanet == null || 
			    Vector3.Distance(planet.transform.position, ship.transform.position) <
			    Vector3.Distance(closestPlanet.transform.position, ship.transform.position))
				ship.currentPlanet = planet;	

		}

	}


	void Start() {

		CreateRandomPlanets(10);
		CreateShipAtCentermostPlanet();

	}

}
