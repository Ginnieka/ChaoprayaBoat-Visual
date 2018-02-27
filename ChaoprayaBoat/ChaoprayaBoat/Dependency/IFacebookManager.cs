using System;
using System.Threading.Tasks;
using ChaoprayaBoat.Library.Facebook;
using Xamarin.Forms;

namespace ChaoprayaBoat
{
    public interface IFacebookManager
    {
        Task<bool> SimpleLogin();
        Task<FacebookUser> Login();
        Task LogOut();
        Task<bool> ValidateToken();
        Task<bool> PostText(string message);
        Task<bool> PostPhoto(ImageSource image);
    }
}