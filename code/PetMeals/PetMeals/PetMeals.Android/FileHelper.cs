using System;
using System.IO;
using Xamarin.Forms;
using PetMeals.Droid;
using PetMeals;

[assembly: Dependency(typeof(FileHelper))]
namespace PetMeals.Droid
{
    public class FileHelper : IFileHelper
    {
        public string GetLocalFilePath(string filename)
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            return Path.Combine(path, filename);
        }
    }
}