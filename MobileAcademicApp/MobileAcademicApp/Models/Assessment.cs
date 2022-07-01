﻿using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace MobileAcademicApp.Models
{
    public class Assessment
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime DueDate { get; set; }
    }
}