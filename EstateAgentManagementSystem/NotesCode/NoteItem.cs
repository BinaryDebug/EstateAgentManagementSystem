using System;

namespace EstateAgentManagementSystem
{
	public class NoteItem
	{
		public NoteItem()
		{
			Key = Guid.NewGuid ();
			Text = string.Empty;
		}

		public static string TEXT = "text";
	
		public static string KEY = "key";

		public Guid Key{ get; set; }

		public String Text{ set; get; }

		public override string ToString ()
		{
			return Text;
		}
	}
}