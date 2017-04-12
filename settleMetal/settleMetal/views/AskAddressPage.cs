using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;

using Xamarin.Forms;

namespace settleMetal
{
    public class AskAddressPage : ContentPage
    {
        //EntryCell address,address2;
        Entry address, address2;
        Switch pdate1, pdate2, ptime1, ptime2, ptime3, check_addr2, pickup;
        StackLayout body, check2_addr;
        int date = 0, time = 0;
        Get_set_city citys;
        Get_Set_Location Locations;
        Get_Set_StoreAddress storeList;
        Picker city_picker, location_picker, store_picker;
        Label address2_lbl, address_lbl, store_lbl, chk_add2, pick_lbl;
        public AskAddressPage()
        {
            Title = "User Info";
            DateTime date = DateTime.Now;
            DateTime nextdate = date.AddDays(1);
            StackLayout main_stk = new StackLayout {
                Orientation = StackOrientation.Vertical,
                HorizontalOptions = LayoutOptions.FillAndExpand
            };
            ScrollView main_scrl = new ScrollView();

            body = new StackLayout()
            {
                Orientation = StackOrientation.Vertical,
                HorizontalOptions = LayoutOptions.FillAndExpand
            };

            StackLayout use_pick = new StackLayout
            {
                Orientation = StackOrientation.Horizontal,
                BackgroundColor = Color.FromHex("#31708f"),
            };
            pick_lbl = new Label { Text = "Come and pick up from me", VerticalTextAlignment = TextAlignment.Center, TextColor = Color.White };
            pickup = new Switch() { IsToggled=true };
            pickup.Toggled += Pickup_Toggled;
            use_pick.Children.Add(pick_lbl);
            use_pick.Children.Add(pickup);

            Label City = new Label { Text = "City",
                BackgroundColor =Color.FromHex("#fb6d69"),
                HeightRequest = 30,
                VerticalTextAlignment = TextAlignment.Center
            };
            city_picker = new Picker
            {
                Title = "Select City",
                VerticalOptions = LayoutOptions.CenterAndExpand,
                Margin = new Thickness(10, 0, 5, 0),
            };
            city_picker.SelectedIndexChanged += City_picker_SelectedIndexChanged;

            address_lbl = new Label { Text = "Get your device picked up from your Address",
                BackgroundColor = Color.FromHex("#fb6d69"),
                HeightRequest = 30,
                VerticalTextAlignment = TextAlignment.Center
            };
            address = new Entry { Placeholder = "Pick-up & Drop-off Address", Margin = new Thickness(10, 0, 5, 0), };
            check2_addr = new StackLayout
            {
                Orientation = StackOrientation.Horizontal,
            };
            chk_add2 = new Label { Text = "Use different drop-off address(optional)" };
            check_addr2 = new Switch();
            check_addr2.Toggled += Check_addr2_Toggled;
            check2_addr.Children.Add(chk_add2);
            check2_addr.Children.Add(check_addr2);

            address2_lbl = new Label { Text = "Get your device drop at your Address", IsVisible = false };
            address2 = new Entry { Placeholder = "Drop-off Address", IsVisible= false, Margin = new Thickness(10, 0, 5, 0), };

            store_lbl = new Label { Text = "Drop-off your device at our store", IsVisible = false,
                BackgroundColor = Color.FromHex("#fb6d69"),
                HeightRequest = 30,
                VerticalTextAlignment = TextAlignment.Center
            };
            location_picker = new Picker
            {
                Title = "select nearest location",
                VerticalOptions = LayoutOptions.CenterAndExpand,
                Margin = new Thickness(10, 0, 5, 0),
                IsVisible = false,
            };
            location_picker.Items.Add("Select city first");
            location_picker.SelectedIndexChanged += Location_picker_SelectedIndexChanged;
            store_picker = new Picker
            {
                Title = "select nearest area",
                VerticalOptions = LayoutOptions.FillAndExpand,
                Margin = new Thickness(10, 0, 5, 0),
                IsVisible = false,
            };
            store_picker.Items.Add("Select location first");

            Label date_lbl = new Label { Text = "Pickup Date",
                BackgroundColor = Color.FromHex("#fb6d69"),
                HeightRequest =30,
                VerticalTextAlignment = TextAlignment.Center };
            StackLayout pick_date1 = new StackLayout
            {
                Orientation = StackOrientation.Horizontal,
                HorizontalOptions = LayoutOptions.FillAndExpand,
            };
            Label date1 = new Label { Text = date.ToString("dd:MM:yyy"), FontAttributes = FontAttributes.Bold,
                HorizontalTextAlignment = TextAlignment.Start,
            };
            pdate1 = new Switch();
            pdate1.Toggled += Pdate1_Toggled;
            pick_date1.Children.Add(date1);
            pick_date1.Children.Add(pdate1);

            StackLayout pick_date2 = new StackLayout
            {
                Orientation = StackOrientation.Horizontal,
            };
            Label date2 = new Label { Text = nextdate.ToString("dd:MM:yyy"), FontAttributes = FontAttributes.Bold };
            pdate2 = new Switch();
            pdate2.Toggled += Pdate2_Toggled;
            pick_date2.Children.Add(date2);
            pick_date2.Children.Add(pdate2);

            Label time_lbl = new Label { Text = "Pickup Time",
                BackgroundColor = Color.FromHex("#fb6d69"),
                HeightRequest = 30,
                VerticalTextAlignment = TextAlignment.Center
            };
            StackLayout pick_time1 = new StackLayout
            {
                Orientation = StackOrientation.Horizontal,
            };
            Label time1 = new Label { Text = "10-11:59 AM", FontAttributes = FontAttributes.Bold };
            ptime1 = new Switch();
            ptime1.Toggled += Ptime1_Toggled;
            pick_time1.Children.Add(time1);
            pick_time1.Children.Add(ptime1);

            StackLayout pick_time2 = new StackLayout
            {
                Orientation = StackOrientation.Horizontal,
            };
            Label time2 = new Label { Text = "12-02:00 PM", FontAttributes = FontAttributes.Bold };
            ptime2 = new Switch();
            ptime2.Toggled += Ptime2_Toggled;
            pick_time2.Children.Add(time2);
            pick_time2.Children.Add(ptime2);

            StackLayout pick_time3 = new StackLayout
            {
                Orientation = StackOrientation.Horizontal,
            };
            Label time3 = new Label { Text = "02-04:00 PM", FontAttributes = FontAttributes.Bold };
            ptime3 = new Switch();
            ptime3.Toggled += Ptime3_Toggled;
            pick_time3.Children.Add(time3);
            pick_time3.Children.Add(ptime3);
            Button PaymentBtn = new Button { Text = "pay" };
            PaymentBtn.Clicked += PaymentBtn_Click;

            body.Children.Add(use_pick);
            body.Children.Add(City);
            body.Children.Add(city_picker);
            body.Children.Add(address_lbl);
            body.Children.Add(address);
            body.Children.Add(check2_addr);
            body.Children.Add(address2_lbl);
            body.Children.Add(address2);
            body.Children.Add(store_lbl);
            body.Children.Add(location_picker);
            body.Children.Add(store_picker);
            body.Children.Add(date_lbl);
            body.Children.Add(pick_date1);
            body.Children.Add(pick_date2);
            body.Children.Add(time_lbl);
            body.Children.Add(pick_time1);
            body.Children.Add(pick_time2);
            body.Children.Add(pick_time3);

            main_scrl.Content = body;
            main_stk.Children.Add(main_scrl);
            main_stk.Children.Add(PaymentBtn);
            Content = main_stk;

            getinfo();
        }

