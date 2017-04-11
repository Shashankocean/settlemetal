using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;
using Newtonsoft.Json;

namespace settleMetal
{
    public partial class Home : ContentPage
    {
        Get_Set_Category gsc;
        public Home()
        {
            InitializeComponent();
            Title = "Home";                  
            get_cateory();
        }

        private void InitializeComponent()
        {
           
        }

        public async void get_cateory()
        {
            request request_category = new request();
            gsc = new Get_Set_Category() { categories = await request_category.getCategory("getCategory.php") };
            if (gsc.categories != null)
            {
                
                //await DisplayAlert("Message","Total Categories: "+ gsc.categories.Count.ToString(), "ok");
            }
            else
            {
                await DisplayAlert("Message","We do not have anything for you!", "ok");
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
            int minrow =0;
            if(gsc.categories.Count>=3)
            {
                minrow = gsc.categories.Count/3;
            }
            else
            {
                minrow = 1;
            }
            Grid grd = new Grid() { RowSpacing = 0, ColumnSpacing = 0, Padding = 0, };
            grd.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Star });
            grd.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Star });
            grd.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Star });
            for (int i=0;i < minrow; i++)
            {
                grd.RowDefinitions.Add(new RowDefinition { Height = 130 });

                for (int j=0;j<3;j++)    
                {
                    if (gsc.categories.Count > count)
                    {
                        Categories c = gsc.categories[count];
                        StackLayout stk_child = new StackLayout
                        {
                            HorizontalOptions = LayoutOptions.StartAndExpand,
                            Orientation = StackOrientation.Vertical,
                            VerticalOptions = LayoutOptions.StartAndExpand,
                            Margin = new Thickness(5, 0, 0, 5),
                            BackgroundColor = Color.FromHex("#cecece"),
                        };
                        var webImage = new Image {
                            Aspect = Aspect.AspectFit,
                            WidthRequest = 100,
                            ClassId = JsonConvert.SerializeObject(c),
                        };
                        webImage.Source = new UriImageSource
                        {

                            Uri = new Uri("http://settlemetal.com/SM_DEV/assets/img/category/" + c.image),
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
            img.BackgroundColor=Color.FromHex("#ff5230");
            ShareInfo.category = JsonConvert.DeserializeObject<Categories>(img.ClassId);
            Navigation.PushAsync(new Devices());
            img.BackgroundColor = Color.FromHex("#cecece");
        }
    }
}
