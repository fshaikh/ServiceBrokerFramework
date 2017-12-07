using Microsoft.AspNetCore.Http;
using ServiceBroker.Shared;
using System;
using System.Text;

namespace ServiceBroker.Middleware.AuthProvider
{
    /// <summary>
    /// Provides authentication using BASIC Authentication. From the SPEC:
    /// The marketplace MUST authenticate with the service broker using HTTP basic authentication
    /// (the Authorization: header) on every request. The broker is responsible for validating the
    /// username and password and returning a 401 Unauthorized message if credentials are invalid
    /// </summary>
    public class BasicAuthenticationProvider : IAuthenticationProvider
    {
        #region Constants
        /// <summary>
        /// Authorization header containing the credentials
        /// </summary>
        private const string AUTHORIZATION_HEADER = "Authorization";
        private const string BASIC_SCHEME = "Basic";
        /// <summary>
        /// Encoding to use for Basic Authentication
        /// <see cref="https://stackoverflow.com/questions/7242316/what-encoding-should-i-use-for-http-basic-authentication"/>
        /// </summary>
        private const string ISO_ENCODING = "iso-8859-1";
        #endregion Constants

        #region Members
        private ICredentialsProvider _credentialsProvider;
        #endregion Members

        #region Constructors
        /// <summary>
        /// Initializes a new instance of 
        /// </summary>
        /// <param name="credentialsProvider"></param>
        public BasicAuthenticationProvider(ICredentialsProvider credentialsProvider)
        {
            _credentialsProvider = credentialsProvider;
        }

        public static BasicAuthenticationProvider Create(ICredentialsProvider credentialsProvider)
        {
            return new BasicAuthenticationProvider(credentialsProvider);
        }
        #endregion Constructors

        #region IAuthenticationProvider Methods

        /// <summary>
        /// Validates using HTTP Basic Authentication Scheme
        /// </summary>
        /// <param name="httpContext"></param>
        /// <returns></returns>
        public AuthenticationResponse Validate(HttpContext httpContext)
        {
            AuthenticationResponse response = new AuthenticationResponse();

            // Fetch the service broker credentials from the credentials provider
            Credentials validCreds = _credentialsProvider.FetchCredentials();

            // Read the credentials from the HTTP request
            // 1. Check if the API Version Header is present. If not, return error
            if (!httpContext.Request.Headers.ContainsKey(AUTHORIZATION_HEADER))
            {
                Console.WriteLine("Authorization Header missing");
                response.IsSuccess = false;
                response.ResponseCode = ResponseCode.UnAuthorized;
                return response;
            }
            var value = httpContext.Request.Headers[AUTHORIZATION_HEADER].ToString();

            // 1. Check if the value is not null/empty or doesnt start with Basic
            if(string.IsNullOrEmpty(value) || !value.StartsWith(BASIC_SCHEME))
            {
                Console.WriteLine("Invalid Authorization Header");
                response.IsSuccess = false;
                response.ResponseCode = ResponseCode.UnAuthorized;
                return response;
            }

            // 2. Parse the value.
            //    2.a) Remove the Basic string
            string parsedValue = value.Substring(BASIC_SCHEME.Length).Trim();
            //    2.b) Base64-decode the value
            Encoding encoding = Encoding.GetEncoding(ISO_ENCODING);
            parsedValue = encoding.GetString(Convert.FromBase64String(parsedValue));
            //    2.c) username:password. Extract the values
            string[] splitValues = parsedValue.Split(new char[] { ':' });

            Credentials credentials = new Credentials
            {
                Username = splitValues[0],
                Password = splitValues[1]
            };


            if (credentials.Username == validCreds.Username &&
               credentials.Password == validCreds.Password)
            {
                response.IsSuccess = true;
                response.ResponseCode = ResponseCode.ValidCredentials;
            }
            else
            {
                response.IsSuccess = false;
                response.ResponseCode = ResponseCode.UnAuthorized;
            }

            return response;
        }

        #endregion IAuthenticationProvider Methods
    }
}
