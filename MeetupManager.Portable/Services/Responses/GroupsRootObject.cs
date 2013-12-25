using System;
using System.Collections.Generic;
using MeetupManager.Portable.Models;

namespace MeetupManager.Portable.Services.Responses
{
	public class EventsRootObject
	{

		public List<Group> results { get; set; }
		public Meta meta { get; set; }
	}
}

