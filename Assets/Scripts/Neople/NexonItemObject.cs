namespace Nexon
{
	public class ItemBlackBoard : BlackBoard
	{
		public override EnumObjectType ObjectType => EnumObjectType.Item;
		public float SpeedEffect { get; private set; } = 2.0f;
		public float EffectDuration { get; private set; } = 10.0f;
		public override void Clear()
		{
			
		}
	}
	
	public class NexonItemObject : NexonObject
	{
		ItemBlackBoard _cachedBlackBoard = null;
		public ItemBlackBoard CachedBlackBoard => _cachedBlackBoard;
		public override void Initialize(int objectId, NexonComponent comp)
		{
			base.Initialize(objectId, comp);
			_blackBoard ??= new ItemBlackBoard();
			_cachedBlackBoard = _blackBoard as ItemBlackBoard;
		}
	}
}