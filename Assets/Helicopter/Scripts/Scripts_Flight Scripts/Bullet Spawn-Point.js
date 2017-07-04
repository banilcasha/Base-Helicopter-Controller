var bullet : Rigidbody; 
var speed : float = 10.0f;
var muzzlePoint : Transform; 
 
var Spawn : Transform;
var Missle : Rigidbody;
var waitTime : int;
var speed_missile : int;
private var ray : Ray;

private var hit : RaycastHit;

function Update() {
	
    if(Input.GetButtonDown("Fire1")) {
        var instance : Rigidbody = Instantiate(bullet, muzzlePoint.position, 
                                               muzzlePoint.rotation);
        instance.velocity = muzzlePoint.forward * speed;
    }

	if(Input.GetKeyDown(KeyCode.Mouse1))
	{
		instance = Instantiate(Missle, Spawn.position, Spawn.rotation);
		instance.velocity = Spawn.forward * speed;
	}

	ray = Camera.main.ScreenPointToRay(Input.mousePosition);

    if(Physics.Raycast(ray, hit)){
    var point: Vector3 = hit.point;

        Spawn.transform.LookAt(point);
    }
}