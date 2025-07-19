using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
      
    public Transform player;
    public float minX, maxX;
    public float minY, maxY;
    public float timeLerp;
   
   


   private void FixedUpdate() {

    Vector3 newPosition = player.position + new Vector3(0, 0, -10);
    newPosition = Vector3.Lerp(transform.position,newPosition, timeLerp);
    transform.position = newPosition;
    transform.position = new Vector3(Mathf.Clamp(transform.position.x, minX, maxX), Mathf.Clamp(transform.position.y, minY, maxY), transform.position.z);
   } 




}
   
   
