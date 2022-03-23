using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using Microsoft.Win32;
using DiverBoard.Base;
using DiverBoard.Models;
using DiverBoard.Services;
using DiverBoard.Types;
using DiverBoard.Views;
using DiverBoard.Enums;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Media;

namespace DiverBoard.ViewModels
{
    internal class TripViewModel : ObservableObject
    {
        Trip _trip;
        Visibility _visibility;
        Visibility _backVisibility;
        Visibility _forwardVisibility;
        DateTime? _tripDate;
        DateTime? _newTripDate;
        private Visibility _thumbDriveInserted;
        private int _activeDive;
        private int _splashBorder;
        private int _unSplashBorder;
        private int _climbBorder;
        private int _unClimbBorder;
        private ICommand _openTripCommand;
        private ICommand _newTripCommand;
        private ICommand _navigateMainPageCommand;
        private ICommand _navigateConfigurePageCommand;
        private ICommand _quitCommand;
        private ICommand _okCommand;
        private ICommand _cancelCommand;
        private ICommand _forwardCommand;
        private ICommand _backCommand;
        private ICommand _splashCommand;
        private ICommand _unSplashCommand;
        private ICommand _climbCommand;
        private ICommand _unClimbCommand;
        private ICommand _archiveCommand;

        public TripViewModel()
        {
            TripLoaded = Visibility.Hidden;
            RaisePropertyChangedEvent("Visibility");
            if (DesignerProperties.GetIsInDesignMode(new DependencyObject()))
            {
                TripLoaded = Visibility.Visible;
            }
            else
            {
                Trip = DataService.Instance.Trip;
                if(Trip != null)
                {
                    UpdateBackForwardVisibility();
                }
            }

            if(NavigationService.Instance.DiveMode==DiveMode.Splash)
            {
                Splash();
            }
            else if(NavigationService.Instance.DiveMode== DiveMode.Unsplash)
            {
                UnSplash();
            }
            else if(NavigationService.Instance.DiveMode==DiveMode.Climb)
            {
                Climb();
            }
            else
            {
                UnClimb();
            }

            if (GetAllRemovableDrives().Count() == 0)
            {
                ThumbDriveInserted = Visibility.Hidden;
            }
            else
            {
                ThumbDriveInserted = Visibility.Visible;
            }
        }

        public Visibility TripLoaded { get { return _visibility; } set { _visibility = value; RaisePropertyChangedEvent("TripLoaded"); } }

        public Visibility BackVisibility { get { return _backVisibility; } set { _backVisibility = value; RaisePropertyChangedEvent("BackVisibility"); } }
        public Visibility ForwardVisibility { get { return _forwardVisibility; } set { _forwardVisibility = value; RaisePropertyChangedEvent("ForwardVisibility"); } }

        public Visibility ThumbDriveInserted
        {
            get
            {
                return _thumbDriveInserted;
            }

            set
            {
                _thumbDriveInserted = value;
                RaisePropertyChangedEvent("ThumbDriveInserted");
            }
        }
        public Trip Trip
        {
            get 
            {
                if (_trip != null)
                {
                    TripLoaded = Visibility.Visible;
                    ActiveDive = _trip.ActiveDive;
                }
                return _trip;
            }

            set 
            { 
                _trip = value;
                RaisePropertyChangedEvent("Trip");
            }
        }
        public DateTime? NewTripDate
        {
            get
            {
                return _newTripDate;
            }

            set
            {
                _newTripDate = value;
                RaisePropertyChangedEvent("NewTripDate");
            }
        }
        public int UnSplashBorder
        {
            get
            {
                return _unSplashBorder;
            }
            set
            {
                _unSplashBorder = value;
                RaisePropertyChangedEvent("UnSplashBorder");
            }
        }
        public int SplashBorder
        {
            get
            {
                return _splashBorder;
            }
            set
            {
                _splashBorder = value;
                RaisePropertyChangedEvent("SplashBorder");
            }
        }

        public int ClimbBorder
        {
            get
            {
                return _climbBorder;
            }
            set
            {
                _climbBorder = value;
                RaisePropertyChangedEvent("ClimbBorder");
            }
        }


        public int UnClimbBorder
        {
            get
            {
                return _unClimbBorder;
            }
            set
            {
                _unClimbBorder = value;
                RaisePropertyChangedEvent("UnClimbBorder");
            }
        }


