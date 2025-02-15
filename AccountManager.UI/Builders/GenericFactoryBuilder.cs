﻿using AccountManager.Core.Factories;
using AccountManager.Core.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountManager.UI.Builders
{
    public sealed class GenericFactoryBuilder<TKey, TInterface> : IGenericFactoryBuilder<TKey, TInterface> where TKey : notnull, new()
    {
        private Dictionary<TKey, Type> _implementations { get; set; }
        private readonly IServiceCollection _services;
        public GenericFactoryBuilder(IServiceCollection services)
        {
            _implementations = new Dictionary<TKey, Type>();
            _services = services;
        }
        public IGenericFactoryBuilder<TKey, TInterface> AddImplementation<TImplementation>(TKey key) where TImplementation : class, TInterface
        {
            _services.AddSingleton<TImplementation>();
            _implementations.Add(key, typeof(TImplementation));
            return this;
        }

        public void Build()
        {
            _services.AddSingleton<IGenericFactory<TKey, TInterface>>(services =>
            {
                return new GenericFactory<TKey, TInterface>(_implementations, services);
            });
        }
    }
}
