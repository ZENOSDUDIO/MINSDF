using System;
using System.Collections;
using System.Collections.Generic;

namespace ECount.Infrustructure.Data.Integration
{
    [Serializable]
    public class ValidationResults : IEnumerable<ValidationResult>
    {
        private readonly List<ValidationResult> _validationResults;

        public ValidationResults()
        {
            _validationResults = new List<ValidationResult>();
        }

        public bool IsValid
        {
            get { return _validationResults.Count == 0; }
        }

        #region IEnumerable<ValidationResult> Members

        IEnumerator<ValidationResult> IEnumerable<ValidationResult>.GetEnumerator()
        {
            return _validationResults.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _validationResults.GetEnumerator();
        }

        #endregion

        public void AddResult(ValidationResult validationResult)
        {
            _validationResults.Add(validationResult);
        }

        public void AddAllResults(IEnumerable<ValidationResult> sourceValidationResults)
        {
            _validationResults.AddRange(sourceValidationResults);
        }
    }
}