        private void Pickup_Toggled(object sender, ToggledEventArgs e)
        {
            Switch swc = (Switch)sender;
            if(swc.IsToggled)
            {
                address_lbl.IsVisible = true;
                address.IsVisible = true;
                check2_addr.IsVisible = true;
                address2_lbl.IsVisible = false;
                address2.IsVisible = false;
                check_addr2.IsToggled = false;

                store_lbl.IsVisible = false;
                location_picker.IsVisible = false;
                store_picker.IsVisible = false;
            }
            else
            {
                store_lbl.IsVisible = true;
                location_picker.IsVisible = true;
                store_picker.IsVisible = true;

                check_addr2.IsToggled = false;
                address_lbl.IsVisible = false;
                address.IsVisible = false;
                check2_addr.IsVisible = false;
                address2_lbl.IsVisible = false;
                address2.IsVisible = false;
            }
        }

        private void Ptime1_Toggled(object sender, ToggledEventArgs e)
        {
            Switch sc = (Switch)sender;
            if (sc.IsToggled)
                time++;
            else
                time--;
            ptime2.IsToggled = false;
            ptime3.IsToggled = false;
        }

        private void Ptime2_Toggled(object sender, ToggledEventArgs e)
        {
            Switch sc = (Switch)sender;
            if (sc.IsToggled)
                time++;
            else
                time--;
            ptime1.IsToggled = false;
            ptime3.IsToggled = false;
        }

