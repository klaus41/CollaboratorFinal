using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Navision.Repository;

namespace Navision
{
    public class NavFacade
    {
        private ContactListRepository _contactListRepository;

        public ContactListRepository ContactList
        {
            get
            {
                return _contactListRepository ?? (_contactListRepository = new ContactListRepository());
            }
        }

    }
}