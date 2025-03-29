using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NeoplePlayerComponent : NeopleComponent
{
    [SerializeField] NeopleColliderTrigger _colliderTigger;
    // Start is called before the first frame update

    int _itemLayer = 0;

    void Awake()
    {
        _itemLayer = LayerMask.NameToLayer("Item");
        _colliderTigger.ChangeReceiveLayer(_itemLayer);
        _colliderTigger.RegisterOnCollide(onCollide);
    }

    void onCollide(NeopleComponent otherNeopleComponent)
    {
        if (otherNeopleComponent == null)
        {
            return;
        }

        var colliderNeopleObject = otherNeopleComponent.Object;
        if (colliderNeopleObject == null ||
            colliderNeopleObject.IsValid == false)
        {
            return;
        }
        
        gs.HandleCollision(Object, colliderNeopleObject);
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
