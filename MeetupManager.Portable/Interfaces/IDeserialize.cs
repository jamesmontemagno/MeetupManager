using System;
using System.Threading.Tasks;

namespace MeetupManager.Portable.Interfaces
{
	public interface IDeserialize
	{
		Task<T> DeserializeObjectAsync<T> (string value);
		T DeserializeObject<T> (string value);
	}
}

