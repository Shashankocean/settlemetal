using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;

using Xamarin.Forms;

namespace settleMetal
{
    public class CartPage : ContentPage
    {
        public CartPage()
        {
            StackLayout mainStack = new StackLayout {
                HorizontalOptions = LayoutOptions.StartAndExpand,
                Orientation = StackOrientation.Vertical,
                Margin = new Thickness(10,10,10,10)
            };
            ScrollView mainScroll = new ScrollView();
            Content = mainScroll;

            mainScroll.Content = mainStack;

            var modobj = ShareInfo.model;
            Image modelImage = new Image {
                Aspect = Aspect.AspectFit,
                HeightRequest = 50,
            };
            modelImage.Source = new UriImageSource
            {
                Uri = new Uri("http://settlemetal.com/SM_DEV/assets/img/model/" + modobj.image_m),
                CachingEnabled = true,
                CacheValidity = new TimeSpan(1, 0, 0, 0),
            };

            Label model_info = new Label { Text = modobj.model_name,
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                VerticalTextAlignment =TextAlignment.Center
            };

            StackLayout headStk = new StackLayout {
                Orientation = StackOrientation.Horizontal,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                BackgroundColor = Color.Gray,
            };

            headStk.Children.Add(modelImage);
            headStk.Children.Add(model_info);
            mainStack.Children.Add(headStk);//main <- head



            Grid grd = new Grid() { RowSpacing = 0, ColumnSpacing = 0, Padding = 0, };

            grd.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Star });
            grd.VerticalOptions = LayoutOptions.StartAndExpand;
            grd.HorizontalOptions = LayoutOptions.CenterAndExpand;
            int row = 0;
            foreach (var services in SelectedService.choosService)
            {
                StackLayout bodystk = new StackLayout
                {
                    Orientation = StackOrientation.Horizontal,
                    HorizontalOptions = LayoutOptions.FillAndExpand,
                    
                };
                Label service = new Label { Text = services.service_name,
                    VerticalTextAlignment = TextAlignment.Center,
                };
                bodystk.Children.Add(service);
                Label price = new Label {
                    Text = services.price.ToString(),
                    HorizontalOptions = LayoutOptions.EndAndExpand,
                    VerticalTextAlignment = TextAlignment.Center,
                    HorizontalTextAlignment = TextAlignment.End };
                bodystk.Children.Add(price);

                grd.RowDefinitions.Add(new RowDefinition { Height = 40 });
                grd.Children.Add(bodystk,0,row);
                row++;

            }
            total(row,grd);
            mainStack.Children.Add(grd);//main <- dody
            StackLayout footer = new StackLayout() {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                Orientation = StackOrientation.Horizontal,
            };
            Button payBtn = new Button { Text="Pay", HorizontalOptions = LayoutOptions.FillAndExpand };
            footer.Children.Add(payBtn);
            mainStack.Children.Add(footer);

        }
        public void total(int row,Grid grd)
        {
            StackLayout bodystk = new StackLayout
            {
                Orientation = StackOrientation.Horizontal,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                BackgroundColor = Color.Gray,
            };
            Label service = new Label
            {
                Text = "Total",
                VerticalTextAlignment = TextAlignment.Center,
                FontAttributes = FontAttributes.Bold,
            };
            bodystk.Children.Add(service);
            Label price = new Label
            {
                Text = ShareInfo.total_amount.ToString(),
                FontAttributes = FontAttributes.Bold,
                HorizontalOptions = LayoutOptions.EndAndExpand,
                VerticalTextAlignment = TextAlignment.Center,
                HorizontalTextAlignment = TextAlignment.End
            };
            bodystk.Children.Add(price);

            grd.RowDefinitions.Add(new RowDefinition { Height = 40 });
            grd.Children.Add(bodystk, 0, row);
        }
    }
}
