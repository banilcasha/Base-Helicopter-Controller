var explosionTime = 1;
var explosionRadius = 5;
var explosionPower = 2000;

function Start () {
	Destroy(gameObject, explosionTime);
	
	var colliders : Collider[] = Physics.OverlapSphere(transform.position, explosionRadius);
	for (var hit in colliders)
	{
		if (hit.GetComponent.<Rigidbody>())
		{
			hit.GetComponent.<Rigidbody>().AddExplosionForce(explosionPower, transform.position, explosionRadius);
		}
	}
}