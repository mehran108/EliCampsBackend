using ELI.Domain.ViewModels;
using ELI.Entity.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ELI.Domain.Common
{
    public class UserConverter
    {
        public static UserViewModel Convert(Users user)
        {
            var userViewModel = new UserViewModel();
            userViewModel.Id = user.Id;
            userViewModel.Email = user.Email;
            userViewModel.FirstName = user.FirstName;
            return userViewModel;
        }

        public static List<UserViewModel> ConvertList(IEnumerable<Users> users)
        {
            return users.Select(user =>
            {
                var userViewModel = new UserViewModel();
                userViewModel.Id = user.Id;
                userViewModel.Email = user.Email;
                userViewModel.FirstName = user.FirstName;
                return userViewModel;
            }).ToList();
        }
    }
}
