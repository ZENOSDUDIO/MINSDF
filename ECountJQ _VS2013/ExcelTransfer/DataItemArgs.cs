using System;

namespace ECount.Infrustructure.Data.Integration
{
    public delegate void DataItemValidationHandler(DataItem sender, DataItemValidationArgs args);

    public class DataItemValidationArgs : EventArgs
    {
        private readonly ValidationResults _validationResults;

        public DataItemValidationArgs(ValidationResults validationResults)
        {
            _validationResults = validationResults;
        }

        public ValidationResults ValidationResults
        {
            get { return _validationResults; }
        }
    }
}