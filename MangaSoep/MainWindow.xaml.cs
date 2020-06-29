using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using AdonisUI.Controls;
using System.Text.RegularExpressions;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace MangaSoep
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : AdonisWindow
    {

        JArray list;

        public MainWindow()
        {
            InitializeComponent();
            onStart();
            JArray JsonArray = new JArray();

        }



        void onStart()
        {
            try
            {
                string file = File.ReadAllText("list.json");

                list = JArray.Parse(file);
                for (int i = 0; i < list.Count(); i++)
                {
                    string source = list[i].ToString();
                    dynamic item = JObject.Parse(source);
                    string title = item.Title;
                    string status = item.ReadStatus;
                    int chapter = item.ChaptersRead;
                    int volumes = item.VolumesRead;
                    int rating = item.Rating;
                    AddToList(status, title, chapter, volumes, 10);
                }
            }
            catch { }
        }

        void AddToList(string status, string title, int chapters, int volumes, int rating)
        {
            if (status == "Reading")
            {
                ReadingBox.Items.Insert(0, new MangaEntry(title, chapters, volumes, status, rating));
            }
            else if (status == "Completed")
            {
                Completedbox.Items.Insert(0, new MangaEntry(title, chapters, volumes, status, rating));
            }
            else if (status == "PlanToRead")
            {
                PlanToRead.Items.Insert(0, new MangaEntry(title, chapters, volumes, status, rating));
            }
            else if (status == "Dropped")
            {
                Dropped.Items.Insert(0, new MangaEntry(title, chapters, volumes, status, rating));
            }
        }

        private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void Button_Click_Add(object sender, RoutedEventArgs e)
        {
            try
            {
                //MangaEntry item = (MangaEntry)(sender as ListBox).DataContext;
                //if (item.Title != )
                AddToList(Status.Text, MangaTitle.Text, int.Parse(currentChapter.Text), int.Parse(currentVolume.Text), int.Parse(Rating.Text));
            }
            catch
            {

            }
  
        }

        private void AdonisWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            JArray JsonArray = new JArray();
            List<ListBox> list = new List<ListBox> { ReadingBox, Completedbox, PlanToRead, Dropped };

            foreach(var box in list)
            {
                foreach (var item in box.Items)
                {
                    string json = JsonConvert.SerializeObject(item);
                    JsonArray.Insert(0, json);
                }
            }
  
            File.WriteAllText("list.json", JsonArray.ToString());
        }

        private void MenuItem_Click_Delete(object sender, RoutedEventArgs e)
        {
            List<ListBox> list = new List<ListBox> { ReadingBox, Completedbox, PlanToRead, Dropped };
            foreach (var box in list)
            {
                if (box.SelectedIndex != -1) {
                    box.Items.Remove(box.SelectedItems[0]);

                }
            }
        }

        void selectImplementation(string title, int chaptersRead, int volumesRead, string status, int rating)
        {
            MangaTitle.Text = title;
            currentChapter.Text = chaptersRead.ToString();
            currentVolume.Text = volumesRead.ToString();
            Status.Text = status;
            Rating.Text = rating.ToString();
        }


        private void selectCompleted(object sender, SelectionChangedEventArgs e)
        {
            if(Completedbox.SelectedIndex != -1)
            {
                var item = ReadingBox.SelectedItem as MangaEntry;
                if (item == null) return;
                selectImplementation(item.Title, item.ChaptersRead, item.VolumesRead, item.ReadStatus, item.Rating);
            }
        }


        private void selectReading(object sender, SelectionChangedEventArgs e)
        {
            if (ReadingBox.SelectedIndex != -1)
            {
                var item = ReadingBox.SelectedItem as MangaEntry;
                if (item == null) return;
                selectImplementation(item.Title, item.ChaptersRead, item.VolumesRead, item.ReadStatus, item.Rating);
            }
        }


        private void selectDropped(object sender, SelectionChangedEventArgs e)
        {
            if (Dropped.SelectedIndex != -1)
            {
                var item = ReadingBox.SelectedItem as MangaEntry;
                if (item == null) return;
                selectImplementation(item.Title, item.ChaptersRead, item.VolumesRead, item.ReadStatus, item.Rating);
            }
        }


        private void selectPlanned(object sender, SelectionChangedEventArgs e)
        {
            if (PlanToRead.SelectedIndex != -1)
            {
                var item = ReadingBox.SelectedItem as MangaEntry;
                if (item == null) return;
                selectImplementation(item.Title, item.ChaptersRead, item.VolumesRead, item.ReadStatus, item.Rating);
            }
        }

        private void Button_Click_Update(object sender, RoutedEventArgs e)
        {
            var item = ReadingBox.SelectedItem as MangaEntry;
            if (item == null) return;

            item.Title = MangaTitle.Text;
            item.Rating = int.Parse(Rating.Text);
            item.ReadStatus = Status.Text;
            item.ChaptersRead = int.Parse(currentChapter.Text);
            item.VolumesRead = int.Parse(currentVolume.Text);
            ReadingBox.Items.Refresh();

        }
    }
}
