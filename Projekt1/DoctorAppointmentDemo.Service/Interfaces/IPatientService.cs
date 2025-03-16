using DoctorAppointmentDemo.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorAppointmentDemo.Service.Interfaces
{
    public interface IPatientService
    {
        Patient Create(Patient patient);
        bool Delete(int id);
        Patient? Get(int id);
        IEnumerable<Patient> GetAll();
        Patient Update(int id, Patient patient);
    }

}
