using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NexonPlayerComponent : NexonComponent
{
    [SerializeField] NexonColliderTrigger _colliderTigger;
    // Start is called before the first frame update

    int _itemLayer = 0;

    void Awake()
    {
        _itemLayer = LayerMask.NameToLayer("Item");
        _colliderTigger.ChangeReceiveLayer(_itemLayer);
        _colliderTigger.RegisterOnCollide(onCollide);
    }

    void onCollide(NexonComponent otherNexonComponent)
    {
        if (otherNexonComponent == null)
        {
            return;
        }

        var colliderNexonObject = otherNexonComponent.Object;
        if (colliderNexonObject == null ||
            colliderNexonObject.IsValid == false)
        {
            return;
        }
        
        gs.HandleCollision(Object, colliderNexonObject);
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
