using System;
using System.Collections.Generic;
using System.Text;

namespace CsharpAdvanced
{
    public class PhotoProcessor
    {
        //EX. of custom delegate
        public delegate void PhotoFilterHandler(Photo photo);

        public void Process(string path, PhotoFilterHandler filterHandler)
        {
            //EX. of alternate use of delegates...
            //1. System.Action<> - points to a method that returns void
            //2. System.Func<> - points to a method that will return a value
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