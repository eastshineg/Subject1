using UnityEngine;

namespace Neople 
{
	public enum EnumObjectType
	{
		None = 0,
		Player = 1,
		Item = 2,
	}
	
	public abstract class BlackBoard
	{
		public virtual EnumObjectType ObjectType => EnumObjectType.None;
		public abstract void Clear();
	}
	
	public class NeopleObject
	{
		int _objectId = 0;
		// gameobject는 
		public NeopleComponent Comp { get; private set; }  
		protected BlackBoard _blackBoard = null;
		public virtual EnumObjectType ObjectType => _blackBoard == null ? EnumObjectType.None : _blackBoard.ObjectType;
		public virtual void Release()
		{
			_objectId = 0;
			
			if (Comp != null)
			{
				// destroy immediately는 쓰지 말것
				// physics이벤트중에는 엔진에서 허용하지 않는다. 
				GameObject.Destroy(Comp.gameObject);
				Comp = null;
			}

			_blackBoard?.Clear();
			_blackBoard = null;
		}
		

		public virtual bool IsValid
		{
			get
			{
				if (_objectId == 0) return false;
				if (_blackBoard == null) return false;
				return true;
			}
		}
		
		public virtual void Initialize(int objectId, NeopleComponent comp)
		{
			if (comp == null) return;
			
			_objectId = objectId;
			Comp = comp;
			comp.ChangeNeopleObject(this);
		}

		public virtual void OnCollision(NeopleObject colliderObject)
		{
			
		}
		
		public virtual void Update()
		{
			
		}
	}
}