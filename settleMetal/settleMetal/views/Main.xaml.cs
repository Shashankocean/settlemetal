using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace settleMetal
{
    public partial class Main : MasterDetailPage
    {
        public Main()
        {
            InitializeComponent();
            Detail = new NavigationPage(new Home());
            IsPresented = false;
            getUser();
        }
        public async void getUser()
        {
            try
            {
                List<User> user = await LocalDB.db_connection.get_user();
                if (user.Count == 1)
                {
                    ShareInfo.user = user[0];
                    Detail = new NavigationPage(new Home());
                    IsPresented = false;
                }
                else
                {
                    await DisplayAlert("Login", "You have not login", "Ok");
                    Detail = new NavigationPage(new Login());
                }
            }
            catch(Exception ex)
            {
               
            }
        }
        void registerUser(object sender, System.EventArgs e)
        {
            Detail = new NavigationPage(new Register());
            IsPresented = false;
        }
        void loginUser(object sender, System.EventArgs e)
        {
            Detail = new NavigationPage(new Login());
            IsPresented = false;
        }
        void home(object sender, System.EventArgs e)
        {
            Detail = new NavigationPage(new Home());
            IsPresented = false;
        }
        
    }
}
