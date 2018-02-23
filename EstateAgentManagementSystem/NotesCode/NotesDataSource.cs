using System;
using System.Collections;
using System.Collections.Generic;

using Android.Content;
using Android.Preferences;

namespace EstateAgentManagementSystem

{
    public class NotesDataSource
	{
		private string SHARED_KEY = "NOTES";
		private ISharedPreferences notePrefs;

		public NotesDataSource(Context context){
			notePrefs = context.GetSharedPreferences(SHARED_KEY, FileCreationMode.Private);
		}

		public List<NoteItem> GetAll()
		{
			List<NoteItem> noteList = new List<NoteItem> ();
			IDictionary<string, object> all = notePrefs.All;
			ICollection<string> keys = all.Keys;

			foreach (var key in keys) {
				NoteItem note = new NoteItem ();
				note.Key = new Guid(key);
				note.Text = (string)all[key];
				noteList.Add (note);
			}

			return noteList;
		}

		public bool Update(NoteItem note)
		{
			ISharedPreferencesEditor editor = notePrefs.Edit();
			editor.PutString(note.Key.ToString(), note.Text);
			editor.Commit();
			return true;
		}

		public bool Remove(NoteItem note)
		{
			if (notePrefs.Contains(note.Key.ToString())) {
				ISharedPreferencesEditor editor = notePrefs.Edit();
				editor.Remove(note.Key.ToString());
				editor.Commit();
			}

			return true;
		}
	}
}