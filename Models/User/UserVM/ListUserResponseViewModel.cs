namespace TodoCustomList.Models.User.UserVM
{
    public class ListUserResponseViewModel
    {
       public List<UserResponseViewModel> userResponseViewModels { get; set; }  
    }

    public class UserResponseViewModel
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }
    }
}
