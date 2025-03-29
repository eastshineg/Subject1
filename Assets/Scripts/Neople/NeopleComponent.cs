using Neople;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NeopleComponent : MonoBehaviour
{
	NeopleObject _NeopleObject;
	public NeopleObject Object => _NeopleObject;

	protected GameSupervisor gs
	{
		get
		{
			if (GameSupervisor.TryGet(out var inst) == false)
			{
				return null;
			}

			return inst;
		}
	}
	
	public void ChangeNeopleObject(NeopleObject NeopleObject)
	{
		_NeopleObject = NeopleObject;
	}
}
