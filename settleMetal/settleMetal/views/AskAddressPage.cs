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
        EntryCell address,address2;
        SwitchCell pdate1, pdate2, ptime1, ptime2, ptime3, check_addr2;
        int date = 0, time = 0;
        Get_set_city citys;
        Get_Set_Location Locations;
        Get_Set_StoreAddress storeList;
        Picker city_picker, location_picker, store_picker;
        TableSection sec1;
        public AskAddressPage()
        {
            Title = "User Info";
            DateTime date = DateTime.Now;
            DateTime date2 = date.AddDays(1);
            TableView table_info = new TableView { Intent = TableIntent.Form, BackgroundColor = Color.FromHex("#C3C3C3")};
            TableRoot tableroot = new TableRoot();
            table_info.Root = tableroot;

            TableSection sec0 = new TableSection() { Title = "City" };
            city_picker = new Picker
            {
                Title = "Select City",
                VerticalOptions = LayoutOptions.CenterAndExpand,
                Margin =new Thickness(10,0,5,0),
            };
            city_picker.SelectedIndexChanged += City_picker_SelectedIndexChanged;
            ViewCell city_pickerview = new ViewCell();
            city_pickerview.View = city_picker;
            sec0.Add(city_pickerview);

            sec1 = new TableSection() { Title = "Get your device picked up from your Address",};
            address = new EntryCell { Placeholder = "Pick-up & Drop-off Address"};
            address2 = new EntryCell { Placeholder = "Address"};

            check_addr2 = new SwitchCell { Text = "Alternate Drop-off Address:(optional)", On = false };
            check_addr2.OnChanged += Check_addr2_OnChanged;
            sec1.Add(address);
            sec1.Add(check_addr2);
            //sec1.Add(address2);
            

            TableSection sec2 = new TableSection() { Title = "Pick-up Date" };
            pdate1 = new SwitchCell { Text = date.ToString("dd:MM:yyy"), On=false };
            pdate2 = new SwitchCell { Text = date2.ToString("dd:MM:yyy"), On = false };
            pdate1.Tapped += Pdate1_Tapped;
            pdate1.OnChanged += Pdate1_OnChanged;
            pdate2.OnChanged += Pdate2_OnChanged;

            sec2.Add(pdate1);
            sec2.Add(pdate2);

            TableSection sec3 = new TableSection() { Title = "Pick-up Time" };
            ptime1 = new SwitchCell { Text = "10-11:59 AM", On = false };
            ptime2 = new SwitchCell { Text = "12-02:00 PM", On = false };
            ptime3 = new SwitchCell { Text = "02-04:00 PM", On = false };
            ptime1.OnChanged += ptime1_OnChanged;
            ptime2.OnChanged += ptime2_OnChanged;
            ptime3.OnChanged += ptime3_OnChanged;


            sec3.Add(ptime1);
            sec3.Add(ptime2);
            sec3.Add(ptime3);

            TableSection sec4 = new TableSection() { Title = "Drop off your device at our store" };
             location_picker = new Picker
            {
                Title = "select nearest location",
                VerticalOptions = LayoutOptions.CenterAndExpand,
                 Margin = new Thickness(10, 0, 5, 0),
             };
            location_picker.Items.Add("Select city first");
            location_picker.SelectedIndexChanged += Location_picker_SelectedIndexChanged;
            ViewCell pickerview = new ViewCell();
            pickerview.View = location_picker;
            sec4.Add(pickerview);

            store_picker = new Picker
            {
                Title = "select nearest area",
                VerticalOptions = LayoutOptions.CenterAndExpand,
                Margin = new Thickness(10, 0, 5, 0),
            };
            store_picker.Items.Add("Select location first");
            ViewCell store_pickerView = new ViewCell();
            store_pickerView.View = store_picker;
            sec4.Add(store_pickerView);

            var nextBtn = new Button { Text = "Payment" };
            nextBtn.Clicked += PaymentBtn_Click;
            ViewCell paymentView = new ViewCell();
            paymentView.View = nextBtn;
            sec3.Add(paymentView);

            tableroot.Add(sec0);
            tableroot.Add(sec1);
            tableroot.Add(sec4);
            tableroot.Add(sec2);
            tableroot.Add(sec3);


            ScrollView mainscrl = new ScrollView();

            mainscrl.Content = table_info;
            Content = mainscrl;

            getinfo();
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
            if (String.IsNullOrEmpty(address.Text) || String.IsNullOrWhiteSpace(address.Text))
            {
                DisplayAlert("Address", "Please provide address", "OK");
                return;
            }
            if (check_addr2.On == true)
            {
                if (String.IsNullOrEmpty(address2.Text) || String.IsNullOrWhiteSpace(address2.Text))
                {
                    DisplayAlert("Address", "Please provide Alternate address", "OK");
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
            if(location_picker.SelectedIndex == -1 || location_picker.Items[0] =="select city first")
            {
                DisplayAlert("Location", "Location not selected", "OK");
                return;
            }
            if (store_picker.SelectedIndex == -1 || store_picker.Items[0] == "select location first")
            {
                DisplayAlert("Address", "Address not selected", "OK");
                return;
            }
            Navigation.PushAsync(new CartPage());

        }

        private void Pdate1_Tapped(object sender, EventArgs e)
        {
            pdate2.On = false;
        }

        private void ptime3_OnChanged(object sender, ToggledEventArgs e)
        {
            SwitchCell sc = (SwitchCell)sender;
            if (sc.On)
                time++;
            else
                time--;
            ptime2.On = false;
            ptime1.On = false;
        }

        private void ptime2_OnChanged(object sender, ToggledEventArgs e)
        {
            SwitchCell sc = (SwitchCell)sender;
            if (sc.On)
                time++;
            else
                time--;
            ptime1.On = false;
            ptime3.On = false;
        }

        private void ptime1_OnChanged(object sender, ToggledEventArgs e)
        {
            SwitchCell sc = (SwitchCell)sender;
            if (sc.On)
                time++;
            else
                time--;
            ptime2.On = false;
            ptime3.On = false;
        }

        private void Pdate2_OnChanged(object sender, ToggledEventArgs e)
        {
            SwitchCell sc = (SwitchCell)sender;
            if (sc.On)
                date++;
            else
                date--;
            pdate1.On = false;
        }

        private void Pdate1_OnChanged(object sender, ToggledEventArgs e)
        {
            SwitchCell sc = (SwitchCell)sender;
            if (sc.On)
                date++;
            else
                date--;
            pdate2.On = false;
        }

        private void Check_addr2_OnChanged(object sender, ToggledEventArgs e)
        {
            SwitchCell sc = (SwitchCell)sender;
            if (sc.On)
            {
                sec1.Add(address2);
            }
            else
            {
                sec1.Remove(address2);
            }
        }
    }
}
