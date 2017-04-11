using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;

using Xamarin.Forms;

namespace settleMetal
{
    public class ModelsPage : ContentPage
    {
        Get_set_Models models;
        public ModelsPage()
        {
            Title = "Models";
            get_devices();
        }
        public async void get_devices()
        {
            Devices_db ctg = new Devices_db { device_id = ShareInfo.device.device_id };
           // await DisplayAlert("device", "device id= " + ShareInfo.device_id.ToString(), "Oks");
            var json = JsonConvert.SerializeObject(ctg);
            request request_category = new request();
            models = new Get_set_Models() { models = JsonConvert.DeserializeObject<List<Models>>(await request_category.requestSettle("mmodels.php", json)) };
            if (models.models != null)
            {
               // await DisplayAlert("Message", "Total models: " + models.models.Count.ToString(), "ok");
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
                Margin = new Thickness(0,5,5,0),
                BackgroundColor = Color.FromHex("#f2f0ef"),

            };
            TapGestureRecognizer tapG = new TapGestureRecognizer();
            tapG.Tapped += TapG_Tapped;

            int count = 0;
            int minrow = 0;
            if (models.models.Count >= 3)
            {
                minrow = models.models.Count / 3;
            }
            else
            {
                minrow = 1;
            }

            Grid grd = new Grid() { RowSpacing = 0, ColumnSpacing = 0, Padding = 0, };
            
            grd.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Star });
            grd.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Star });
            grd.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Star });
            
            for (int i = 0; i <= minrow; i++)
            {
                grd.RowDefinitions.Add(new RowDefinition { Height = 140 });             
                for (int j = 0; j < 3; j++)
                {
                    if (models.models.Count > count)
                    {
                        Models c = models.models[count];
                        StackLayout stk_child = new StackLayout
                        {
                            HorizontalOptions = LayoutOptions.CenterAndExpand,
                            VerticalOptions = LayoutOptions.Center,
                            Orientation = StackOrientation.Vertical,
                            Margin = new Thickness(5, 0, 0, 5),
                            BackgroundColor = Color.FromHex("#cecece"),
                            
                        };
                        Label lable = new Label { Text= c.model_name };
                        var webImage = new Image
                        {
                            Aspect = Aspect.AspectFit,
                            HeightRequest = 100,
                            StyleId = c.model_id.ToString(),
                            ClassId = JsonConvert.SerializeObject(c),
                        };
                        webImage.Source = new UriImageSource
                        {
                            Uri = new Uri("http://settlemetal.com/SM_DEV/assets/img/model/" + c.image_m),
                            CachingEnabled = true,
                            CacheValidity = new TimeSpan(1, 0, 0, 0),
                        };
                        webImage.GestureRecognizers.Add(tapG);
                        stk_child.Children.Add(webImage);
                        stk_child.Children.Add(lable);

                        grd.Children.Add(stk_child, j, i);
                    }
                    count++;
                }
            }
            stk.Children.Add(grd);
            var scrl = new ScrollView();
            scrl.Content = stk;
            Content = scrl;
        }

        private void TapG_Tapped(object sender, EventArgs e)
        {
            var img = (Image)sender;
            img.BackgroundColor = Color.FromHex("#ff5230");
            ShareInfo.model = JsonConvert.DeserializeObject<Models>(img.ClassId);
            Navigation.PushAsync(new Prices_Services());
            img.BackgroundColor = Color.FromHex("#cecece");
        }
    }
}
