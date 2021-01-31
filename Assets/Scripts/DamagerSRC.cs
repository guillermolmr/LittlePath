using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagerSRC : MonoBehaviour
{
  private Collider2D collider;
    public float Damage_ = 0.0f;
  private void Start()
  {
    collider = GetComponent<Collider2D>();

  }
  
  private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
      collider.enabled = false;
      transform.parent.SendMessage("Stun");
      collision.gameObject.GetComponent<MyPlayerSRC>().DamagePlayer(Damage_);
            
        }
    }

   public void UnStun()
  {
    if(!collider.enabled)
    collider.enabled = true;
  }
}
