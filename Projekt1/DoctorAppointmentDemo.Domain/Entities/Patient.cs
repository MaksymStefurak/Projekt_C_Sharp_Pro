﻿using DoctorAppointmentDemo.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorAppointmentDemo.Domain.Entities
{
    public class Patient : UserBase
    {
        public IllnessTypes IllnessType { get; set; }

        public string? AdditionalInfo { get; set; }

        public string? Address { get; set; }
        public int Age { get; set; }    
    }
}
