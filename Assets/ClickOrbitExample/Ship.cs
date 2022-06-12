using UnityEngine;
using System.Collections;

public class Ship : MonoBehaviour {

	public Planet currentPlanet;
	public float orbitSpeed = 90f; //Degrees per second
	public float orbitHeight = 1f; //Units from surface;

	float orbitPhase = 0f;

	void Update() {

		SetClickedPlanetToCurrent();
		FlyToAndAroundCurrentPlanet();

	}

	void SetClickedPlanetToCurrent(){
		if (!Input.GetMouseButtonDown(0))
			return;

		Vector2 mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition + Vector3.forward * 10f);

		var collider = Physics2D.OverlapCircle(mouseWorldPosition, 0.25f);

		Planet planet;

		if (collider && (planet = collider.transform.GetComponent<Planet>()) != null) {
			currentPlanet = planet;
		}

	}
	
	void FlyToAndAroundCurrentPlanet() {

		float heightFromSurface = currentPlanet.diameter * 0.5f + orbitHeight;

		//Pick a spot on an imaginary circle surrounding the planet, on orbitPhase angle around that circle
		Vector3 targetPosition = PointOnCircle (currentPlanet.transform.position, heightFromSurface, orbitPhase);

		//Lerp to that spot
		transform.position = Vector3.Lerp (transform.position, targetPosition, Time.deltaTime * 10f);

		//Increment the orbit phase to spin around it.
		orbitPhase += Time.deltaTime * orbitSpeed;


	}

	static Vector2 PointOnCircle(Vector2 origin, float radius, float angle) {
		
		float x = radius * Mathf.Cos (angle * Mathf.Deg2Rad) + origin.x;
		float y = radius * Mathf.Sin (angle * Mathf.Deg2Rad) + origin.y;
		
		return new Vector2(x,y);
	}
}
