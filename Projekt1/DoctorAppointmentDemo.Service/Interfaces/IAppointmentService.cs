using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DoctorAppointmentDemo.Domain.Entities;

namespace DoctorAppointmentDemo.Service.Interfaces
{
    public interface IAppointmentService
    {
        Appointment Create(Appointment appointment);
        bool Delete(int id);
        Appointment? Get(int id);
        IEnumerable<Appointment> GetAll();
        Appointment Update(int id, Appointment appointment);
    }
}