        public ICommand NavigateMainPageCommand
        {
            get
            {
                return _navigateMainPageCommand ?? (_navigateMainPageCommand = new CommandHandler(() => NavigateMainPage(), true));
            }
        }
        public ICommand NavigateConfigurePageCommand
        {
            get
            {
                return _navigateConfigurePageCommand ?? (_navigateConfigurePageCommand = new CommandHandler(() => NavigateConfigurePage(), true));
            }
        }
        public ICommand NewTripCommand
        {
            get
            {
                return _newTripCommand ?? (_newTripCommand = new CommandHandler(() => NewTrip(), true));
            }
        }
        public ICommand OpenTripCommand
        {
            get
            {
                return _openTripCommand ?? (_openTripCommand = new CommandHandler(() => OpenTrip(), true));
            }
        }

        public ICommand QuitCommand
        {
            get
            {
                return _quitCommand ?? (_quitCommand = new CommandHandler(() => Quit(), true));
            }
        }

        public ICommand OkCommand
        {
            get
            {
                return _okCommand ?? (_okCommand = new CommandHandler(() => Ok(), true));
            }
        }

        public ICommand CancelCommand
        {
            get
            {
                return _cancelCommand ?? (_cancelCommand = new CommandHandler(() => Cancel(), true));
            }
        }

        public ICommand ForwardCommand
        {
            get
            {
                return _forwardCommand ?? (_forwardCommand = new CommandHandler(() => Forward(), true));
            }
        }

        public ICommand BackCommand
        {
            get
            {
                return _backCommand ?? (_backCommand = new CommandHandler(() => Back(), true));
            }
        }

        public ICommand SplashCommand
        {
            get
            {
                return _splashCommand ?? (_splashCommand = new CommandHandler(() => Splash(), true));
            }
        }
        public ICommand UnSplashCommand
        {
            get
            {
                return _unSplashCommand ?? (_unSplashCommand = new CommandHandler(() => UnSplash(), true));
            }
        }

        public ICommand ClimbCommand
        {
            get
            {
                return _climbCommand ?? (_climbCommand = new CommandHandler(() => Climb(), true));
            }
        }
        public ICommand UnClimbCommand
        {
            get
            {
                return _unClimbCommand ?? (_unClimbCommand = new CommandHandler(() => UnClimb(), true));
            }
        }

        public ICommand ArchiveCommand
        {
            get
            {
                return _archiveCommand ?? (_archiveCommand = new CommandHandler(() => Archive(), true));
            }
        }

        public void Splash()
        {
            SplashBorder = 5;
            UnSplashBorder = 0;
            ClimbBorder = 0;
            UnClimbBorder = 0;
            NavigationService.Instance.DiveMode = DiveMode.Splash;
        }

        public void UnSplash()
        {
            SplashBorder = 0;
            UnSplashBorder = 5;
            ClimbBorder = 0;
            UnClimbBorder = 0;
            NavigationService.Instance.DiveMode = DiveMode.Unsplash;
        }

        public void Climb()
        {
            SplashBorder = 0;
            UnSplashBorder = 0;
            ClimbBorder = 5;
            UnClimbBorder = 0;
            NavigationService.Instance.DiveMode = DiveMode.Climb;
        }

        public void UnClimb()
        {
            SplashBorder = 0;
            UnSplashBorder = 0;
            ClimbBorder = 0;
            UnClimbBorder = 5;
            NavigationService.Instance.DiveMode = DiveMode.UnClimb;
        }

        public void Forward()
        {
            if (Trip.ActiveDive < 30)
            {
                Trip.ActiveDive++;
                ActiveDive++;
                UpdateBackForwardVisibility();
                RaisePropertyChangedEvent("Trip");
            }
            else
            {
                ForwardVisibility = Visibility.Hidden;
            }
        }
        public void Back()
        {
            Trip.ActiveDive--;
            ActiveDive--;
            UpdateBackForwardVisibility();
            RaisePropertyChangedEvent("Trip");
        }

        public void UpdateBackForwardVisibility()
        {
            if (ActiveDive <= 1)
            {
                BackVisibility = Visibility.Hidden;
            }
            else
            {
                BackVisibility = Visibility.Visible;
            }
        }
        public void NewTrip()
        {
            NavigationService.Instance.MainFrame.Navigate(new TripDatePage());
        }
        public void OpenTrip()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = SettingsService.TripsDirectory;
            openFileDialog.Filter = "Trip Files (*.json)|*.json";
            if (openFileDialog.ShowDialog() == true)
            {
                DataService.Instance.Load(openFileDialog.FileName);
            }
            Trip = DataService.Instance.Trip;
        }

        public void NavigateMainPage()
        {
            NavigationService.Instance.MainFrame.Navigate(new MainPage());
        }
        public void NavigateConfigurePage()
        {
            NavigationService.Instance.MainFrame.Navigate(new ConfigurePage());
        }
        public void Quit()
        {
            Application.Current.Shutdown();
        }

