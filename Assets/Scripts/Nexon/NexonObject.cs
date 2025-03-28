using UnityEngine;

namespace Nexon 
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
	
	public class NexonObject
	{
		int _objectId = 0;
		// gameobject는 
		public NexonComponent Comp { get; private set; }  
		protected BlackBoard _blackBoard = null;
		public virtual EnumObjectType ObjectType => _blackBoard == null ? EnumObjectType.None : _blackBoard.ObjectType;
		public void Release()
		{
			_objectId = 0;
			
			if (Comp != null)
			{
				GameObject.DestroyImmediate(Comp.gameObject);
				Comp = null;
			}

			_blackBoard?.Clear();
			_blackBoard = null;
		}
		

		public bool IsValid
		{
			get
			{
				if (_objectId == 0) return false;
				if (_blackBoard == null) return false;
				if (Comp == null) return false;
				return true;
			}
		}
		
		public virtual void Initialize(int objectId, NexonComponent comp)
		{
			if (comp == null) return;
			
			_objectId = objectId;
			Comp = comp;
		}
		
		public virtual void Update()
		{
			
		}
	}
}