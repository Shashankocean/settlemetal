using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Reflection.Emit;
using System.Text;

using Xamarin.Forms;

namespace settleMetal
{
    public partial class Login : ContentPage
    {
        Entry  email, password;
        public Login()
        {
            
            email = new Entry()
            {
                Placeholder = "Email",


            };
            password = new Entry()
            {
                Placeholder = "Password",

            };

            Button btn_register = new Button()
            {
                Text = "Login",
            };
            btn_register.Clicked += Btn_register_Clicked;
            StackLayout stack_register = new StackLayout()
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.Center,
                Orientation = StackOrientation.Vertical,
            };
            stack_register.Children.Add(email);
            stack_register.Children.Add(password);
            stack_register.Children.Add(btn_register);
            Content = stack_register;
        }

        private void Btn_register_Clicked(object sender, EventArgs e)
        {
            isUser(new OnlyUser
            { 
                user_email = email.Text,
                pass4word = password.Text,
            });
        }
        public async void isUser(OnlyUser user)
        {
            string content = "nothing";

            try
            {
                var json = JsonConvert.SerializeObject(user);
                
                content = await new request().requestSettle("mlogin.php", json);

                if (content != "null" || content != null)
                {
                    Get_Set_User users = new Get_Set_User { user = JsonConvert.DeserializeObject<List<User>>(content) };
                    if (users.user.Count > 1)
                    {
                        await DisplayAlert("Alert!", "More then one user exsist", "OK");
                    }
                    if(users.user.Count == 0)
                    {
                        await DisplayAlert("Invalid!", "Invalid Email/Password", "OK");
                    }
                    else
                    {
                        foreach (User usr in users.user)
                        {
                            await LocalDB.db_connection.updateUser(usr);
                            ShareInfo.user = usr;
                            await DisplayAlert("Success !", "Welcome to \"SettleMetal\" " + usr.user_name, "OK");
                            
                        }
                    }
                }
                else
                {
                    await DisplayAlert("Oops!", content, "OK");
                }

            }
            catch (Exception ex)
            {
                await DisplayAlert("Error !", ex.Message, "OK");

            }
            
            
        }
    }
}
