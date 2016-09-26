using System;

namespace ECount.Infrustructure.Data.Integration
{
    [Serializable]
    public class ValidationResult
    {
        private readonly string _key;
        private readonly string _message;

        public ValidationResult(string message, string key)
        {
            _message = message;
            _key = key;
        }

        public string Key
        {
            get { return _key; }
        }

        public string Message
        {
            get { return _message; }
        }
    }
}