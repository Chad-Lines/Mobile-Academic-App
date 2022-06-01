﻿using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace Mobile_Academic_App.Models
{
    public class Term
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime DateTime { get; set; }
        public IEnumerable<Course> Courses { get; set; }
    }
}