using System;

namespace PetMeals.Models
{
    public class Feeding : BaseDataObject
    {
        private int _petId = -1;
        public int PetId
        {
            get { return _petId; }
            set { SetProperty(ref _petId, value); }
        }

        private DateTime _timeFed = DateTime.Now;
        public DateTime TimeFed
        {
            get { return _timeFed; }
            set { SetProperty(ref _timeFed, value); }
        }
    }
}
