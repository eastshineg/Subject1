using Nexon;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NexonComponent : MonoBehaviour
{
	NexonObject _nexonObject;
	public NexonObject Object => _nexonObject;

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
	
	public void ChangeNexonObject(NexonObject nexonObject)
	{
		_nexonObject = nexonObject;
	}
}
