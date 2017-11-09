using System;
using PetMeals.Helpers;
using SQLite;

namespace PetMeals.Models
{
    public class BaseDataObject : ObservableObject
    {
        public BaseDataObject()
        {
        }

        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
    }
}
