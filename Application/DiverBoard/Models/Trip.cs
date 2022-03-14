using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Windows;
using DiverBoard.Base;

namespace DiverBoard.Models
{
    public class Trip : ObservableObject
    {
        private string _tripDate;
        private Dictionary<string, Bunk> _bunks;
        private int _dives = 7;
        private int _activeDive;

        public string TripDate 
        {
            get
            {
                return _tripDate;
            }

            set
            {
                _tripDate = value;
                RaisePropertyChangedEvent("Trip");
            }
        }

        public int ActiveDive
        {
            get
            {
                if (_activeDive == 0)
                {
                    _activeDive = 1;

                }
                return _activeDive;
            }

            set
            {
                _activeDive = value;
                RaisePropertyChangedEvent("ActiveDive");
                RaisePropertyChangedEvent("Trip");
            }
        }
        public int Dives
        {
            get
            {
                return _dives;
            }

            set
            {
                int oldDays = _dives;
                if (value < 2 || value > 5)
                {
                    _dives = oldDays;
                }
                else
                {
                    _dives = value;
                }
                RaisePropertyChangedEvent("Dives");
            }
        }

        public Dictionary<string, Bunk> Bunks 
        {
            get
            {
                return _bunks;
            }
            set 
            {
                _bunks = value;
                RaisePropertyChangedEvent("Trip");
            }
        }

        [JsonIgnore]
        public Dictionary<string, Bunk> OccupiedBunks
        {
            get
            {
                return _bunks.Where(p=>p.Value.Occupied==true).ToDictionary(p=>p.Key,p=>p.Value);
            }
        }
    }
}
