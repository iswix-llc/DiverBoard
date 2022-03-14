using DiverBoard.Enums;
using DiverBoard.Models;
using DiverBoard.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace DiverBoard.Services
{
    public sealed class NavigationService
    {
        string _command;
        DiveMode _diveMode = DiveMode.Splash;

        private static readonly Lazy<NavigationService> lazy = new Lazy<NavigationService>(() => new NavigationService());

        public static NavigationService Instance { get { return lazy.Value; } }

        private NavigationService()
        {
        }

        public Frame MainFrame { get; set; }

        public string Command
        {
            get
            {
                return _command;
            }

            set
            {
                _command = value;
            }
        }
        public DiveMode DiveMode
        {
            get
            {
                return _diveMode;
            }

            set
            {
                _diveMode = value;
            }
        }

    }
}
