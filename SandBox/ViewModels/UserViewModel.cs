using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SandBox.ViewModels
{
    public class UserViewModel
    {
        public string Id { get; set; }
        public string Nickname { get; set; }
        public bool AlreadySubscribedTo { get; set; }

        public string GetLinkText()
        {
            return AlreadySubscribedTo ? "Anuluj subskrypcję" : "Subskrybuj";
        }

        public string GetAssociatedAction()
        {
            return AlreadySubscribedTo ? "UnsubscribeFromUser" : "SubscribeToUser";
        }

        public string GetHtmlAttributes()
        {
            return AlreadySubscribedTo ? "btn btn-danger btn-block" : "btn btn-primary btn-block";
        }
    }
}