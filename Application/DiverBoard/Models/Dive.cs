using DiverBoard.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DiverBoard.Models
{
    public class Dive : ObservableObject
    {
        DateTime? _timeIn;
        DateTime? _timeOut;
        string _maxDepth;
        string _bottomTime;
        string _status;

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public DateTime? TimeIn
        {
            get
            {
                return _timeIn;
            }
            set
            {
                _timeIn = value;
                RaisePropertyChangedEvent("TimeIn");
                CalculateStatus();
            }
        }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public DateTime? TimeOut
        {
            get
            {
                return _timeOut;
            }
            set
            {
                _timeOut = value;
                RaisePropertyChangedEvent("TimeOut");
                CalculateStatus();
            }
        }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string MaxDepth
        {
            get
            {
                return _maxDepth;
            }
            set
            {
                _maxDepth = value;
                RaisePropertyChangedEvent("MaxDepth");
                CalculateStatus();
            }
        }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string  BottomTime
        {
            get
            {
                return _bottomTime;
            }
            set
            {
                _bottomTime = value;
                RaisePropertyChangedEvent("BottomTime");
                CalculateStatus();
            }
        }

        private void CalculateStatus()
        {
            if (TimeIn == null)
            {
                Status = "\r\n";
            }
            else if (TimeOut == null)
            {
                Status = $"In Water\r\n{TimeIn?.ToString("HH:mm")}";
            }
            else if(MaxDepth == null || BottomTime == null)
            {
                Status = $"Back\r\n{TimeOut?.ToString("HH:mm")}";
            }
            else
            {
                Status = $"Back\r\n{TimeOut?.ToString("HH:mm")}\r\n{MaxDepth}' for {BottomTime}";
            }
        }

        [JsonIgnore]
        public string Status
        {
            get
            {
                return _status;
            }
            set
            {
                _status = value;
                RaisePropertyChangedEvent("Status");
            }
        }

    }
}
