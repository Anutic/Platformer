using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
   protected int lives;
   public virtual void GetDamage()
   {
      lives--;
      if (lives < 1)
         Die();
   }

   public int GetHeal()
   {
      int temp = lives;
      temp++;
      return temp;
   }
   public virtual void Die()
   {
      Destroy(this.gameObject);
   }

   /*private void OnTriggerEnter2D(Collider2D collision)
   {
      if (collision.CompareTag("Player"))
      {
         HealthSystem.health -= 1;
      }
   }*/
}
