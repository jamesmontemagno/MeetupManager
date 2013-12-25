using System;

namespace MeetupManager.Portable.Models
{
	public class MemberPhoto
	{
		public MemberPhoto ()
		{
		}

		public string photo_link { get; set; }
		public string highres_link { get; set; }
		public string thumb_link { get; set; }
		public int photo_id { get; set; }
	}
}

