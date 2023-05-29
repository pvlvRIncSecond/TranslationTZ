using System;
using System.Collections.Generic;

namespace Infrastructure.Services
{
    public class ServiceLocator
    {
        public static ServiceLocator Container => _instance ??= new ServiceLocator();
        
        private static ServiceLocator _instance;
        
        private Dictionary<Type, IService> _services = new Dictionary<Type, IService>();

        public TService Single<TService>() where TService : class, IService => 
            _services[typeof(TService)] as TService;

        public void RegisterSingle<TService>(TService service) where TService : class, IService => 
            _services[typeof(TService)] = service;
    }
}