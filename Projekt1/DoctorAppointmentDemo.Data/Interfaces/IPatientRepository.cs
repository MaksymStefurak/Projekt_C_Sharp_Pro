﻿using DoctorAppointmentDemo.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorAppointmentDemo.Data.Interfaces
{
    public interface IPatientRepository : IGenericRepository<Patient>
    {
        // you can add more specific doctor's methods
    }
}
