﻿using Barber.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Barber.Domain.Interfaces
{
    public interface IBarberRepository
    {
        Task<BarberMain> AddNewBarberAsync(Barber.Domain.Entities.BarberMain barber);
        Task<BarberMain> RemoveBarberAsync(Barber.Domain.Entities.BarberMain barber);
        Task<IEnumerable<Barber.Domain.Entities.BarberMain>> GetBarbersAsync();  
        Task<BarberMain> SetDisponibilityAsync(Barber.Domain.Entities.BarberMain barber, bool disponibility);
    }
}
