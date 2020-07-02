using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace MangaSoep
{
    public interface IEntry
    {

    }

    public class MangaStatus : Enumeration
    {
        public static MangaStatus Completed = new MangaStatus(0, "Completed", "#6AB187");
        public static MangaStatus Dropped = new MangaStatus(1, "Dropped", "#D32D41");
        public static MangaStatus Reading = new MangaStatus(2, "Reading", "White"  );
        public static MangaStatus PlanToRead = new MangaStatus(3, "Plan to read", "#1C4E80");
        public static MangaStatus OnHold = new MangaStatus(4, "On hold", "Yellow");

        public string Color { get; }

        public MangaStatus() { }

        public MangaStatus(int id, string name, string color) : base(id, name)
        {
            Color = color;
        }

    }

    class MangaEntry : INotifyPropertyChanged, IEntry
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public string Title { get; set; }
        public int ChaptersRead { get; set; }
        public int VolumesRead { get; set; }
        public int Rating { get; set; }
        public string ReadStatus { get; set; }
        public string Color { get; set; }
        public string Source { get; set; } = "question mark.png";


        public MangaEntry(string title, int chaptersRead, int volumesRead, string status, string color, int rating, string source)
        {
            Title = title;
            ChaptersRead = chaptersRead;
            VolumesRead = volumesRead;
            ReadStatus = status;
            Rating = rating;
            Color = color;
            Source = source;
        }
    }

}
