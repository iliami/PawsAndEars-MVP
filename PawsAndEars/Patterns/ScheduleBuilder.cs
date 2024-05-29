using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PawsAndEars.EF.Entities;

namespace PawsAndEars.Patterns
{
    public class ScheduleBuilder
    {
        private Dog dog;
        private int mealsPerDay = 2;
        private int walkingMinutesPerDay = 60;
        private DateTime startWorkingTime = DateTime.Today.AddHours(8);
        private DateTime endWorkingTime = DateTime.Today.AddHours(18);

        public ScheduleBuilder WithMealsPerDay(int mealsPerDay)
        {
            this.mealsPerDay = mealsPerDay;
            return this;
        }

        public ScheduleBuilder WithWalkingMinutesPerDay(int walkingMinutesPerDay)
        {
            this.walkingMinutesPerDay = walkingMinutesPerDay;
            return this;
        }

        public ScheduleBuilder WithWorkingTime(DateTime start, DateTime end)
        {
            startWorkingTime = start;
            endWorkingTime = end;
            return this;
        }

        public IEnumerable<ScheduleTimeInterval> Build(Dog dog)
        {
            this.dog = dog;
            List<ScheduleTimeInterval> scheduleTimeIntervals = new List<ScheduleTimeInterval>();

            var startWorkTime = startWorkingTime.Hour * 60 + startWorkingTime.Minute;
            var endWorkTime = endWorkingTime.Hour * 60 + endWorkingTime.Minute;

            DateTime startTime;
            int morningTrainingMins = 0;
            DateTime endTime = DateTime.Today.AddHours(23);
            if (startWorkTime <= 8 * 60)
            {
                startTime = DateTime.Today.AddHours(startWorkTime / 60 - 1).AddMinutes(startWorkTime % 60);
            }
            else if (8 * 60 < startWorkTime && startWorkTime <= 11 * 60)
            {
                startTime = DateTime.Today.AddHours(startWorkTime / 60 - 2).AddMinutes(startWorkTime % 60);
                morningTrainingMins = 60;
            }
            else if (11 * 60 < startWorkTime && startWorkTime <= 14 * 60)
            {
                startTime = DateTime.Today.AddHours(startWorkTime / 60 - 5).AddMinutes(startWorkTime % 60);
                morningTrainingMins = 120;
            }
            else
            {
                startTime = DateTime.Today.AddHours(startWorkTime / 60 - 8).AddMinutes(startWorkTime % 60);
                morningTrainingMins = 240;
            }

            var meals = mealsPerDay;
            var walkingMinutes = walkingMinutesPerDay;

            var timeDiff = (endTime - startTime - (DateTime.Today.AddMinutes(endWorkTime) - DateTime.Today.AddMinutes(startWorkTime))).TotalMinutes;

            var intervalBetweenMeals = (int)((timeDiff - meals * 15) / meals);


            for (int i = 0; i < meals; i++)
            {
                var sti = ScheduleTimeIntervalFactory.CreateTimeIntervalWithPurchasedFood(200, 300, 50);

                sti.StartTime = startTime;
                sti.EndTime = startTime.AddMinutes(15);
                sti.Dog = dog;
                sti.DogId = dog.Id;

                scheduleTimeIntervals.Add(sti);

                if (startWorkingTime <= startTime.AddMinutes(intervalBetweenMeals) && startTime.AddMinutes(intervalBetweenMeals) <= endWorkingTime)
                {
                    startTime = endWorkingTime;
                }
                else
                {
                    startTime = startTime.AddMinutes(intervalBetweenMeals);
                }
            }

            int morningTrainingMinsForDog = Math.Min(morningTrainingMins, walkingMinutes);
            var training = new Training { Id = Guid.NewGuid().ToString() };
            startTime = scheduleTimeIntervals[0].EndTime.AddMinutes(15);
            if (morningTrainingMins > 0)
            {
                var sti = ScheduleTimeIntervalFactory.CreateTimeIntervalWithWalking();

                sti.StartTime = startTime;
                sti.EndTime = startTime.AddMinutes(morningTrainingMins);
                sti.Dog = dog;
                sti.DogId = dog.Id;

                scheduleTimeIntervals.Add(sti);

                walkingMinutes -= morningTrainingMins;
            }
            if (walkingMinutes > 0)
            {
                var sti = ScheduleTimeIntervalFactory.CreateTimeIntervalWithWalking();

                sti.StartTime = scheduleTimeIntervals.First(f => f.ActivityType == "Food" && f.StartTime >= endWorkingTime).EndTime.AddMinutes(15);
                sti.EndTime = scheduleTimeIntervals.First(f => f.ActivityType == "Food" && f.StartTime >= endWorkingTime).EndTime.AddMinutes(15 + walkingMinutes);
                sti.Dog = dog;
                sti.DogId = dog.Id;

                scheduleTimeIntervals.Add(sti);
            }

            return scheduleTimeIntervals;
        }
    }
}