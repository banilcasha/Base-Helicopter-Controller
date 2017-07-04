using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
public class Snaryad : MonoBehaviour
{
    //������ �����
    public GameObject explosion;
    public float timeOut = 3.0f; //����� ����� �������
    public int damage = 5;
    public float explosionRadius = 1;
    public float explosionPower = 10.0f;


    //������ ���� ������ ������� ����� �������
    void Start()
    {
        Invoke("Destroy_p", timeOut);
        Physics.IgnoreCollision(GameObject.FindWithTag("Player").GetComponent< Collider > (), GetComponent< Collider > ());
    }

    //��������� �� ������������
    void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.gameObject.name);
        if (collision.transform.tag != "Player")
        {
            // Instantiate explosion at the impact point and rotate the explosion
            // so that the y-axis faces along the surface normal
            ContactPoint contact = collision.contacts[0];
            Quaternion rotation = Quaternion.FromToRotation(Vector3.up, contact.normal);
            Instantiate(explosion, contact.point, rotation); //������ ��� ������

            Vector3 explosionPosition = transform.position;
            Collider[] colliders = Physics.OverlapSphere(explosionPosition, explosionRadius);

            foreach (var hit in colliders)
            {
                // Calculate distance from the explosion position to the closest point on the collider
                Vector3 closestPoint = hit.ClosestPointOnBounds(explosionPosition);
                float distance = Vector3.Distance(closestPoint, explosionPosition);

                // The hit points we apply fall decrease with distance from the explosion point
                float hitPoints = 1.0f - Mathf.Clamp01(distance / explosionRadius);
                hitPoints *= damage;

                // Tell the rigidbody or any other script attached to the hit object how much damage is to be applied!
                hit.SendMessageUpwards("ApplyDamage", hitPoints, SendMessageOptions.DontRequireReceiver);
            }

            Destroy_p();
        }
    }

    void Destroy_p()
    {
        // Stop emitting particles in any children
        ParticleEmitter emitter = GetComponentInChildren<ParticleEmitter>();
        if (emitter)
        {
            emitter.emit = false;

            // Detach children - We do this to detach the trail rendererer which should be set up to auto destruct
            transform.DetachChildren();
        }

        Destroy(gameObject);
    }
}