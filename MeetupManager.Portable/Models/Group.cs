using System;

namespace MeetupManager.Portable.Models
{
	public class Group
	{
		public Group ()
		{
		}

		public int id { get; set; }
		public double group_lat { get; set; }
		public string name { get; set; }
		public double group_lon { get; set; }
		public string join_mode { get; set; }
		public string urlname { get; set; }
		public string who { get; set; }
	}
}

