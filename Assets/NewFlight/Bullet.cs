using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Rigidbody bullet;
    public float speed = 10.0f;

    public Transform Spawn;
    public Rigidbody Missle;
    public int waitTime;
    public int speed_missile;
    private Ray ray = new Ray();

    private RaycastHit hit = new RaycastHit();

    void Update()
    {

        ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit))
        {
            Vector3 point = hit.point;

            Spawn.transform.LookAt(point);
        }
        else
        {
            Spawn.transform.localRotation = Quaternion.identity;
        }

        if (Input.GetButtonDown("Fire1"))
        {
            Rigidbody instance = Instantiate(bullet, Spawn.position,
                                                   Spawn.rotation);
            instance.velocity = Spawn.forward * speed;
        }

        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            Debug.Log(Spawn.rotation);
            Rigidbody instance = Instantiate(Missle, Spawn.position, Spawn.rotation);
            instance.velocity = Spawn.forward * speed_missile;
        }
    }
}

