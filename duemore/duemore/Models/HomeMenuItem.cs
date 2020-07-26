﻿using System;
using System.Collections.Generic;
using System.Text;

namespace DueMore.Models
{
    public enum MenuItemType
    {
        Inbox,
        Tasks,
        Projects,
        Calendar,
        Filters,
        Settings,
        About
    }

    public class HomeMenuItem
    {
        public MenuItemType Id { get; set; }
        public string Title { get; set; }
        public string ImageSource { get; set; }
    }
}
