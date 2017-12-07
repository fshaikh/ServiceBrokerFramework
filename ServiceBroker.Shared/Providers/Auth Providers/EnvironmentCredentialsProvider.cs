using System;


namespace ServiceBroker.Shared
{
    /// <summary>
    /// Fetches the credentials data from envrionment variables
    /// </summary>
    public class EnvironmentCredentialsProvider : ICredentialsProvider
    {
        #region Constants
        private const string USERNAME_ENV_KEY = "username";
        private const string PASSWORD_ENV_KEY = "password";
        #endregion Constants

        #region ICredentialsProvider Methods
        /// <summary>
        /// Fetches the credentials from environment variables. Ensure the env variables are set at the time of
        /// deploying the service broker application
        /// </summary>
        /// <returns></returns>
        public Credentials FetchCredentials()
        {
            Credentials credentials = new Credentials();
            // Read from the environment variables
            credentials.Username = Environment.GetEnvironmentVariable(USERNAME_ENV_KEY);
            credentials.Password = Environment.GetEnvironmentVariable(PASSWORD_ENV_KEY);
            return credentials;
        }
        #endregion ICredentialsProvider Methods
    }
}
