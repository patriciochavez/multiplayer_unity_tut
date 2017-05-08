using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class PlayerController : MonoBehaviour {	
	public float speed = 3f;
	public float turn_speed = 50f;
	
void Update () {
	if (Input.anyKey) {
		if (Input.GetKey (KeyCode.UpArrow)) {
			GetComponent<Animation> ().Play ("run");            
			transform.Translate (Vector3.forward * Time.deltaTime * speed);			
		}
		if (Input.GetKey (KeyCode.DownArrow)) {
			GetComponent<Animation> ().Play ("run");            
			transform.Translate (Vector3.back * Time.deltaTime * speed);
		}
		if (Input.GetKey (KeyCode.LeftArrow)) {						
			transform.Rotate (Vector3.up, -turn_speed * Time.deltaTime);
		}
		if (Input.GetKey (KeyCode.RightArrow)) {						
			transform.Rotate (Vector3.up, turn_speed * Time.deltaTime);
		}
		if (Input.GetButton ("Fire1")) {
			GetComponent<Animation> ().Play ("assault_combat_shoot");
		}			
	} else {
		GetComponent<Animation>().Play("assault_combat_idle");	
		}
	}	
}
