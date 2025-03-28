namespace Nexon
{
	public class ItemBlackBoard : BlackBoard
	{
		public override EnumObjectType ObjectType => EnumObjectType.Item;
		public override void Clear()
		{
			
		}
	}
	
	public class NexonItemObject : NexonObject
	{
		public override void Initialize(int objectId, NexonComponent comp)
		{
			base.Initialize(objectId, comp);
			_blackBoard ??= new ItemBlackBoard();
		}
	}
}