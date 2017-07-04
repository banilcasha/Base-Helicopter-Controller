using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {

    void Start()
    {

    }
    public Rigidbody bullet;
    RaycastHit hit;
    void Update()

    {
        Ray pickray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if(Physics.Raycast(pickray,out hit))
        {
            Debug.Log("맞은애 : " + hit.transform.name);
            this.transform.LookAt(hit.point);
        }
        else
        {
            Debug.Log("없음");

        }

        if (Input.GetKeyDown(KeyCode.Z))
        {
            Rigidbody rb = Instantiate(bullet, transform.position, transform.rotation) as Rigidbody;
            rb.velocity = transform.forward;
        }
    }
}
