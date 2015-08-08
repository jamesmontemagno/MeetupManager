

using System;

namespace MeetupManager.Portable.Interfaces
{
  public interface IMessageDialog
  {
	    
    void SendMessage(string message, string title = null);
    void SendToast(string message);
	void SendConfirmation(string message, string title, Action<bool> confirmationAction);
	void AskForString(string message, string title, Action<string> returnString);
  }
}
