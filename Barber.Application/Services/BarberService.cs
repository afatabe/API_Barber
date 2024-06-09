﻿using AutoMapper;
using Barber.Application.CQRS.Barber.Commands;
using Barber.Application.DTOs;
using Barber.Application.DTOs.Register;
using Barber.Application.Interfaces;
using Barber.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Barber.Application.Services
{
    public class BarberService : IBarberService
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        public BarberService(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        public async Task<BarberRegisterDTO> AddNewBarberAsync(BarberRegisterDTO barberDTO)
        {
            var registerBarberCommand = _mapper.Map<RegisterBarberCommand>(barberDTO);
            return await _mediator.Send(registerBarberCommand);
        }

        public Task<IEnumerable<BarberDTO>> GetBarbersAsync()
        {
            throw new NotImplementedException();
        }

        public Task<BarberDTO> RemoveBarberAsync(BarberDTO barberDTO)
        {
            throw new NotImplementedException();
        }

        public Task<BarberDTO> SetDisponibilityAsync(BarberDTO barberDTO, bool disponibility)
        {
            throw new NotImplementedException();
        }
    }
}
