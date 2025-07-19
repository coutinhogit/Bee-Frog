using UnityEngine;

public class Water_applyDamage
{
    
  
  private void OnCollisionEnter2D(Collision2D other)
  {
    var player  = other.collider.GetComponent<Player>();

    if(player != null){
      player.DamageJump();

    }



  }

}
