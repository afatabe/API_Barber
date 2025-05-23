﻿using Barber.Domain.Entities.Enums;
using Barber.Domain.Validation;

namespace Barber.Domain.Entities
{
    public sealed class Schedules
    {
        public int Id { get; private set; }
        public int IdBarber { get; private set; }
        public int IdClient { get; private set; }
        public TypeOfService TypeOfService { get; private set; }    
        public DateTime DateSchedule { get; private set; }
        public decimal ValueForService { get; private set; }
        public bool IsFinalized { get; private set; }
        public string ClientName { get; private set; }
        public string BarberName { get; private set; }

        public Client _Client { get; private set; }  
        public BarberMain _Barber { get; private set; }
        
        private void ValidateDomain(int idBarber, int idClient, TypeOfService typeOfService, DateTime dateSchedule, decimal valueOfService, bool isFinalized)
        {
            DomainExceptionValidation.When(idBarber == 0, "ID Barber is required!");
            DomainExceptionValidation.When(idClient == 0, "ID Client is required!");
            DomainExceptionValidation.When(typeOfService == 0, "Error, type of service cant be null!");
            DomainExceptionValidation.When(string.Equals(dateSchedule.Year.ToString(), "0001"),"Error, Date Schedule is required!");
            DomainExceptionValidation.When(decimal.IsNegative(valueOfService),"Error!, Value of service has to be positive!");
            DomainExceptionValidation.When(valueOfService > 999,"Error, 999 Is maximum value!");
            IdBarber = idBarber;
            IdClient = idClient;
            TypeOfService = typeOfService;
            DateSchedule = dateSchedule;
            ValueForService = valueOfService;
            IsFinalized = isFinalized;
        }
        
        public Schedules(int id, int idBarber, int idClient, TypeOfService typeOfService, DateTime dateSchedule, decimal valueForService, bool isFinalized)
        {
            ValidateDomain(idBarber, idClient, typeOfService, dateSchedule, valueForService, isFinalized);
            Id = id;
        }
        public Schedules(int idBarber, int idClient, TypeOfService typeOfService, DateTime dateSchedule, decimal valueForService, bool isFinalized)
        {
            ValidateDomain(idBarber, idClient, typeOfService, dateSchedule, valueForService, isFinalized);
        }
        public void SetNames(string clientName, string barberName)
        {
            DomainExceptionValidation.When(string.IsNullOrEmpty(clientName) || string.IsNullOrEmpty(barberName), "O nome é requerido!");
            ClientName = clientName;
            BarberName = barberName;
            
        }
        public void SetBarber(BarberMain barber)
        {
            _Barber = barber;
        }
        public void SetClient(Client client)
        {
            _Client = client;
        }
        public void UpdateValueForService(decimal amount)
        {
            ValueForService = amount;
        }
        public void Update(int idBarber, int idClient, TypeOfService typeOfService, DateTime dateSchedule, decimal valueForService, bool isFinalized)
        {
            ValidateDomain(idBarber, idClient, typeOfService, dateSchedule, valueForService, isFinalized);
        }
        public void SetIsClose(bool close)
        {
            IsFinalized = close;
        }
    }
}
