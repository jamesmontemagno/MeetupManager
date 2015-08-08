
using System;
using System.Collections.Generic;
using MeetupManager.Portable.Models;
using Newtonsoft.Json;

namespace MeetupManager.Portable.Services.Responses
{
	public class EventsRootObject
	{
		[JsonProperty("results")]
		public List<Event> Events { get; set; }

		[JsonProperty("meta")]
		public Meta Metadata { get; set; }
	}
}