        public void Archive()
        {
            var drive = GetAllRemovableDrives().First();

            string srcPath = Path.Combine(@"C:\DiveBoard\Trips");
            string destPath = Path.Combine(drive.RootDirectory.FullName, @"DiveBoard\Trips");

            if (!drive.IsReady)
            {
                MessageBox.Show("USB Drive is not ready.");
            }
            else
            {
                if(!Directory.Exists(destPath))
                {
                    Directory.CreateDirectory(destPath);
                }
                string [] tripFilePaths = Directory.GetFiles(srcPath, "trip*.json");
                foreach (var tripFilePath in tripFilePaths)
                {
                    string fileName = new FileInfo(tripFilePath).Name;
                    string destFilePath = Path.Combine(destPath, fileName);
                    File.Copy(tripFilePath, destFilePath, true);
                }
                MessageBox.Show("Trip Files Archived to Thumb Drive");
            }

        }
        public void Ok()
        {
            bool proceed = false;
            if(DataService.TripExists(NewTripDate))
            {
                MessageBoxResult messageBoxResult = MessageBox.Show("Trip Date already exists. Overwrite?", "Diver Board", MessageBoxButton.YesNo);
                if (messageBoxResult == MessageBoxResult.Yes)
                {
                    proceed = true;
                }
            }
            else
            {
                proceed = true;
            }
            if (proceed)
            {
                DataService.Instance.Create(NewTripDate);
                DataService.Instance.Load();
                Trip = DataService.Instance.Trip;
                NavigationService.Instance.MainFrame.Navigate(new ConfigurePage());
            }
        }
        public void Cancel()
        {
            NewTripDate = null;
            NavigationService.Instance.MainFrame.Navigate(new ConfigurePage());
        }
        public int ActiveDive
        {
            get
            {
                return _activeDive;
            }

            set
            {
                _activeDive = value;
                RaisePropertyChangedEvent("ActiveDive");
            }
        }

        public static IEnumerable<DriveInfo> GetAllRemovableDrives()
        {
            var driveInfos = DriveInfo.GetDrives().AsEnumerable();
            driveInfos = driveInfos.Where(drive => drive.DriveType == DriveType.Removable);
            return driveInfos;
        }

        public void ProcessCommand(string command)
        {
            switch(command.ToUpper())
            {
                case "COMMAND-SPLASH":
                    Splash();
                    break;
                case "COMMAND-UNSPLASH":
                    UnSplash();
                    break;
                case "COMMAND-CLIMB":
                    Climb();
                    break;
                case "COMMAND-UNCLIMB":
                    UnClimb();
                    break;
                default:
                    if (Trip.Bunks.ContainsKey(command))
                    {
                        ProcessButtonClick(Trip.Bunks[command], true);
                    }
                    else
                    {
                        SystemSounds.Exclamation.Play();

                    }
                    break;
            }
        }

        public void ProcessButtonClick(Bunk bunk, bool scan)
        {

            int activeDive = DataService.Instance.Trip.ActiveDive;
            Dive dive = bunk.Dives[activeDive];
            dive = bunk.Dives[activeDive];


            if (DiverBoard.Services.NavigationService.Instance.DiveMode == DiveMode.Splash)
            {
                if (dive.TimeIn == null)
                {
                    dive.TimeIn = DateTime.Now;
                    dive.TimeOut = null;
                    dive.MaxDepth = null;
                    dive.BottomTime = null;
                }
                else
                {
                    Beep();
                }
            }
            else if (DiverBoard.Services.NavigationService.Instance.DiveMode == DiveMode.Unsplash)
            {
                dive.TimeIn = null;
                dive.TimeOut = null;
                dive.MaxDepth = null;
                dive.BottomTime = null;

            }
            else if (DiverBoard.Services.NavigationService.Instance.DiveMode == DiveMode.Climb)
            {
                if (dive.TimeIn != null)
                {
                    if (dive.TimeOut == null)
                    {
                        dive.TimeOut = DateTime.Now;
                        dive.MaxDepth = null;
                        dive.BottomTime = null;
                    }
                    else
                    {
                        if (!scan)
                        {
                            DiverDetail diverDetail = new DiverDetail(bunk, dive);
                            diverDetail.ShowDialog();
                        }
                    }
                }
                else
                {
                    Beep();
                }
            }
            else
            {
                if (dive.TimeIn != null)
                {
                    dive.TimeOut = null;
                    dive.MaxDepth = null;
                    dive.BottomTime = null;
                }
                else
                {
                    Beep();
                }
            }

        }
        public void Beep()
        {
            SystemSounds.Exclamation.Play();
        }

    }
}