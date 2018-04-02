using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Speech.Tts;
using Android.Views;
using Android.Widget;
using Java.Util;

namespace EstateAgentManagementSystem
{
	[Activity (Label = "Editor", Theme = "@style/NotesEditTheme", Icon = "@drawable/ic_launcher")]			
	public class NoteEditorActivity : Activity, TextToSpeech.IOnInitListener
	{
		private NoteItem noteItem;
	    private TextToSpeech tts;
		protected override void OnCreate (Bundle bundle)
		{
            //ActionBar.Show();
			base.OnCreate (bundle);
			SetContentView (Resource.Layout.NoteEditor);
			//ActionBar.SetDisplayHomeAsUpEnabled (true);
			Intent intent = this.Intent;
			noteItem = new NoteItem ();
			noteItem.Key = new Guid(intent.GetStringExtra ("key"));
			noteItem.Text = intent.GetStringExtra ("text");

			EditText text = FindViewById<EditText> (Resource.Id.noteText);
			text.Text = noteItem.Text;
			text.SetSelection (noteItem.Text.Length);

		    Button btnSaveNote = FindViewById<Button>(Resource.Id.btnSaveNote);
		    btnSaveNote.Click += btnSaveNoteClick;

            tts = new TextToSpeech(this, this);
		    Button btnReadNote = FindViewById<Button>(Resource.Id.btnReadNote);
		    btnReadNote.Click += delegate
		    {
                SpeakOut();
		    };

        }

	    private void btnSaveNoteClick(object sender, EventArgs e)
	    {
	        saveAndFinish();
	    }

        private void saveAndFinish()
		{
			EditText text = FindViewById<EditText> (Resource.Id.noteText);
			String noteText = text.Text;
			Intent intent = new Intent ();
			intent.PutExtra ("key", noteItem.Key.ToString());
			intent.PutExtra ("text", noteText);
			SetResult (Result.Ok, intent);
			Finish();
		}

		public override bool OnOptionsItemSelected (IMenuItem item)
		{
			if (item.ItemId == Android.Resource.Id.Home) 
			{
				saveAndFinish();
			}
			return false;
		}

		protected override void OnResume ()
		{
			base.OnResume();
		}

		public override void OnBackPressed ()
		{
			saveAndFinish();
		}

        public void OnInit([GeneratedEnum] OperationResult status)
        {
            if (status == OperationResult.Success)
            {
                tts.SetLanguage(Locale.English);
            }
        }

	    private void SpeakOut()
	    {
	        EditText text = FindViewById<EditText>(Resource.Id.noteText);
	        if (!String.IsNullOrEmpty(text.Text))
	        {
	            tts.Speak(text.Text, QueueMode.Flush, null);
	        }
	    }
    }
}