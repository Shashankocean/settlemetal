using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace settleMetal
{
    class Prices_Services : ContentPage
    {
        GetSet_Price_service priceservice;
        Label total = new Label() { HorizontalTextAlignment=TextAlignment.End, VerticalTextAlignment=TextAlignment.Center};
        List<long> price = new List<long>();
        List<Price_Service> selectedservice = new List<Price_Service>();
        Editor add_info;
        public Prices_Services()
        {
            Title = "Services";
            ShareInfo.total_amount = 0;
            get_service_price();
            
        }
        public async void get_service_price()
        {
            Models ctg = new Models { model_id = ShareInfo.model.model_id };

            var modobj = ShareInfo.model;//model information
            //await DisplayAlert("device", "Model id= " + ShareInfo.model.model_id.ToString(), "Oks");
            var json = JsonConvert.SerializeObject(ctg);
            request request_category = new request();
            priceservice = new GetSet_Price_service() { price_service = JsonConvert.DeserializeObject<List<Price_Service>>(await request_category.requestSettle("mprice.php", json)) };
            if (priceservice.price_service != null)
            {
               // await DisplayAlert("Message", "Total services: " + priceservice.price_service.Count.ToString(), "ok");
            }
            else
            {
                await DisplayAlert("Message", "We do not have anything for you!", "ok");
                return;
            }
            //Main stack
            StackLayout mainstk = new StackLayout()
            {
                HorizontalOptions = LayoutOptions.Start,
                Orientation = StackOrientation.Vertical,
                Margin = new Thickness(5, 5, 0, 0),

            };

            //----child top row stack---
            StackLayout toprow = new StackLayout()
            {
                HorizontalOptions = LayoutOptions.Start,
                VerticalOptions = LayoutOptions.Center,
                Orientation = StackOrientation.Horizontal,
            };
            Grid grdtop = new Grid() { RowSpacing = 0, ColumnSpacing = 0, Padding = 0, };
            grdtop.RowDefinitions.Add(new RowDefinition { Height = 70 });
            grdtop.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(2,GridUnitType.Star) });
            grdtop.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(2, GridUnitType.Star) });
            grdtop.VerticalOptions = LayoutOptions.CenterAndExpand;
            grdtop.HorizontalOptions = LayoutOptions.CenterAndExpand;
       
            var webImageModel = new Image
            {
                Aspect = Aspect.AspectFit,
                HeightRequest = 50,
                
            };
            webImageModel.Source = new UriImageSource
            {
                Uri = new Uri("http://settlemetal.com/SM_DEV/assets/img/model/" + modobj.image_m ),
                CachingEnabled = true,
                CacheValidity = new TimeSpan(1, 0, 0, 0),
            };
            Label maodelinfo = new Label {
                Text = "  "+ modobj.model_name,
                FontSize =14,
                HorizontalTextAlignment =TextAlignment.Center,
                VerticalTextAlignment =TextAlignment.Center
            };

            grdtop.Children.Add(webImageModel, 0, 0);
            grdtop.Children.Add(maodelinfo, 1, 0);

            toprow.Children.Add(grdtop);

            //---end of top row---
            mainstk.Children.Add(toprow);

            StackLayout stk = new StackLayout()
            {
                HorizontalOptions = LayoutOptions.Start,
                Orientation = StackOrientation.Vertical,
                BackgroundColor = Color.FromHex("#f2f0ef"),
                Margin = new Thickness(0, 0, 5, 0),

            };
            TapGestureRecognizer tapG = new TapGestureRecognizer();
            tapG.Tapped += TapG_Tapped;

            int count = 0;
            int minrow = 0;
            if (priceservice.price_service.Count >= 3)
            {
                minrow = priceservice.price_service.Count / 3;
            }
            else
            {
                minrow = 1;
            }

            Grid grd = new Grid() { RowSpacing = 0, ColumnSpacing = 0, Padding = 0, };

            grd.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Star });
            grd.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Star });
            grd.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Star });
            grd.VerticalOptions = LayoutOptions.CenterAndExpand;
            grd.HorizontalOptions = LayoutOptions.CenterAndExpand;
            for (int i = 0; i <= minrow; i++)
            {
                grd.RowDefinitions.Add(new RowDefinition { Height = 140 });
                for (int j = 0; j < 3; j++)
                {
                    if (priceservice.price_service.Count > count)
                    {
                        Price_Service c = priceservice.price_service[count];
                        
                        StackLayout stk_child = new StackLayout
                        {
                            HorizontalOptions = LayoutOptions.StartAndExpand,
                            VerticalOptions = LayoutOptions.StartAndExpand,
                            Orientation = StackOrientation.Vertical,
                            Margin = new Thickness(5, 0, 0, 5),
                            BackgroundColor = Color.FromHex("#cecece"),

                        };
                        Label lable = new Label { Text = c.service_name, BackgroundColor =Color.White };
                        var webImage = new Image
                        {
                            Aspect = Aspect.AspectFit,
                            HeightRequest = 100,
                            StyleId = c.service_id.ToString(),
                            ClassId= JsonConvert.SerializeObject(c),
                    };
                        webImage.Source = new UriImageSource
                        {
                            Uri = new Uri("http://settlemetal.com/SM_DEV/assets/img/services/" + c.image_s),
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
            stk.Children.Add(new Label { Text = "Additional information", TextColor = Color.FromHex("#ff5230") });
            add_info = new Editor() { HeightRequest = 100, BackgroundColor = Color.White };
            stk.Children.Add(add_info);
            var scrl = new ScrollView();
            scrl.Content = stk;

            mainstk.Children.Add(scrl);
            //--footer--
            StackLayout footer = new StackLayout {
                HorizontalOptions = LayoutOptions.StartAndExpand,
                Orientation = StackOrientation.Horizontal,
            };
            Grid grdbtm = new Grid() { RowSpacing = 0, ColumnSpacing = 0, Padding = 0, };
            grdbtm.RowDefinitions.Add(new RowDefinition { Height = 55 });
            grdbtm.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(2, GridUnitType.Star) });
            grdbtm.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(2, GridUnitType.Star) });
            grdbtm.VerticalOptions = LayoutOptions.Center;
            grdbtm.HorizontalOptions = LayoutOptions.End;
            Button next = new Button { Text="Add to Cart"};
            next.Clicked += Next_Clicked;
            total.Text = "0";
            grdbtm.Children.Add(total, 0, 0);
            grdbtm.Children.Add(next, 1, 0);
            footer.Children.Add(grdbtm);
            //--end footer
            mainstk.Children.Add(footer);

            Content = mainstk;
        }

        private void Next_Clicked(object sender, EventArgs e)
        {
            SelectedService.choosService = selectedservice;
            if(!String.IsNullOrEmpty(add_info.Text))
                ShareInfo.addtional_info = add_info.Text;
            Navigation.PushAsync(new AskAddressPage());
        }

        private void TapG_Tapped(object sender, EventArgs e)
        {
            var img = (Image)sender;
            string price_serv = img.ClassId;
            var obj = JsonConvert.DeserializeObject<Price_Service>(price_serv);
            long id = obj.price_id;
            float srv_price = obj.price;

            if (price.Contains(id))
            {
                price.Remove(id);
                ShareInfo.total_amount -= srv_price;
                total.Text = ShareInfo.total_amount.ToString();
                img.BackgroundColor = Color.FromHex("#cecece");
               // Price_Service rvobj = selectedservice.Find(rv => rv.price_id == obj.price_id);
                selectedservice.RemoveAll(rv => rv.price_id == obj.price_id );
            }
            else
            {
                price.Add(id);
                ShareInfo.total_amount += srv_price;
                total.Text = ShareInfo.total_amount.ToString();
                img.BackgroundColor = Color.FromHex("#ff5230");
                selectedservice.Add(obj);
            }
        }
    }
    
}
