using System;
using System.Threading.Tasks;

namespace MeetupManager.Portable.Interfaces
{
	public interface ILogin
	{
		void LoginAsync(Action test);
		string AccountId { get; }
	}
}

