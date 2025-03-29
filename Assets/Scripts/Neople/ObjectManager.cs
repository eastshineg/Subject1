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
		
		public bool TryClaim(EnumObjectType objectType, out NeopleObject outNeopleObject)
		{
			outNeopleObject = null;
			
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
					outNeopleObject = obj;
					objList.RemoveAt(i);
					break;
				}
			}

			if (outNeopleObject == null)
			{
				switch (objectType)
				{
					case EnumObjectType.Player:
						outNeopleObject = new NeoplePlayObject();
						break;
					case EnumObjectType.Item:
						outNeopleObject = new NeopleItemObject();
						break;
					default:
						{
							Debug.LogError("Not implemented object type");
							return false;
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
						Debug.Log("Not implemented object type component " + objectType);
					}
					break;
			}

			comp = createComponent(prefabName);
			
			_keyId++;
			outNeopleObject.Initialize(_keyId, comp);

			// 생성된 데이터는 그냥 버린다.
			if (outNeopleObject.IsValid == false)
			{
				Release(outNeopleObject);
				return false;
			}
			
			_claimedDictObjects.Add(outNeopleObject);
			return true;
		}

		NeopleComponent createComponent(string prefabName)
		{
			if (string.IsNullOrEmpty(prefabName) == true)
			{
				return null;
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
			
			var comp = newGameObject.GetComponent<NeopleComponent>();
			if (comp == null)
			{
				Debug.LogError("Not exist Component target prefab");
			}

			return comp;
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