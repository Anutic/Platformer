using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class FlyingMonster : Entity
{
   private SpriteRenderer sprite;
   [SerializeField] private AIPath aiPath;

   private void Awake()
   {
      sprite = GetComponentInChildren<SpriteRenderer>();
      lives = 2;
   }

   void Update()
   {
      sprite.flipX = aiPath.desiredVelocity.x >= 0.1f;
   }
   private void OnCollisionEnter2D(Collision2D collision)
   {
      if (collision.gameObject == Hero.Instance.gameObject)
      {
         Hero.Instance.GetDamage();
         //OnTrigger
      }
   }
}
