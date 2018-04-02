using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using System.Globalization;
using Android.Graphics;
using Android.Provider;
using Java.IO;
using Environment = Android.OS.Environment;


namespace EstateAgentManagementSystem
{
    class CameraFragment : Fragment
    {
        private ImageView _imageView;
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View view = LayoutInflater.From(Activity).Inflate(Resource.Layout.CameraView, null);

            App._dir = new File(
                Environment.GetExternalStoragePublicDirectory(
                    Android.OS.Environment.DirectoryPictures), "CameraAppDemo");
            if (!App._dir.Exists())
            {
                App._dir.Mkdirs();
            }

            Button button = view.FindViewById<Button>(Resource.Id.myButton);
            _imageView = view.FindViewById<ImageView>(Resource.Id.imageView1);
            button.Click += TakeAPicture;

            Intent intent = new Intent(MediaStore.ActionImageCapture);
            App._file = new File(App._dir, String.Format("myPhoto_{0}.jpg", Guid.NewGuid()));
            intent.PutExtra(MediaStore.ExtraOutput, App._file.AbsolutePath);
            StartActivityForResult(intent, 0);

            return view;
        }

        private void TakeAPicture(object sender, EventArgs eventArgs)
        {
            Intent intent = new Intent(MediaStore.ActionImageCapture);
            App._file = new File(App._dir, String.Format("myPhoto_{0}.jpg", Guid.NewGuid()));
            intent.PutExtra(MediaStore.ExtraOutput, App._file.AbsoluteFile);
            StartActivityForResult(intent, 0);
        }

        public override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);

            // Make it available in the gallery

            Intent mediaScanIntent = new Intent(Intent.ActionMediaScannerScanFile);
            //Uri contentUri = App._file.AbsoluteFile.ToURI();
            //mediaScanIntent.SetData(App._file.AbsoluteFile.ToURI());
            //SendBroadcast(mediaScanIntent);

            // Display in ImageView. We will resize the bitmap to fit the display.
            // Loading the full sized image will consume to much memory
            // and cause the application to crash.

            try
            {
                App.bitmap = (Bitmap) data.Extras.Get("data");
                _imageView.SetImageBitmap(App.bitmap);
            }
            catch (Exception)
            { }


            //int height = Resources.DisplayMetrics.HeightPixels;
            //int width = _imageView.Height;
            //App.bitmap = App._file.Path.LoadAndResizeBitmap(width, height);
            //if (App.bitmap != null)
            //{
            //    _imageView.SetImageBitmap(App.bitmap);
            //    App.bitmap = null;
            //}

            // Dispose of the Java side bitmap.
            //GC.Collect();
        }
    }

    public static class BitmapHelpers
    {
        public static Bitmap LoadAndResizeBitmap(this string fileName, int width, int height)
        {
            // First we get the the dimensions of the file on disk
            BitmapFactory.Options options = new BitmapFactory.Options { InJustDecodeBounds = true };
            BitmapFactory.DecodeFile(fileName, options);

            // Next we calculate the ratio that we need to resize the image by
            // to fit the requested dimensions.
            int outHeight = options.OutHeight;
            int outWidth = options.OutWidth;
            int inSampleSize = 1;

            if (outHeight > height || outWidth > width)
            {
                inSampleSize = outWidth > outHeight
                    ? outHeight / height
                    : outWidth / width;
            }

            // Now we will load the image and have BitmapFactory resize it for us.
            options.InSampleSize = inSampleSize;
            options.InJustDecodeBounds = false;
            Bitmap resizedBitmap = BitmapFactory.DecodeFile(fileName, options);

            return resizedBitmap;
        }
    }
    public static class App
    {
        public static File _file;
        public static File _dir;
        public static Bitmap bitmap;
    }

}