namespace MeetupManager.Portable.Interfaces
{
	public interface IMessageDialog
  {
    void SendMessage(string message, string title = null);
  }
}
