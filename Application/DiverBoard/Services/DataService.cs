using DiverBoard.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace DiverBoard.Services
{
    public class DataService
    {
        private static readonly Lazy<DataService> lazy =   new Lazy<DataService>(() => new DataService());

        public static DataService Instance { get { return lazy.Value; } }

        public Trip Trip { get; set; }

        private DataService()
        {
            string tripsDirectory = SettingsService.TripsDirectory;
            if (!Directory.Exists(tripsDirectory))
            {
                Directory.CreateDirectory(tripsDirectory);
            }
            Load();
        }

        public static bool TripExists(DateTime? tripDate)
        {
            string fileName = $"trip-{tripDate?.ToString("yyyy-MM-dd")}";
            string filePath = Path.Combine(SettingsService.TripsDirectory, $"{fileName}.json");
            return File.Exists(filePath);
        }
        public void Create(DateTime? tripDate)
        {
            Trip = new Trip();
            Trip.TripDate = tripDate?.ToString("yyyy-MM-dd");
            Trip.Bunks = new Dictionary<string, Bunk>();

            List<Bunk> bunks = new List<Bunk>();
            foreach (var bunkNumber in SettingsService.BunkNumbers)
            {
#if DEBUG
                Bunk bunk = new Bunk(bunkNumber) { DiverName = "Some diver"};
#else
                Bunk bunk = new Bunk(bunkNumber) { DiverName = string.Empty};
#endif
                bunk.Dives = new Dictionary<int, Dive>();
                bunk.Dives.Add(1, new Dive());
                Trip.Bunks.Add(bunkNumber, bunk);

            }
           Trip.ActiveDive = 1;
            Save();
        }

        public  void Load()
        {
            string lastDocument = SettingsService.LastDocument;
            if (!string.IsNullOrEmpty(lastDocument))
            {
                if (File.Exists(lastDocument))
                {
                    Load(lastDocument);
                }
            }
        }
        public void Load(string filePath)
        {
            string json = File.ReadAllText(filePath);
            SettingsService.LastDocument = filePath;
            Trip = JsonSerializer.Deserialize<Trip>(json);
        }
        public void Save()
        {
            JsonSerializerOptions jsonSerializerOptions = new JsonSerializerOptions();
            jsonSerializerOptions.WriteIndented = true;
            string json = JsonSerializer.Serialize(Trip, jsonSerializerOptions);
            string filePath = Path.Combine(SettingsService.TripsDirectory, $"trip-{Trip.TripDate}.json");

            File.WriteAllText(filePath, json);
            SettingsService.LastDocument = filePath;

        }
    }
}
