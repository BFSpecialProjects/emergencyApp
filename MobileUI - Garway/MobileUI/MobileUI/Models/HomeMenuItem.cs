using System;
using System.Collections.Generic;
using System.Text;

namespace MobileUI.Models
{
    public enum MenuItemType
    {
        MainBuilding,
        Modules,
        Trailers,
    }
    public class HomeMenuItem
    {
        public MenuItemType Id { get; set; }

        public string Title { get; set; }
    }
}
