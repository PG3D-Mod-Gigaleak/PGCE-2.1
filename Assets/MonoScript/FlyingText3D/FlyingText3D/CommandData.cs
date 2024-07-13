namespace FlyingText3D
{
	public class CommandData
	{
		public int index;

		public Command command;

		public object data;

		public CommandData(int index, Command command, object data)
		{
			this.index = index;
			this.command = command;
			this.data = data;
		}
	}
}
