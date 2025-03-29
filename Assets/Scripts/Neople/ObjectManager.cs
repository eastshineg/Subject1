using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace Nexon
{
	public class ObjectManager
	{
		int _keyId = 0;
		Dictionary<EnumObjectType, List<NexonObject>> _dictObjects = new();
		HashSet<NexonObject> _claimedDictObjects = new();
		
		public void Release(NexonObject nexonObject)
		{
			if (nexonObject == null) return;
			// 중복 해제 방지
			if (nexonObject.IsValid == false) return;
			if (_dictObjects.TryGetValue(nexonObject.ObjectType, out var objList) == false)
			{
				Debug.LogError("Not found object list");
				return;
			}
			
			nexonObject.Release();
			objList.Add(nexonObject);

			_claimedDictObjects.Remove(nexonObject);
		}
		
		public NexonObject Claim(EnumObjectType objectType)
		{
			NexonObject newObj = null;
			
			if (_dictObjects.TryGetValue(objectType, out var objList) == false)
			{
				objList = new();
				_dictObjects.Add(objectType, objList);
			}
			
			for(int i = 0; i < objList.Count; i++)
			{
				var obj = objList[i];
				if (obj.IsValid == false)
				{
					newObj = obj;
					objList.RemoveAt(i);
					break;
				}
			}

			if (newObj == null)
			{
				switch (objectType)
				{
					case EnumObjectType.Player:
						newObj = new NexonPlayObject();
						break;
					case EnumObjectType.Item:
						newObj = new NexonItemObject();
						break;
					default:
						{
							Debug.LogError("Not implemented object type");
							return null;
						}
				}	
			}

			NexonComponent comp = null;
			string prefabName = string.Empty;
			switch (objectType)
			{
				case EnumObjectType.Player:
					prefabName = "player";
					break;
				case EnumObjectType.Item:
					prefabName = "item";
					break;
				default:
					{
						Debug.LogError("Not implemented object type");
						return null;
					}
			}	
			var prefabObj = Resources.Load(prefabName, typeof(GameObject));
			if (prefabObj == null)
			{
				Debug.LogError("Not found prefab");
				return null;
			}

			var newObjectInst = GameObject.Instantiate(prefabObj);
			if (newObjectInst == null) return null;
			var newNexonGameObject = newObjectInst as GameObject;
			if (newNexonGameObject == null)
			{
				GameObject.DestroyImmediate(newObjectInst);
				return null;
			}
			
			comp = newNexonGameObject.GetComponent<NexonComponent>();
			if (comp == null)
			{
				Debug.LogError("Not exist Component target prefab");
				return null;
			}

			_keyId++;
			newObj.Initialize(_keyId, comp);
			_claimedDictObjects.Add(newObj);
			return newObj;
		}

		public void Update()
		{
			foreach (var obj in _claimedDictObjects)
			{
				if (obj.IsValid == false)
				{
					continue;
				}
				
				obj.Update();
			}
		}
	}
}