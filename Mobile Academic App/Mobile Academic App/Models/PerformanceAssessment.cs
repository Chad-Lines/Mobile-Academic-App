﻿using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace Mobile_Academic_App.Models
{
    public class PerformanceAssessment
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}