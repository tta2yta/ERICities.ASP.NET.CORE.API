﻿namespace CitiesAPI.ASP.NET.CORE.Services
{
    public interface IMailService
    {
        void send(string subject, string message);
    }
}