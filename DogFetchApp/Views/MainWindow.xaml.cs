using ApiHelper;
using ApiHelper.Models;
using DogFetchApp.Commands;
using DogFetchApp.ViewModels;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Navigation;
using System.Diagnostics;
using System.Windows.Media.Imaging;
using System;

namespace DogFetchApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        MainViewModel currentViewmodel;
        
        App app;
        public DogApiModel dogPictures { get; set; }
        int imgIndex;


        public MainWindow(App app)
        {
            InitializeComponent();
            currentViewmodel = new MainViewModel();
            ApiHelper.ApiHelper.InitializeClient();
            DataContext = currentViewmodel;

            this.app = app;

            currentViewmodel.RestartCommand = new DelegateCommand<string>(Restart);
        }
        private async Task LoadBreedList()
        {
            var Breeds = await DogApiProcessor.LoadBreedList();
            var _breeds = Breeds.breeds;
            foreach (var breed in _breeds)
            {
                if (breed.Value.Count == 0)
                {
                    ComboBox.Items.Add(breed.Key);
                }
                else
                {
                    for (int i = 0; i < breed.Value.Count; i++)
                    {
                        ComboBox.Items.Add(breed.Key + "/" + breed.Value[i]);
                    }
                }
            }
        }

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ComboBox.Items.Add("ALL");
            await LoadBreedList();
            previousImageButton.IsEnabled = false;
            nextImageButton.IsEnabled = false;
        }

        private async void Fetching(object sender, RoutedEventArgs e)
        {
            imgIndex = 0;
            string nbrOfPictures = NbContent.Text;
            string breed = ComboBox.Text;
            if (nbrOfPictures != "" && breed != "")
            {
                dogPictures = await DogApiProcessor.GetImageUrl(breed, nbrOfPictures);
                var r = dogPictures.Pictures[imgIndex];
                TheImage.Source = new BitmapImage(new Uri(r));
                if (nbrOfPictures != "1")
                {
                    nextImageButton.IsEnabled = true;
                }
                if (nbrOfPictures == "1")
                {
                    nextImageButton.IsEnabled = false;
                    previousImageButton.IsEnabled = false;
                }

            }
        }

        private void Previous_Click(object sender, RoutedEventArgs e)
        {
            imgIndex--;
            var r = dogPictures.Pictures[imgIndex];
            TheImage.Source = new BitmapImage(new Uri(r));
            nextImageButton.IsEnabled = true;
            if (imgIndex == 0)
            {
                previousImageButton.IsEnabled = false;
            }
        }

        private void Next_Click(object sender, RoutedEventArgs e)
        {
            imgIndex++;
            var r = dogPictures.Pictures[imgIndex];
            TheImage.Source = new BitmapImage(new Uri(r));
            previousImageButton.IsEnabled = true;
            if (imgIndex == dogPictures.Pictures.Count - 1)
            {
                nextImageButton.IsEnabled = false;
            }
        }

        private void Restart(string obj)
        {
            var result = MessageBox.Show(Properties.Resources.MessageBox, "Message", MessageBoxButton.OKCancel);
            if (result == MessageBoxResult.OK)
            {
                app.Restart();
            }
        }


    }
}
