using UnityEngine;
namespace Neople
{
	public class PlayerBlackBoard : BlackBoard
	{
		public float OriginSpeed { get; private set; } = 5.0f;
		public float CurrSpeed { get; private set; } = 5.0f;
		public float ItemEffectEndTime { get; private set; } = 0.0f;
		public override EnumObjectType ObjectType => EnumObjectType.Player;
		public void ChangeMoveSpeed(float speed)
		{
			CurrSpeed = speed;
		}
		
		public void ChangeItemEffectEndTime(float time)
		{
			ItemEffectEndTime = time;
		}

		public void ResetMoveSpeed()
		{
			CurrSpeed = OriginSpeed;
		}
		
		public override void Clear()
		{
			ResetMoveSpeed();
		}
	}
	
	public class NeoplePlayObject : NeopleObject
	{
		PlayerBlackBoard _cachedBlackBoard = null;
		PlayerBlackBoard PlayerBoard
		{
			get
			{
				if (_cachedBlackBoard == null) _cachedBlackBoard = _blackBoard as PlayerBlackBoard;
				return _cachedBlackBoard;
			}
		}

		public override bool IsValid
		{
			get
			{
				if (base.IsValid == false) return false;
				if (Comp == null) return false;
				return true;
			}
		}

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
		
		public override void Initialize(int objectId, NeopleComponent comp)
		{
			base.Initialize(objectId, comp);
			_blackBoard ??= new PlayerBlackBoard();
		}

		public override void Release()
		{
			base.Release();
		}

		public override void OnCollision(NeopleObject colliderObject)
		{
			base.OnCollision(colliderObject);

			if (colliderObject == null ||
			    colliderObject.IsValid == false)
			{
				return;
			}

			if (colliderObject.ObjectType != EnumObjectType.Item)
			{
				return;
			}
			
			var itemObject = colliderObject as NeopleItemObject;
			
			var playerBlackBoard = PlayerBoard;
			playerBlackBoard.ChangeMoveSpeed(playerBlackBoard.OriginSpeed * itemObject.CachedBlackBoard.SpeedEffect);
			playerBlackBoard.ChangeItemEffectEndTime(Time.realtimeSinceStartup + itemObject.CachedBlackBoard.EffectDuration);
			
			Debug.Log("Change Speed " + playerBlackBoard.CurrSpeed);
		}

		void updateTransfromByInput()
		{
			if (PlayerBoard == null)
			{
				return;
			}
			
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
				var mv = deltaTime * PlayerBoard.CurrSpeed;
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

		public override void Update()
		{
			if(IsValid == false)
				return;
			
			base.Update();

			updateTransfromByInput();
			updateSpeed();
		}

		void updateSpeed()
		{
			if (_blackBoard == null)
			{
				return;
			}

			var playerBlackBoard = PlayerBoard;
			if (playerBlackBoard.ItemEffectEndTime <= 0f)
			{
				return;
			}
			
			if (Time.realtimeSinceStartup < playerBlackBoard.ItemEffectEndTime)
			{
				return;
			}
			
			Debug.Log("Rseet Speed");
			playerBlackBoard.ResetMoveSpeed();
			playerBlackBoard.ChangeItemEffectEndTime(0f);
		}
	}
}