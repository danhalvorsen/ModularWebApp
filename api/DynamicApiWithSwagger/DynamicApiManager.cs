namespace NSDynamicApiManager
{
    using System;
    using System.Collections.Generic;
    using Microsoft.OpenApi.Models;

    public class DynamicApiManager
    {
        private readonly Dictionary<string, (Func<IDictionary<string, object>, object> Implementation, OpenApiOperation Operation)> _dynamicApis;

        public DynamicApiManager()
        {
            _dynamicApis = new Dictionary<string, (Func<IDictionary<string, object>, object>, OpenApiOperation)>();
        }

        public void AddApi(string name, Func<IDictionary<string, object>, object> implementation, OpenApiOperation operation)
        {
            if (_dynamicApis.ContainsKey(name))
                throw new InvalidOperationException($"API '{name}' already exists.");

            _dynamicApis[name] = (implementation, operation);
        }

        public object ExecuteApi(string name, IDictionary<string, object> parameters)
        {
            if (!_dynamicApis.TryGetValue(name, out var entry))
                throw new InvalidOperationException($"API '{name}' does not exist.");

            return entry.Implementation(parameters);
        }

        public Dictionary<string, OpenApiOperation> GetAllApis()
        {
            var apiDescriptions = new Dictionary<string, OpenApiOperation>();
            foreach (var api in _dynamicApis)
            {
                apiDescriptions[api.Key] = api.Value.Operation;
            }
            return apiDescriptions;
        }
    }

}