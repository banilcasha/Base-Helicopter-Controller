var plane: Transform;
var Trail : ParticleSystem;
var Emission : float;
function Update(){
if(plane == null) return;
var spd = plane.GetComponent.<Rigidbody>().velocity.magnitude;
Trail.emissionRate=spd*10;
}