using System;

namespace MeetupManager.Portable.Models
{
	public class Meta
	{
		public Meta ()
		{
		}

		public string lon { get; set; }
		public int count { get; set; }
		public string link { get; set; }
		public string next { get; set; }
		public int total_count { get; set; }
		public string url { get; set; }
		public string id { get; set; }
		public string title { get; set; }
		public long updated { get; set; }
		public string description { get; set; }
		public string method { get; set; }
		public string lat { get; set; }
	}
}

