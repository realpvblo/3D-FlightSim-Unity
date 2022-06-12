using UnityEngine;
using System.Collections;

[RequireComponent(typeof(CircleCollider2D))]
public class Planet : MonoBehaviour {

	public float diameter {
		get {
			return transform.localScale.x ;
		}
		set {

			transform.localScale = new Vector3(value, value, value);
		}
	}
	
}