        private void Ptime3_Toggled(object sender, ToggledEventArgs e)
        {
            Switch sc = (Switch)sender;
            if (sc.IsToggled)
                time++;
            else
                time--;
            ptime2.IsToggled = false;
            ptime1.IsToggled = false;
        }

        private void Check_addr2_Toggled(object sender, ToggledEventArgs e)
        {
            Switch sc = (Switch)sender;
            if (sc.IsToggled)
            {
                address2.IsVisible = true;
            }
            else
            {
                address2.IsVisible = false;
            }
        }

        private async void Location_picker_SelectedIndexChanged(object sender, EventArgs e)
        {
            try {
                
                    store_picker.Items.Clear();
                    Picker location_pick = (Picker)sender;
                    Location location = new Location { location_name = location_pick.Items[location_pick.SelectedIndex] };
                    request request_store = new request();
                    string json = JsonConvert.SerializeObject(location);
                    storeList = new Get_Set_StoreAddress() { get_set_address = await request_store.getAddress(json) };
                    if (storeList.get_set_address != null)
                    {
                        // await DisplayAlert("Message", "Total services: " + priceservice.price_service.Count.ToString(), "ok");
                        foreach (StoreAddress address in storeList.get_set_address)
                        {
                            if(String.IsNullOrEmpty(address.address))
                            {

                            }
                            else
                                store_picker.Items.Add(address.address);
                        }

                        return;
                    }
                
            }
            catch(Exception ex)
            {
               
            }
        }

        private async void City_picker_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                location_picker.Items.Clear();
                Picker city_pick = (Picker)sender;
                City city = new City { city_name = city_pick.Items[city_pick.SelectedIndex] };
                request request_city = new request();
                string json = JsonConvert.SerializeObject(city);
                Locations = new Get_Set_Location() { get_set_location = await request_city.getlocation(json) };
                if (Locations.get_set_location != null)
                {
                    // await DisplayAlert("Message", "Total services: " + priceservice.price_service.Count.ToString(), "ok");
                    foreach (Location location in Locations.get_set_location)
                    {
                        location_picker.Items.Add(location.location_name);
                    }

                    return;
                }
            }
            catch(Exception ex)
            {
                
            }
        }

        public async void getinfo()
        { 
            request request_city = new request();
            citys = new Get_set_city() { get_set_city = await request_city.getCity()};
            if (citys.get_set_city != null)
            {
                // await DisplayAlert("Message", "Total services: " + priceservice.price_service.Count.ToString(), "ok");
                foreach( City city in citys.get_set_city )
                {
                    city_picker.Items.Add(city.city_name);
                }

                return;
            }
            else
            {
                await DisplayAlert("Message", "We do not have anything for you!", "ok");
                return;
            }
        }

        private void PaymentBtn_Click(object sender, EventArgs e)
        {
            if (city_picker.SelectedIndex == -1 || city_picker.Items[0] == "select location first")
            {
                DisplayAlert("City", "City not selected", "OK");
                return;
            }
            if (pickup.IsToggled)
            {
                
                if (String.IsNullOrEmpty(address.Text) || String.IsNullOrWhiteSpace(address.Text))
                {
                    DisplayAlert("Address", "Please provide address", "OK");
                    return;
                }
                if (check_addr2.IsToggled)
                {
                    if (String.IsNullOrEmpty(address2.Text) || String.IsNullOrWhiteSpace(address2.Text))
                    {
                        DisplayAlert("Address", "Please provide Alternate address", "OK");
                        return;
                    }
                }
                
            }
            else
            {
                if (location_picker.SelectedIndex == -1 || location_picker.Items[0] == "select city first")
                {
                    DisplayAlert("Location", "Location not selected", "OK");
                    return;
                }
                if (store_picker.SelectedIndex == -1 || store_picker.Items[0] == "select location first")
                {
                    DisplayAlert("Address", "Address not selected", "OK");
                    return;
                }
            }
            if (date == 0)
            {
                DisplayAlert("Date", "Select Date", "OK");
                return;
            }
            if (time == 0)
            {
                DisplayAlert("Time", "Select Time", "OK");
                return;
            }
            Navigation.PushAsync(new CartPage());

        }

        private void Pdate2_Toggled(object sender, ToggledEventArgs e)
        {
            Switch sc = (Switch)sender;
            if (sc.IsToggled)
                date++;
            else
                date--;
            pdate1.IsToggled = false;
        }

        private void Pdate1_Toggled(object sender, ToggledEventArgs e)
        {
            Switch sc = (Switch)sender;
            if (sc.IsToggled)
                date++;
            else
                date--;
            pdate2.IsToggled = false;
        }

    }
}
