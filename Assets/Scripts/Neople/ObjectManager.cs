using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace Neople
{
	public class ObjectManager
	{
		int _keyId = 0;
		Dictionary<EnumObjectType, List<NeopleObject>> _dictObjects = new();
		HashSet<NeopleObject> _claimedDictObjects = new();
		
		public void Release(NeopleObject NeopleObject)
		{
			if (NeopleObject == null) return;
			// 중복 해제 방지
			if (NeopleObject.IsValid == false) return;
			if (_dictObjects.TryGetValue(NeopleObject.ObjectType, out var objList) == false)
			{
				Debug.LogError("Not found object list");
				return;
			}
			
			NeopleObject.Release();
			objList.Add(NeopleObject);

			_claimedDictObjects.Remove(NeopleObject);
		}
		
		public NeopleObject Claim(EnumObjectType objectType)
		{
			NeopleObject newObj = null;
			
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
						newObj = new NeoplePlayObject();
						break;
					case EnumObjectType.Item:
						newObj = new NeopleItemObject();
						break;
					default:
						{
							Debug.LogError("Not implemented object type");
							return null;
						}
				}	
			}

			NeopleComponent comp = null;
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
			var newGameObject = newObjectInst as GameObject;
			if (newGameObject == null)
			{
				GameObject.DestroyImmediate(newObjectInst);
				return null;
			}
			
			comp = newGameObject.GetComponent<NeopleComponent>();
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