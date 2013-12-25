using System;

namespace MeetupManager.Portable.Models
{
	public class Venue
	{
		public Venue ()
		{
		}

		public int id { get; set; }
		public string zip { get; set; }
		public double lon { get; set; }
		public bool repinned { get; set; }
		public string name { get; set; }
		public string state { get; set; }
		public string address_1 { get; set; }
		public double lat { get; set; }
		public string city { get; set; }
		public string country { get; set; }

	}
}

