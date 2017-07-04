// Converted from UnityScript to C# at http://www.M2H.nl/files/js_to_c.php - by Mike Hergaarden
// Do test the code! You usually need to change a few small bits.

using UnityEngine;
using System.Collections;

public class DamageDestroy : MonoBehaviour
{

    private Vector3 vRandomRotation;
    float rotationSpeed = 3;

    public bool rand = true;
    private int randMultiplier;
    public int minMultiplier = 1;
    public int maxMultiplier = 3;
    private int health;
    public GameObject explosion;
    public bool makeChildren = true;
    public GameObject newClone;

    public int points = 100;


    void Start()
    {
        vRandomRotation = new Vector3(Random.Range(-rotationSpeed, rotationSpeed), Random.Range(-rotationSpeed, rotationSpeed), Random.Range(-rotationSpeed, rotationSpeed));
        gameObject.tag = "Respawn";
        if (rand)
        {
            randMultiplier = Random.Range(minMultiplier, maxMultiplier);
        }
        //transform.localScale = new Vector3(randMultiplier, randMultiplier, randMultiplier);

        health = 10 * randMultiplier;
    }

    void Update()
    {
        //transform.Rotate(vRandomRotation * Time.deltaTime);

        if (health <= 0)
        {
            Die();
        }
    }

    void ApplyDamage(int damage)
    {
        Debug.Log(health);
        health -= damage;
    }

    void Die()
    {
        if (explosion)
        {
            Instantiate(explosion, transform.position, transform.rotation);
        }

        int clones = 0;
        if (makeChildren)
        {
            if (randMultiplier == maxMultiplier)
            {
                clones = 3;
            }
            else if (randMultiplier == ((minMultiplier + maxMultiplier) / 2))
            {
                clones = 2;
            }
            else if (randMultiplier == minMultiplier)
            {
                clones = 1;
            }

            while (clones>0)
            {
                clones -= 1;

                Vector3 randPosPlus = new Vector3(Random.Range(-2, 2), Random.Range(-2, 2), Random.Range(-2, 2));

                if (newClone != null)
                {
                    GameObject clone = Instantiate(newClone, transform.position + randPosPlus, transform.rotation);
                    clone.name = "asteroid clone";
                    DamageDestroy cloneAstScr = clone.GetComponent<DamageDestroy>();
                    cloneAstScr.rand = false;
                    cloneAstScr.randMultiplier = 1;
                    cloneAstScr.makeChildren = false;
                }

                Collider[] colliders = Physics.OverlapSphere(transform.position, 3);
                foreach (var hit in colliders)
                {
                    if (hit.GetComponent<Rigidbody>())
                    {
                        hit.GetComponent<Rigidbody>().AddExplosionForce(100, transform.position, 5);
                    }
                }
                print("Создание клона астероида");
            }

        }
        if (clones <= 0)
        {
            //GameObject gameManagerObj = GameObject.Find("GameManager");
            //LocalValues localValues = gameManagerObj.GetComponent<LocalValues>();
            //localValues.points += points;
            Destroy(gameObject);
        }
    }
}