// Converted from UnityScript to C# at http://www.M2H.nl/files/js_to_c.php - by Mike Hergaarden
// Do test the code! You usually need to change a few small bits.

using UnityEngine;
using System.Collections;

public class DestroyWhenHit : MonoBehaviour
{
    public Transform explosionPrefab;

    void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Player" || collision.transform.tag == "Missile")
            return;

        foreach (ContactPoint contacts in collision.contacts)
        {
            print(contacts.thisCollider.name + " hit " + contacts.otherCollider.name);
            Debug.DrawRay(contacts.point, contacts.normal, Color.white);
        }

        if (collision.contacts.Length < 3 && (
            gameObject.GetComponent<Collider>().GetType() == typeof(CapsuleCollider) ||
            gameObject.GetComponent<Collider>().GetType() == typeof(MeshCollider)
            ))
            return;

        if (gameObject.GetComponent<Collider>().GetType() == typeof(BoxCollider) && collision.contacts.Length < 5)
            return;
        
        ContactPoint contact = collision.contacts[0];
        Quaternion rot = Quaternion.FromToRotation(Vector3.up, contact.normal);
        Vector3 pos = contact.point;
        Instantiate(explosionPrefab, pos, rot);

        Destroy(gameObject, 0);
    }
}