﻿using System;
using Microsoft.WindowsAzure.Storage.Table;

namespace Slidable.UserProfile.Data
{
    public class ShowEntity : TableEntity
    {
        public string ShowId { get; set; }
        public DateTimeOffset ViewedAt { get; set; }
    }
}