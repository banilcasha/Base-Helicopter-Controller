@script ExecuteInEditMode()

var blip : Texture; //Текстура для обохначения вражески?объектов
var blipChasing : Texture; //Текстура для обозначения сагревшихся вражески?объектов
var radarBG : Texture; //Фо?радара
var centerObject : Transform; //Центральны?объект
var mapScale = 0.3;
var mapSizePercent = 15;
var checkAIscript : boolean = true;
var enemyTag = "Enemy"; //Те?вражески?объектов

enum radarLocationValues {topLeft, topCenter, topRight, middleLeft, middleCenter, middleRight, bottomLeft, bottomCenter, bottomRight, custom}

var radarLocation : radarLocationValues = radarLocationValues.bottomLeft;

private var mapWidth : float;
private var mapHeight : float;
private var mapCenter : Vector2;
var mapCenterCustom : Vector2;

function OnGUI () {

    //Прорисовка радара
    var bX=centerObject.transform.position.x * mapScale;
    var bY=centerObject.transform.position.z * mapScale;   
    GUI.DrawTexture(Rect(mapCenter.x - mapWidth/2,mapCenter.y-mapHeight/2,mapWidth,mapHeight),radarBG);
   
    //Прорисовка врагов
    DrawBlipsForEnemies();
   
}

function Start () {
    setMapLocation();   
}
 
function DrawBlipsForEnemies(){

    var gos : GameObject[];
    gos = GameObject.FindGameObjectsWithTag(enemyTag);
 
    var distance = Mathf.Infinity;
    var position = transform.position;
 
    for (var go : GameObject in gos)  {
          var blipChoice : Texture = blip;
        drawBlip(go,blipChoice);
    }
 
}

function drawBlip(go : GameObject,aTexture : Texture){
   
    var centerPos=centerObject.position;
    var extPos=go.transform.position;
   
    //Расстояни?от игрока до вражеского объект?    
	var dist=Vector3.Distance(centerPos,extPos);
    
    var dx=centerPos.x-extPos.x; 
    var dz=centerPos.z-extPos.z; 
   
    var deltay=Mathf.Atan2(dx,dz)*Mathf.Rad2Deg - 270 - centerObject.eulerAngles.y;
   
    var bX=dist*Mathf.Cos(deltay * Mathf.Deg2Rad);
    var bY=dist*Mathf.Sin(deltay * Mathf.Deg2Rad);
   
    bX=bX*mapScale; 
    bY=bY*mapScale; 
   
    if(dist<=mapWidth*.5/mapScale){
        //Диамет?радара
       GUI.DrawTexture(Rect(mapCenter.x+bX,mapCenter.y+bY,4,4),aTexture);
 
    }
 
}

function setMapLocation () {
    mapWidth = Screen.width*mapSizePercent/100.0;
    mapHeight = mapWidth;

    if(radarLocation == radarLocationValues.topLeft){
        mapCenter = Vector2(mapWidth/2, mapHeight/2);
    } else if(radarLocation == radarLocationValues.topCenter){
        mapCenter = Vector2(Screen.width/2, mapHeight/2);
    } else if(radarLocation == radarLocationValues.topRight){
        mapCenter = Vector2(Screen.width-mapWidth/2, mapHeight/2);
    } else if(radarLocation == radarLocationValues.middleLeft){
        mapCenter = Vector2(mapWidth/2, Screen.height/2);
    } else if(radarLocation == radarLocationValues.middleCenter){
        mapCenter = Vector2(Screen.width/2, Screen.height/2);
    } else if(radarLocation == radarLocationValues.middleRight){
        mapCenter = Vector2(Screen.width-mapWidth/2, Screen.height/2);
    } else if(radarLocation == radarLocationValues.bottomLeft){
        mapCenter = Vector2(mapWidth/2, Screen.height - mapHeight/2);
    } else if(radarLocation == radarLocationValues.bottomCenter){
        mapCenter = Vector2(Screen.width/2, Screen.height - mapHeight/2);
    } else if(radarLocation == radarLocationValues.bottomRight){
        mapCenter = Vector2(Screen.width-mapWidth/2, Screen.height - mapHeight/2);
    } else if(radarLocation == radarLocationValues.custom){
        mapCenter = mapCenterCustom;
    }
   
} 