﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Занятие_в_аудитории_1_29._08._2023__ADO.NET_
{
    public class Manager
    {
        public Guid Id { get; set; }
        public String Surname { get; set; } = null!;
        public String Name { get; set; } = null!;
        public String Secname { get; set; } = null!;
        public String Login { get; set; } = null!;
        public String PassSalt { get; set; } = null!;
        public String PassDk { get; set; } = null!;
        public Guid IdMainDep { get; set; }
        public Guid? IdSecDep { get; set; }
        public Guid? IdChief { get; set; }
        public DateTime CreateDt { get; set; }
        public DateTime? DeleteDt { get; set; }
        public String Email { get; set; } = null!;
        public String? Avatar { get; set; }
    }
}
