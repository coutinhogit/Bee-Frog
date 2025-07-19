using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
  

    public Transform platform;
    public Transform pointA;
    public Transform pointB;
    public float speed = 1.5f;
    private int direction = 1;
    private float lerpTime = 0f;

    public bool startOnPointA = true;
    

    private void Start()
    {
        transform.position = (startOnPointA)?pointA.transform.position:pointB.transform.position;        
    }

    private void Update()
    {
        Vector2 target = (direction == 1)?pointB.position:pointA.position; 
       
        lerpTime = Time.deltaTime * speed / Vector2.Distance(platform.position, target);
       transform.position = Vector3.MoveTowards(transform.position, target,Time.deltaTime*speed);
        // platform.position = Vector2.Lerp();

        float distance = (target - (Vector2)transform.position).magnitude;
        if(distance <= 0.1f){
            direction = direction*-1;

        }

    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        var player = other.GetComponent<Player>();
        if (player){
            player.transform.parent = this.transform;
            player.IsJumping = false;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        var player = other.GetComponent<Player>();
        if (player){
            player.transform.parent = null;
            player.IsJumping = false;

        }




    }



    private void OnDrawGizmos()
    {
        if(platform != null && pointA != null && pointB != null){

            Gizmos.DrawLine(transform.position, pointA.position);
            Gizmos.DrawLine(transform.position, pointB.position);
        }
    }   

}