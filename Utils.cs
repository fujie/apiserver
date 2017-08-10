#define TRACE
using Microsoft.MetadirectoryServices;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;


namespace FimSync_Ezma
{
    class Utils
    {
        TraceSource traceSource = new TraceSource("Generic REST MA", SourceLevels.All);

        // Proxy Server Type
        public enum ProxyServerType
        {
            NoProxy,
            Proxy,
            ProxyWithAuthN
        }

        //
        // common utilities
        //
        // logging
        public void Logger(TraceEventType _traceEventType, int _id, string _message)
        {
            traceSource.TraceEvent(_traceEventType, _id, _message);
            traceSource.Flush();
        }
        // Base64URL Encode
        private string base64UrlEncode(byte[] _input)
        {
            var _output = Convert.ToBase64String(_input);
            _output = _output.Split('=')[0]; // Remove any trailing '='s
            _output = _output.Replace('+', '-'); // 62nd char of encoding
            _output = _output.Replace('/', '_'); // 63rd char of encoding
            return _output;
        }
        // descrypt secure string
        public string DecryptSecureString(SecureString _input)
        {
            IntPtr _pointer = Marshal.SecureStringToBSTR(_input);
            string _output = Marshal.PtrToStringUni(_pointer);
            return _output;
        }

        //
        // utilities for HTTP connection
        //
        // Create HTTP Client
        private HttpClient createHttpClient(WebProxy _webProxy)
        {
            HttpClient _httpClient;
            try
            {
                if (_webProxy != null)
                {
                    var _httpClientHandler = new HttpClientHandler();
                    _httpClientHandler.Proxy = _webProxy;
                    _httpClientHandler.UseProxy = true;
                    _httpClient = new HttpClient(_httpClientHandler);
                }
                else
                {
                    _httpClient = new HttpClient();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            return _httpClient;
        }
        // HTTP Async POST
        private async Task<string> postContents(string _url, Dictionary<string, string> _postData, WebProxy _webProxy)
        {
            try
            {
                var _httpClient = createHttpClient(_webProxy);
                var _content = new FormUrlEncodedContent(_postData);
                var _response = await _httpClient.PostAsync(_url, _content);
                return await _response.Content.ReadAsStringAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }
        // HTTP Async POST with Access Token
        private async Task<string> postContentsWithAccessToken(string _url, string _accessToken, string _postData, WebProxy _webProxy)
        {
            var _httpClient = createHttpClient(_webProxy);
            try
            {
                _httpClient.DefaultRequestHeaders.Add("x-cdata-authtoken", _accessToken);
                _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var _stringContent = new StringContent(_postData, System.Text.Encoding.UTF8, "application/json");
                var _response = await _httpClient.PostAsync(_url, _stringContent);
                return await _response.Content.ReadAsStringAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }
        // HTTP Async PUT with Access Token
        private async Task<string> putContentsWithAccessToken(string _url, string _accessToken, string _putData, WebProxy _webProxy)
        {
            var _httpClient = createHttpClient(_webProxy);
            try
            {
                _httpClient.DefaultRequestHeaders.Add("x-cdata-authtoken", _accessToken);
                _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var _stringContent = new StringContent(_putData, System.Text.Encoding.UTF8, "application/json");
                var _response = await _httpClient.PutAsync(_url, _stringContent);
                return await _response.Content.ReadAsStringAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }
        // HTTP Async DELETE with AccessToken
        private async Task<string> deleteContentsWithAccessToken(string _uri, string _accessToken, WebProxy _webProxy)
        {
            try
            {
                var _httpClient = createHttpClient(_webProxy);
                _httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + _accessToken);
                _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var _response = await _httpClient.DeleteAsync(_uri);
                return await _response.Content.ReadAsStringAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }
        // HTTP Async GET with Access Token
        private async Task<string> getContentsWithAccessToken(string _url, string _accessToken, WebProxy _webProxy)
        {
            try
            {
                var _httpClient = createHttpClient(_webProxy);
                _httpClient.DefaultRequestHeaders.Add("x-cdata-authtoken", _accessToken);
                return await _httpClient.GetStringAsync(_url);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }
        // HTTP POST with Access Token
        public string PostContentsWithAccessToken(string _url, string _accessToken, string _postData, WebProxy _webProxy)
        {
            string _result = null;
            try
            {
                Task _task = Task.Factory.StartNew(async () =>
                {
                    _result = await postContentsWithAccessToken(_url, _accessToken, _postData, _webProxy);
                }).Unwrap();
                _task.Wait();
                return _result;
            }
            catch (Exception ex)
            {

                throw new Exception("Exception in PostContentsWithAccessToken", ex);
            }
        }
        // HTTP PUT with Access Token
        public string PutContentsWithAccessToken(string _url, string _accessToken, string _putData, WebProxy _webProxy)
        {
            string _result = null;
            try
            {
                Task _task = Task.Factory.StartNew(async () =>
                {
                    _result = await putContentsWithAccessToken(_url, _accessToken, _putData, _webProxy);
                }).Unwrap();
                _task.Wait();
                return _result;
            }
            catch (Exception ex)
            {
                throw new Exception("Exception in PutContentsWithAccessToken", ex);
            }
        }
        // HTTP DELETE with Access Token
        public string DeleteContentsWithAccessToken(string _url, string _accessToken, WebProxy _webProxy)
        {
            string _result = null;
            try
            {
                Task _task = Task.Factory.StartNew(async () =>
                {
                    _result = await deleteContentsWithAccessToken(_url, _accessToken, _webProxy);
                }).Unwrap();
                _task.Wait();
                return _result;
            }
            catch (Exception ex)
            {
                throw new Exception("Exception in DeleteContentsWithAccessToken", ex);
            }
        }
        // HTTP GET with Access Token
        public string GetContentsWithAccessToken(string _url, string _accessToken, WebProxy _webProxy)
        {
            string _result = null;
            try
            {
                Task _task = Task.Factory.StartNew(async () =>
                {
                    _result = await getContentsWithAccessToken(_url, _accessToken, _webProxy);
                }).Unwrap();
                _task.Wait();
                return _result;
            }
            catch (Exception ex)
            {
                throw new Exception("Exception in GetContentsWithAccessToken", ex);
            }
        }

        // Add attribute to CS entry
        // for multi value
        public void AddCSEntryAttribute(AttributeType _attributeType, CSEntryChange _csentry, string _attributeName, List<object> _attributeValueList)
        {
            try
            {
                switch (_attributeType)
                {
                    case AttributeType.String:
                        _csentry.AttributeChanges.Add(AttributeChange.CreateAttributeAdd(_attributeName, _attributeValueList));
                        break;
                    default:
                        throw new ExtensionException("Unsupported attribute type for multiple value : " + _attributeType.GetTypeCode().ToString());
                }
            }
            catch (Exception ex)
            {
                throw new ExtensibleExtensionException("Exception in AddCSEntryAttribute", ex);
            }
        }
        // for single value
        public void AddCSEntryAttribute(AttributeType _attributeType, CSEntryChange _csentry, string _attributeName, object _attributeValue)
        {
            try
            {
                switch (_attributeType)
                {
                    case AttributeType.String:
                        _csentry.AttributeChanges.Add(AttributeChange.CreateAttributeAdd(_attributeName, _attributeValue.ToString()));
                        break;
                    case AttributeType.Boolean:
                        _csentry.AttributeChanges.Add(AttributeChange.CreateAttributeAdd(_attributeName, System.Convert.ToBoolean(_attributeValue.ToString())));
                        break;
                    case AttributeType.Integer:
                        _csentry.AttributeChanges.Add(AttributeChange.CreateAttributeAdd(_attributeName, System.Convert.ToInt64(_attributeValue.ToString())));
                        break;
                    default:
                        throw new ExtensionException("Unsupported attribute type : " + _attributeType.GetTypeCode().ToString());
                }
            }
            catch (Exception ex)
            {
                throw new ExtensibleExtensionException("Exception in AddCSEntryAttribute", ex);
            }
        }
    }
}
