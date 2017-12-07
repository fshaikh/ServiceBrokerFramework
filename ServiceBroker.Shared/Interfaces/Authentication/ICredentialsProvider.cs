using System;
using System.Collections.Generic;
using System.Text;

namespace ServiceBroker.Shared
{
    /// <summary>
    /// Implementers must realize the interface to provide Credentials data.
    /// </summary>
    public interface ICredentialsProvider
    {
        /// <summary>
        /// Fetches the credentials from input sources. For eg: Environment variables, CredHub, Vaults, etc
        /// Define a separate provider for each of the input sources
        /// </summary>
        /// <returns></returns>
        Credentials FetchCredentials();
    }
}
