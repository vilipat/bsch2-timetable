using Avalonia;
using Avalonia.Controls;
using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using Timetable.Models.Validators;
using Timetable.Shared;

namespace Timetable.Models
{

    public partial class ActivitySlot : BaseModel
    {
        public TimeSpan StartTime
        {
            get => startTime;
            set => SetProperty(ref startTime, value, true);
        }
        private TimeSpan startTime;
        public string StartTimeFormat => StartTime.ToString(@"hh\:mm");


        public TimeSpan EndTime
        {
            get => endTime;
            set => SetProperty(ref endTime, value, true);
        }
        private TimeSpan endTime;
        public string EndTimeFormat => EndTime.ToString(@"hh\:mm");


        public DayOfWeek DayOfWeek
        {
            get => dayOfWeek;
            set => SetProperty(ref dayOfWeek, value, true);
        }
        private DayOfWeek dayOfWeek;

        public string? DayOfWeekLoc => DayOfWeeksLoc?.FirstOrDefault(x => x.Key == DayOfWeek).Value;
        public static ObservableCollection<KeyValuePair<DayOfWeek, string>>? DayOfWeeksLoc
        {
            get
            {
                Application app = Application.Current!;

                return new ObservableCollection<KeyValuePair<DayOfWeek, string>>()
                {
                    new KeyValuePair<DayOfWeek, string>(
                        DayOfWeek.Monday,
                        ((string?)app.FindResource("Lang.Days.Monday")) ?? "Monday"),

                    new KeyValuePair<DayOfWeek, string>(
                        DayOfWeek.Tuesday,
                        ((string?)app.FindResource("Lang.Days.Tuesday")) ?? "Tuesday"),

                    new KeyValuePair<DayOfWeek, string>(
                        DayOfWeek.Wednesday,
                        ((string?)app.FindResource("Lang.Days.Wednesday")) ?? "Wednesday"),

                    new KeyValuePair<DayOfWeek, string>(
                        DayOfWeek.Thursday,
                        ((string?)app.FindResource("Lang.Days.Thursday")) ?? "Thursday"),

                    new KeyValuePair<DayOfWeek, string>(
                        DayOfWeek.Friday,
                        ((string?)app.FindResource("Lang.Days.Friday")) ?? "Friday"),

                    new KeyValuePair<DayOfWeek, string>(
                        DayOfWeek.Saturday,
                        ((string?)app.FindResource("Lang.Days.Saturday")) ?? "Saturday"),

                    new KeyValuePair<DayOfWeek, string>(
                        DayOfWeek.Sunday,
                        ((string?)app.FindResource("Lang.Days.Sunday")) ?? "Sunday"),
                };
            }
        }


        private WeekPeriod regularity;
        public WeekPeriod Regularity
        {
            get => regularity;
            set => SetProperty(ref regularity, value, true);
        }

        public string? WeekPeriodLoc => WeekPeriodsLoc?.FirstOrDefault(x => x.Key == Regularity).Value;
        public static ObservableCollection<KeyValuePair<WeekPeriod, string>>? WeekPeriodsLoc
        {
            get
            {
                Application app = Application.Current!;

                return new ObservableCollection<KeyValuePair<WeekPeriod, string>>()
                {
                    new KeyValuePair<WeekPeriod, string>(
                        WeekPeriod.ALL,
                        ((string?)app.FindResource("Lang.Regularity.Every")) ?? "Every"),

                    new KeyValuePair<WeekPeriod, string>(
                        WeekPeriod.ODD,
                        ((string?)app.FindResource("Lang.Regularity.Odd")) ?? "Odd"),

                    new KeyValuePair<WeekPeriod, string>(
                        WeekPeriod.EVEN,
                        ((string?)app.FindResource("Lang.Regularity.Even")) ?? "Even")
                };
            }
        }

        [ObservableProperty]
        private string activityTitle = string.Empty;

        [ObservableProperty]
        [Required]
        public Activity? activity;
        public int ActivityId { get; set; }

        public ObservableCollection<Person> Persons { get; set; } = new();


        private bool ValidateSlotOverlap()
        {
            var slots = new List<ActivitySlot>(ActivitySlots);
            slots.Remove(this);

            foreach (ActivitySlot slot in slots)
            {
                if (!OverlapActivitySlotValidator.AreSlotsValid(this, slot))
                    return false;
            }
            return true;
        }

        public List<ActivitySlot> ActivitySlots { get; set; } = new();
    }
}
