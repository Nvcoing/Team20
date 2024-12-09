using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Delivery : MonoBehaviour
{
	[SerializeField] Color32 hasPackageColor = new Color32 (1,1,1,1);
	[SerializeField] Color32 noPackageColor = new Color32 (0,0,0,1);
	[SerializeField] float destroyDelay = 0.5f;
	SpriteRenderer spriteRenderer;

	void Start() {
		spriteRenderer = GetComponent<SpriteRenderer>();
	}

	private bool gotPackage = false;
	void OnCollisionEnter2D(Collision2D other) 
	{
		Debug.Log("Ouch! Collision."); 
	}
	void OnTriggerEnter2D(Collider2D other)
	{
		//Debug.Log("What was that? Trigger!");
		if (other.tag == "Package" && !gotPackage) {
			Debug.Log("Package Picked up!");
			gotPackage = true;
			spriteRenderer.color = hasPackageColor;
			Destroy(other.gameObject, destroyDelay);
		}
		if (gotPackage && other.tag == "Customer") {
			Debug.Log("Package Delivered!");
			gotPackage = false;
			spriteRenderer.color = noPackageColor;
		}
	}
}
