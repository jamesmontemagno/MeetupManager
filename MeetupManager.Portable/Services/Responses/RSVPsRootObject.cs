using System;
using MeetupManager.Portable.Models;
using System.Collections.Generic;

namespace MeetupManager.Portable.Services.Responses
{
	public class RSVPsRootObject
	{
		public RSVPsRootObject ()
		{
		}

		public List<RSVP> results { get; set; }
		public Meta meta { get; set; }
	}
}

