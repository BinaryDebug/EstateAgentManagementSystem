using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace EstateAgentManagementSystem
{
    public class NotesHomeFragment : ListFragment
    {
        private static int EDITOR_ACTIVITY_REQUEST = 1001;
        private static int MENU_DELETE_ID = 1002;
        private int currentNoteId;
        private List<NoteItem> notesList;
        private NotesDataSource datasource;

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            SetHasOptionsMenu(true);

            // Use this to return your custom view for this Fragment
            // return inflater.Inflate(Resource.Layout.YourFragment, container, false);

            View view = LayoutInflater.From(Activity).Inflate(Resource.Layout.MainNotes, null);

            return view;
        }

        public override void OnViewCreated(View view, Bundle savedInstanceState)
        {
            RegisterForContextMenu(ListView);
            datasource = new NotesDataSource(this.Context);
            refresh();
        }

        public override void OnCreateOptionsMenu(IMenu menu, MenuInflater inflater)
        {
            inflater.Inflate(Resource.Menu.menu_notes, menu);
        }
        public override Boolean OnOptionsItemSelected(IMenuItem item)
        {

            if (item.ItemId == Resource.Id.action_create)
            {
                NoteItem note = new NoteItem();
                Intent intent = new Intent(this.Activity, typeof(NoteEditorActivity));
                intent.PutExtra(NoteItem.KEY, note.Key.ToString());
                intent.PutExtra(NoteItem.TEXT, note.Text);
                StartActivityForResult(intent, EDITOR_ACTIVITY_REQUEST);
            }

            return base.OnOptionsItemSelected(item);
        }

        private void refresh()
        {
            notesList = datasource.GetAll();
            ArrayAdapter adapter = new ArrayAdapter(this.Context, Android.Resource.Layout.SimpleListItem1, notesList);
            ListAdapter = adapter;
        }

        public override void OnListItemClick(ListView l, View v, int position, long id)
        {
            NoteItem noteItem = notesList[position];
            Intent intent = new Intent(this.Activity, typeof(NoteEditorActivity));
            intent.PutExtra(NoteItem.KEY, noteItem.Key.ToString());
            intent.PutExtra(NoteItem.TEXT, noteItem.Text);
            StartActivityForResult(intent, EDITOR_ACTIVITY_REQUEST);
        }

        public override void OnCreateContextMenu(IContextMenu menu, View v, IContextMenuContextMenuInfo menuInfo)
        {
            AdapterView.AdapterContextMenuInfo info = (AdapterView.AdapterContextMenuInfo)menuInfo;
            currentNoteId = (int)info.Id;
            menu.Add(0, MENU_DELETE_ID, 0, Resource.String.delete);
        }

        public override bool OnContextItemSelected(IMenuItem item)
        {
            if (item.ItemId == MENU_DELETE_ID)
            {
                NoteItem note = notesList[currentNoteId];
                datasource.Remove(note);
                refresh();
            }

            return base.OnContextItemSelected(item);
        }

        public override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            if (requestCode == EDITOR_ACTIVITY_REQUEST && resultCode == Result.Ok)
            {
                NoteItem note = new NoteItem();
                note.Key = new Guid(data.GetStringExtra(NoteItem.KEY));
                note.Text = data.GetStringExtra(NoteItem.TEXT);
                datasource.Update(note);
                refresh();
            }
        }
    }
}