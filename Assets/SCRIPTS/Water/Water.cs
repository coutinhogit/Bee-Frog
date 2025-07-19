using UnityEngine;

public class Water : MonoBehaviour
{
   
  private void OnCollisionEnter2D(Collision2D other)
  {
    var player  = other.collider.GetComponent<Player>();

    if(player != null){
      player.DamageJump();

    }



  }
}
