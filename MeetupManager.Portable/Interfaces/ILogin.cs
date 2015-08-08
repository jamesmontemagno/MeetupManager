
using System;
using System.Collections.Generic;

namespace MeetupManager.Portable.Interfaces
{
	public interface ILogin
	{
        void LoginAsync(Action<bool, Dictionary<string, string>> loginCallback);
	}
}

