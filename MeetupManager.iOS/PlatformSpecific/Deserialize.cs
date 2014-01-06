using System;
using MeetupManager.Portable.Interfaces;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace MeetupManager.iOS.PlatformSpecific
{
	public class Deserialize : IDeserialize
	{
		public T DeserializeObject<T> (string value)
		{
			return JsonConvert.DeserializeObject<T> (value);
		}


		public Task<T> DeserializeObjectAsync<T> (string value)
		{
			return Task.Factory.StartNew (() => JsonConvert.DeserializeObject<T> (value));
		}

	}
}

