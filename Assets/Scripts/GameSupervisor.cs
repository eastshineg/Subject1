using Nexon;
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
          var player = _objectManager.Claim(EnumObjectType.Player);
          var item = _objectManager.Claim(EnumObjectType.Item);
          if (player.IsValid == false)
          {
               Debug.LogError("player is not valid");
               return;
          }

          if (item.IsValid == false)
          {
               Debug.LogError("item is not valid");
               return;
          }

          var pos = player.Comp.transform.position;
          pos.z += 1.5f;
          player.Comp.transform.position = pos;
     }

     public void Update()
     {
          _objectManager.Update();
     }
}
