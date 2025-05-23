﻿using Barber.Application.CQRS.Schedule.Queries;
using Barber.Domain.Entities;
using Barber.Domain.Interfaces;
using Barber.Domain.Validation;
using MediatR;


namespace Barber.Application.CQRS.Schedule.Handlers
{
    public class GetScheduleByIdQueryHandler : IRequestHandler<GetScheduleByIdQuery, Schedules>
    {
        private readonly IUnityOfWork _uof;
        public GetScheduleByIdQueryHandler(IUnityOfWork uof)
        {
            _uof = uof; 
        }
        public async Task<Schedules> Handle(GetScheduleByIdQuery request, CancellationToken cancellationToken)
        {
            if (request is null)
            {
                throw new ApplicationException("Error args is null");
            }
            var schedule = await _uof.SchedulesRepository.GetByIdDataAsync(request.Id);
            if(schedule is null)
            {
                return null;
            }
            var names = await _uof.SchedulesRepository.GetNameById(schedule.IdClient, schedule.IdBarber);
            schedule.SetNames(names[2], names[1]);
            return schedule;
           
        }
    }
}
