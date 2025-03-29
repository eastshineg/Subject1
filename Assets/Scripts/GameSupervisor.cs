using Neople;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Singleton(true)]
public class GameSupervisor : Singleton<GameSupervisor>
{
     ObjectManager _objectManager = new();

     public void Play()
     {
          if (_objectManager.TryClaim(EnumObjectType.Player, out var player) == false)
          {
               Debug.LogError("player is not valid");
               return;
          }

          if (_objectManager.TryClaim(EnumObjectType.Item, out var item) == false)
          {
               Debug.LogError("item is not valid");
               return;
          }
          
          var pos = player.Comp.transform.position;
          pos.z += 1.5f;
          player.Comp.transform.position = pos;
     }
     
     public void HandleCollision(NeopleObject collidee, NeopleObject collider)
     {
          if (collidee == null || collidee.IsValid == false) return;
          if (collider == null || collider.IsValid == false) return;

          collidee.OnCollision(collider);

          if (collider.ObjectType == EnumObjectType.Item)
          {
               _objectManager.Release(collider);
          }
     }

     public void Update()
     {
          _objectManager.Update();
     }
}
