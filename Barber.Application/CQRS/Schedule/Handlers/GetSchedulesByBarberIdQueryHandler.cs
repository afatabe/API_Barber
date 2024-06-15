﻿using Barber.Application.CQRS.Schedule.Queries;
using Barber.Domain.Entities;
using Barber.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Barber.Application.CQRS.Schedule.Handlers
{
    public class GetSchedulesByBarberIdQueryHandler : IRequestHandler<GetSchedulesByBarberIdQuery, IEnumerable<Schedules>>
    {
        private readonly ISchedulesRepository _shedulesRepository;
        public GetSchedulesByBarberIdQueryHandler(ISchedulesRepository shedulesRepository)
        {
            _shedulesRepository = shedulesRepository;
        }
        public async Task<IEnumerable<Schedules>> Handle(GetSchedulesByBarberIdQuery request, CancellationToken cancellationToken)
        {
            if (request is null)
            {
                throw new ApplicationException("Error args is null");
            }
            var schedules = await _shedulesRepository.GetSchedulesByBarberId(request.IdBarber) ?? Enumerable.Empty<Schedules>();
            return schedules;
        }
    }
}
