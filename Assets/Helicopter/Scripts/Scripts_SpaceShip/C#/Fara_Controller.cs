using UnityEngine;
using System.Collections;

public class Fara_Controller : MonoBehaviour {
	
	public int light_range = 30;
	private bool off_on = false;
	
	void Start () {
		off_on = false;
	}
	
	void Update () {
		if (Input.GetKeyDown("e")) {
			if (!off_on) {
				off_on = true;
				print("���� ����������");
				GetComponent<Light>().enabled = true;
			} else {
				off_on = false;
				print("���� �����������");
				GetComponent<Light>().enabled = false;
			}
		}
	}
}

