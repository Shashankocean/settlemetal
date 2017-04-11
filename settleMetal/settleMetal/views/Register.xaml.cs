using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using Xamarin.Forms;

namespace settleMetal
{
    public partial class Register : ContentPage
    {
        HttpClient client;
        Entry username,conform_password, phone_number,email,password;
        public Register()
        {
            InitializeComponent();
            username = new Entry()
            {
                Placeholder = "User name",

            };
            email = new Entry()
            {
                Placeholder = "Email",
                

            };
            password = new Entry()
            {
                Placeholder = "Password",

            };
            conform_password = new Entry()
            {
                Placeholder = "Conform password",

            };
            phone_number = new Entry()
            {
                Placeholder = "Phone number",
                Keyboard = Keyboard.Numeric,

            };
            Button btn_register = new Button()
            {
                Text = "Register",
            };
            btn_register.Clicked += Btn_register_Clicked;
            StackLayout stack_register = new StackLayout()
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                Orientation = StackOrientation.Vertical,
            };
            stack_register.Children.Add(username);
            stack_register.Children.Add(email);
            stack_register.Children.Add(password);
            stack_register.Children.Add(conform_password);
            stack_register.Children.Add(phone_number);
            stack_register.Children.Add(btn_register);
            Content = stack_register;
        }

        private void Btn_register_Clicked(object sender, EventArgs e)
        {
            set_user(new User {
                user_name = username.Text,
                user_email = email.Text,
                pass4word = password.Text,
                user_phone =phone_number.Text
            });

            
        }
        public async void set_user(User user )
        {
            string content = "nothing";
            client = new HttpClient();

            try
            {
                var json = JsonConvert.SerializeObject(user);
                await DisplayAlert("Message", json, "OK");

                content = await new request().requestSettle("mregister.php", json);

                    if (content!="null" || content != null)
                    {

                        if (content == "success")
                        {
                            await DisplayAlert("Welcome", "You have Successfuly register to \"SettleMetal\"", "OK");
                        }
                        else
                        {
                            
                           await DisplayAlert("Oops!", "Something went wrong! "+content, "OK");
                            
                        }
                    }
                    else
                    {
                        await DisplayAlert("Oops!", content, "OK");
                    }
                 
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error!", ex.Message, "OK");

            }
            
            
            await Navigation.PushAsync(new Home());


        }
    }
}
