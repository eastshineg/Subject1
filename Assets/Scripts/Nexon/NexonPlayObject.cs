using UnityEngine;
namespace Nexon
{
	public class PlayerBlackBoard : BlackBoard
	{
		public override EnumObjectType ObjectType => EnumObjectType.Player;
		public override void Clear()
		{
			
		}
	}
	
	public class NexonPlayObject : NexonObject
	{
		float _moveSpeed = 5.0f;
		bool _pressed = false;
		bool _isUp = false;
		bool _isDown = false;
		bool _isLeft = false;
		bool _isRight = false;

		void resetDir()
		{
			_isUp = false;
			_isDown = false;
			_isLeft = false;
			_isRight = false;
		}
		
		public override void Initialize(int objectId, NexonComponent comp)
		{
			base.Initialize(objectId, comp);
			_blackBoard ??= new PlayerBlackBoard();
		}

		public override void Update()
		{
			if(IsValid == false)
				return;
			
			base.Update();

			if (_pressed == false)
			{
				_isUp = Input.GetKey(KeyCode.S);				
				_isDown = Input.GetKey(KeyCode.W);
				_isLeft = Input.GetKey(KeyCode.A);
				_isRight = Input.GetKey(KeyCode.D);
				
				_pressed  = _isUp || _isDown || _isLeft || _isRight;	
			}

			if (_pressed == true)
			{
				if (!Input.GetKey(KeyCode.W) && 
				    !Input.GetKey(KeyCode.S) && 
				    !Input.GetKey(KeyCode.A) && 
				    !Input.GetKey(KeyCode.D))
				{
					_pressed = false;
					resetDir();
				}
				else
				{
					_isUp = Input.GetKey(KeyCode.S);				
					_isDown = Input.GetKey(KeyCode.W);
					_isLeft = Input.GetKey(KeyCode.A);
					_isRight = Input.GetKey(KeyCode.D);
				}
			}

			if (_pressed == true)
			{
				var deltaTime = Time.deltaTime;
				var mv = deltaTime * _moveSpeed;
				if (_isUp)
				{
					Comp.transform.position += (Vector3.forward * mv);
				}
				else if (_isDown)
				{
					Comp.transform.position -= (Vector3.forward * mv);
				}
				else if (_isLeft)
				{
					Comp.transform.position += (Vector3.right * mv);
				}
				else if (_isRight)
				{
					Comp.transform.position += (Vector3.left * mv);
				}	
			}
		}
	}
}