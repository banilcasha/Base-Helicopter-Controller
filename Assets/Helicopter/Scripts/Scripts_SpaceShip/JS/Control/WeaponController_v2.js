///////////////////////////////////
var Pulya : GameObject;
var PulyaSpeed = 50.0;
var shotSound : AudioClip; //Звук попадания по объект?
public var healthController : HealthController;
///////////////////////////////////
function Update () {
	if (Input.GetButtonDown("Fire1"))
	{
		Shot();
	}
}

function Shot() {
	var PulyaK = Instantiate(Pulya,transform.position,transform.rotation);
	var PulyaRBody = PulyaK.GetComponent(Rigidbody);
	PulyaRBody.velocity = transform.TransformDirection(Vector3(0,0,PulyaSpeed));
	if (shotSound) {
		AudioSource.PlayClipAtPoint(shotSound, transform.position, 0.6);
	}
	healthController.curHealth -= 1;
}