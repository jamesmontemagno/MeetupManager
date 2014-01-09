using System;
using System.Windows.Input;

namespace MeetupManager.Portable.Interfaces
{
	public interface IRemove
	{
		ICommand RemoveCommand { get; }
		bool CanRemove(int index);
	}
}

