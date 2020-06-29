using System;
using System.Collections.Generic;
using System.Text;

namespace MangaSoep
{
    public interface IEntry
    {


    }

    public class Status
    {
        public const string
            PlanToRead = "Plan to Read",
            Dropped = "Dropped",
            Reading = "Reading",
            Completed = "Completed",
            OnHold = "On hold";
    }


        class MangaEntry : IEntry
    {
        public string Title { get; set; }
        public int ChaptersRead { get; set; }
        public int VolumesRead { get; set; }
        public int Rating { get; set; }
        public string ReadStatus { get; set; }
        public string Color { get; set; }
        public string Source { get; set; }


        public MangaEntry(string title, int chaptersRead, int volumesRead, string status, int rating)
        {
            Title = title;
            ChaptersRead = chaptersRead;
            VolumesRead = volumesRead;
            ReadStatus = status;
            Rating = rating;
            CheckStats();
        }

        void CheckStats()
        {
            UpdateStatus();
        }

        void UpdateStatus()
        {
            switch (ReadStatus){
                case Status.Completed:
                    Color = "#6AB187";
                    break;
                case Status.Dropped:
                    Color = "#D32D41";
                    break;
                case Status.PlanToRead:
                    Color = "1C4E80";
                    break;
                case Status.Reading:
                    Color = "White";
                    break;
                case Status.OnHold:
                    Color = "#DBAE58";
                    break;
                default:
                    Color = "#DADADA";
                    break;
            }
        }
    }
}
