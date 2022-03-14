using DiverBoard.Base;
using DiverBoard.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DiverBoard.Models
{
    public class Bunk : ObservableObject
    {
        string _bunkNumber;
        string _diverName;
        Dictionary<int, Dive> _dives;
        Dive _activeDive;


        public Bunk(string bunkNumber)
        {
            _bunkNumber = bunkNumber;

        }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonPropertyOrderAttribute(2)]
        public Dictionary<int, Dive> Dives
        {
            get
            {
                if(_dives==null)
                {
                    _dives = new Dictionary<int, Dive>();
                    for (int diveNumber = 1; diveNumber < 31; diveNumber++)
                    {
                        _dives.Add(diveNumber, new Dive());
                    }
                }
                return _dives;
            }
            set
            {
                _dives = value;
            }
        }

        public string BunkNumber
        {
            get
            {
                return _bunkNumber;
            }
        }
        [JsonIgnore]
        public bool Occupied
        {
            get
            {
                return !string.IsNullOrEmpty(DiverName);
            }
        }

        [JsonPropertyOrderAttribute(1)]
        public string DiverName
        {
            get
            {
                return _diverName;
            }
            set
            {
                _diverName = value;
                RaisePropertyChangedEvent("DiverName");
                RaisePropertyChangedEvent("Occupied");
            }
        }
        [JsonIgnore]
        public string BunkDiver
        {
            get
            {
                return $"{_bunkNumber}\r\n{_diverName}";
            }
            set
            {
                _diverName = value;
                RaisePropertyChangedEvent("DiverName");
                RaisePropertyChangedEvent("Occupied");
            }
        }

        [JsonIgnore]
        public Dive ActiveDive
        {
            get
            {
                int activeDive = DataService.Instance.Trip.ActiveDive;
                if (!Dives.ContainsKey(activeDive))
                {
                    Dives.Add(activeDive, new Dive());
                }
                 return  Dives[activeDive];
            }
            set
            {
                _activeDive = value;
                RaisePropertyChangedEvent("ActiveDive");
            }
        }

    }
}
