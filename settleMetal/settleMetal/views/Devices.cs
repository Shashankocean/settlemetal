using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace settleMetal
{
    class Devices : ContentPage
    {
        Get_set_Devices_db devices;
        public Devices()
        {
            Title = "Devices";
            get_devices();
        }
        public async void get_devices()
        {
            Categories ctg = new Categories { category_id = ShareInfo.category.category_id };
            //await DisplayAlert("Cateory", "Category id= " + ShareInfo.category_id.ToString(), "Oks");
            var json = JsonConvert.SerializeObject(ctg);
            request request_category = new request();
            devices = new Get_set_Devices_db() { devices_db = JsonConvert.DeserializeObject<List<Devices_db>>(await request_category.requestSettle("mdevices.php", json))};
            if (devices.devices_db != null)
            {
                //await DisplayAlert("Message", "Total Devices: " + devices.devices_db.Count.ToString(), "ok");
            }
            else
            {
                await DisplayAlert("Message", "We do not have anything for you!", "ok");
                return;
            }
            StackLayout stk = new StackLayout()
            {
                HorizontalOptions = LayoutOptions.Start,
                Orientation = StackOrientation.Vertical,
                BackgroundColor = Color.FromHex("#f2f0ef"),
                Margin = new Thickness(0, 5, 5, 0),
            };
            TapGestureRecognizer tapG = new TapGestureRecognizer();
            tapG.Tapped += TapG_Tapped;

            int count = 0;
            int minrow = 0;
            if (devices.devices_db.Count >= 3)
            {
                minrow = devices.devices_db.Count / 3;
            }
            else
            {
                minrow = 1;
            }
            Grid grd = new Grid() { RowSpacing = 0, ColumnSpacing = 0, Padding = 0, };

            grd.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Star });
            grd.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Star });
            grd.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Star });

            for (int i = 0; i < minrow; i++)
            {

                grd.RowDefinitions.Add(new RowDefinition { Height = 130 });
                for (int j = 0; j < 3; j++)
                {
                    if (devices.devices_db.Count > count)
                    {
                        Devices_db c = devices.devices_db[count];
                        StackLayout stk_child = new StackLayout
                        { 
                            HorizontalOptions = LayoutOptions.CenterAndExpand,
                            VerticalOptions = LayoutOptions.Center,
                            Orientation = StackOrientation.Vertical,
                            Margin = new Thickness(5, 0, 0, 5),
                            BackgroundColor = Color.FromHex("#cecece"),
                        };
                        var webImage = new Image
                        {
                            Aspect = Aspect.AspectFit,
                            WidthRequest = 100,
                            ClassId = JsonConvert.SerializeObject(c),
                        };
                        webImage.Source = new UriImageSource
                        {

                            Uri = new Uri("http://settlemetal.com/SM_DEV/assets/img/devices/" + c.image),
                            CachingEnabled = true,
                            CacheValidity = new TimeSpan(1, 0, 0, 0),
                        };
                        webImage.GestureRecognizers.Add(tapG);
                        stk_child.Children.Add(webImage);
                        grd.Children.Add(stk_child, j, i);

                    }
                    count++;
                }
                stk.Children.Add(grd);

            }

            var scrl = new ScrollView();
            scrl.Content = stk;
            Content = scrl;
        }

        private void TapG_Tapped(object sender, EventArgs e)
        {
            var img = (Image)sender;
           
            img.BackgroundColor = Color.FromHex("#ff5230");
            ShareInfo.device = JsonConvert.DeserializeObject<Devices_db>(img.ClassId);
            Navigation.PushAsync(new ModelsPage());
            img.BackgroundColor = Color.FromHex("#cecece");
        }
    }
}
