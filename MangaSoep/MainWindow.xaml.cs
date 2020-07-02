using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Win32;
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
            DataContext = new MainWindowViewModel();
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
                    string json = list[i].ToString();
                    MangaEntry manga = Newtonsoft.Json.JsonConvert.DeserializeObject<MangaEntry>(json);
                    AddToList(manga);
                }
            }
            catch {
                System.Windows.MessageBox.Show("Json read failed");
            
            }

        }

        void AddToList(MangaEntry manga)
        {
            string status = manga.ReadStatus;

            if (status == MangaStatus.Reading.Name)
            {
                ReadingBox.Items.Insert(0, manga);
            }
            else if (status == MangaStatus.Completed.Name)
            {
                Completedbox.Items.Insert(0, manga);
            }
            else if (status == MangaStatus.PlanToRead.Name)
            {
                PlanToRead.Items.Insert(0, manga);
            }
            else if (status == MangaStatus.Dropped.Name)
            {
                Dropped.Items.Insert(0, manga);
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
                AddToList(new MangaEntry(
                    MangaTitle.Text, 
                    int.Parse(currentChapter.Text), 
                    int.Parse(currentVolume.Text), 
                    Status.Text, Status.Foreground.ToString(), 
                    int.Parse(Rating.Text), 
                    "question mark.png")
                    );
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

            if (list[header.SelectedIndex].SelectedIndex != -1) {
                list[header.SelectedIndex].Items.Remove(list[header.SelectedIndex].SelectedItem);
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
            if(Completedbox.SelectedIndex != -1 && header.SelectedIndex == 1)
            {
                var item = Completedbox.SelectedItem as MangaEntry;
                if (item == null) return;
                selectImplementation(item.Title, item.ChaptersRead, item.VolumesRead, item.ReadStatus, item.Rating);
            }
        }


        private void selectReading(object sender, SelectionChangedEventArgs e)
        {
            if (ReadingBox.SelectedIndex != -1 && header.SelectedIndex == 0)
            {
                var item = ReadingBox.SelectedItem as MangaEntry;
                if (item == null) return;
                selectImplementation(item.Title, item.ChaptersRead, item.VolumesRead, item.ReadStatus, item.Rating);
            }
        }


        //private void selectHold(object sender, SelectionChangedEventArgs e)
        //{
        //    if (Hold.SelectedIndex != -1)
        //    {
        //        var item = Hold.SelectedItem as MangaEntry;
        //        if (item == null) return;
        //        selectImplementation(item.Title, item.ChaptersRead, item.VolumesRead, item.ReadStatus, item.Rating);
        //    }
        //}


        private void selectDropped(object sender, SelectionChangedEventArgs e)
        {
            if (Dropped.SelectedIndex != -1 && header.SelectedIndex == 3)
            {
                var item = Dropped.SelectedItem as MangaEntry;
                if (item == null) return;
                selectImplementation(item.Title, item.ChaptersRead, item.VolumesRead, item.ReadStatus, item.Rating);
            }
        }


        private void selectPlanned(object sender, SelectionChangedEventArgs e)
        {
            if (PlanToRead.SelectedIndex != -1 && header.SelectedIndex == 2)
            {
                var item = PlanToRead.SelectedItem as MangaEntry;
                if (item == null) return;
                selectImplementation(item.Title, item.ChaptersRead, item.VolumesRead, item.ReadStatus, item.Rating);
            }
        }

        private void Button_Click_Update(object sender, RoutedEventArgs e)
        {
            List<ListBox> list = new List<ListBox> { ReadingBox, Completedbox, PlanToRead, Dropped };

                var item = list[header.SelectedIndex].SelectedItem as MangaEntry;
                if (item == null) return;

                item.Title = MangaTitle.Text;
                item.Rating = int.Parse(Rating.Text);
                item.ReadStatus = Status.Text;
                item.ChaptersRead = int.Parse(currentChapter.Text);
                item.VolumesRead = int.Parse(currentVolume.Text);
                list[header.SelectedIndex].Items.Refresh();
            
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            List<ListBox> list = new List<ListBox> { ReadingBox, Completedbox, PlanToRead, Dropped };
            var item = list[header.SelectedIndex].SelectedItem as MangaEntry;
            if (item == null) return;

            OpenFileDialog op = new OpenFileDialog();
            op.Title = "Select a picture";
            op.Filter = "All supported graphics|*.jpg;*.jpeg;*.png|" +
              "JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|" +
              "Portable Network Graphic (*.png)|*.png";
            if (op.ShowDialog() == true)
            {
                item.Source = op.FileName;
            }
            list[header.SelectedIndex].Items.Refresh();
        }

    }
}
