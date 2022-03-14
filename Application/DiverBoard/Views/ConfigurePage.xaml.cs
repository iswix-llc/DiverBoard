using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Management;
using System.Threading;
using System.Windows;
using System.Windows.Controls;

namespace DiverBoard.Views
{
    /// <summary>
    /// Interaction logic for ConfigurePage.xaml
    /// </summary>
    public partial class ConfigurePage : Page
    {
        private readonly BackgroundWorker _worker = new BackgroundWorker();
        public ConfigurePage()
        {
            InitializeComponent();
            _worker.DoWork += worker_DoWork;
            _worker.RunWorkerCompleted += worker_RunWorkerCompleted;
            _worker.RunWorkerAsync();
        }
        private void worker_DoWork(object sender, DoWorkEventArgs e)
        {
            WqlEventQuery insertQuery = new WqlEventQuery("SELECT * FROM __InstanceCreationEvent WITHIN 2 WHERE TargetInstance ISA 'Win32_USBHub'");

            ManagementEventWatcher insertWatcher = new ManagementEventWatcher(insertQuery);
            insertWatcher.EventArrived += new EventArrivedEventHandler(DeviceInsertedEvent);
            insertWatcher.Start();

            WqlEventQuery removeQuery = new WqlEventQuery("SELECT * FROM __InstanceDeletionEvent WITHIN 2 WHERE TargetInstance ISA 'Win32_USBHub'");
            ManagementEventWatcher removeWatcher = new ManagementEventWatcher(removeQuery);
            removeWatcher.EventArrived += new EventArrivedEventHandler(DeviceRemovedEvent);
            removeWatcher.Start();

            while (_worker.CancellationPending == false)
            {
                if(GetAllRemovableDrives().Count()>0)
                {
                    tripViewModel.ThumbDriveInserted = Visibility.Visible;
                }
                else
                {

                }
                Thread.Sleep(1000);
            }
        }

        private void DeviceInsertedEvent(object sender, EventArrivedEventArgs e)
        {
            tripViewModel.ThumbDriveInserted = Visibility.Visible;
        }

        private void DeviceRemovedEvent(object sender, EventArrivedEventArgs e)
        {
            tripViewModel.ThumbDriveInserted = Visibility.Hidden;
        }

        private void worker_RunWorkerCompleted(object sender,
                                                   RunWorkerCompletedEventArgs e)
        {
        }

        public static IEnumerable<DriveInfo> GetAllRemovableDrives()
        {
            var driveInfos = DriveInfo.GetDrives().AsEnumerable();
            driveInfos = driveInfos.Where(drive => drive.DriveType == DriveType.Removable);
            return driveInfos;
        }
    }
}
