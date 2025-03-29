using System;
using UnityEngine;

public class NeopleColliderTrigger : MonoBehaviour
{
	int _receiveLayer = 0;
	public void ChangeReceiveLayer(int layer)
	{
		_receiveLayer = layer;
	}

	System.Action<NeopleComponent> _onCollide = null;
	public void RegisterOnCollide(System.Action<NeopleComponent> onCollide)
	{
		_onCollide = onCollide;
	}
	
	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.layer != _receiveLayer)
		{
			return;
		}

		var comp = other.gameObject.GetComponentInParent<NeopleComponent>();
		if (comp == null)
		{
			return;
		}
		
		_onCollide?.Invoke(comp);
	}

	void OnTriggerExit(Collider other)
	{
		if (other.gameObject.layer != _receiveLayer)
		{
			return;
		}	
	}
}