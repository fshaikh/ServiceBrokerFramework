using ServiceBroker.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServiceBroker.App
{
    public class ServiceProvisionParameterValidator : IServiceProvisionParameterValidator
    {
        public List<ResponseBase> IsValid(ServiceProvisionRequest request)
        {
            return new List<ResponseBase>();
        }
    }
}
