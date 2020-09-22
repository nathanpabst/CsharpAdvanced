using System;
using System.Collections.Generic;
using System.Text;

namespace CsharpAdvanced
{
    public class PhotoProcessor
    {
        public delegate void PhotoFilterHandler(Photo photo);

        public void Process(string path, PhotoFilterHandler filterHandler)
        {
            var photo = Photo.Load(path);

            filterHandler(photo);

            photo.Save();

            //the following code is no longer needed bc of the delegate
            //var filters = new PhotoFilters();
            //filters.ApplyBrightness(photo);
            //filters.ApplyContrast(photo);
            //filters.Resize(photo);
        }
    }
}