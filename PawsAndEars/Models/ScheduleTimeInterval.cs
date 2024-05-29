using System;

namespace PawsAndEars.Models
{
    public class ScheduleTimeInterval
    {
        public string Id { get; set; }
        public string DogName { get; set; }
        public string DogId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string ActivityType { get; set; }
        public string ActivityId { get; set; }
        public string ActivityString { get; set; }
        public string ActivityName { get; set; }
        public string ActivityDescription { get; set; }

        public bool IsFood { get; set; }
        public double Weight { get; set; }
        public double CaloriesPer100g { get; set; }
        public bool IsPurchased { get; set; }
        public decimal Price { get; set; }
    }
}