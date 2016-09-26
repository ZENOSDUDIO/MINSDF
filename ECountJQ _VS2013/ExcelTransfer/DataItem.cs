using System;
using System.Collections.Generic;
using System.Text;

namespace ECount.Infrustructure.Data.Integration
{
    public class DataItem
    {
        private bool _hasBeenValid;
        public DataItemCollection Container;
        public List<DataItemField> Items = new List<DataItemField>();
        public int RowIndex;
        public DataSchema Schema;

        internal ValidationResults ValidationResults = new ValidationResults();

        public DataItem(DataSchema schema)
        {
            Schema = schema;
            if (schema != null)
                Array.ForEach(schema.Fields, delegate(DataSchemaField field) { Items.Add(new DataItemField(field)); });
        }

        public bool IsValid
        {
            get
            {
                DoValidate();
                return ValidationResults.IsValid;
            }
        }

        public string ValidationMessage
        {
            get
            {
                if (ValidationResults == null)
                    DoValidate();
                if (ValidationResults.IsValid)
                    return string.Empty;
                StringBuilder sb = new StringBuilder();
                foreach (ValidationResult result in ValidationResults)
                {
                    sb.Append(result.Message);
                }
                return sb.ToString();
            }
        }

        public DataItemField this[string fieldName]
        {
            get
            {
                DataItemField field = GetField(fieldName);
                return field;
            }
        }

        public event DataItemValidationHandler OnValidating;

        internal void DoValidate()
        {
            if (!_hasBeenValid)
            {
                Items.ForEach(delegate(DataItemField item) { if (!item.Skip) item.DoValidate(ValidationResults); });
                if (OnValidating != null)
                    OnValidating(this, new DataItemValidationArgs(ValidationResults));

                _hasBeenValid = true;
            }
        }

        public DataItemField GetField(string name)
        {
            if (Schema != null && (Items.Count == 0))
                Array.ForEach(Schema.Fields, delegate(DataSchemaField field) { Items.Add(new DataItemField(field)); });
            return Items.Find(delegate(DataItemField f)
            {
                return f.Name.Equals(name,
                                     StringComparison.
                                        InvariantCultureIgnoreCase);
            });
        }

        /// <summary>
        ///  
        /// </summary>
        /// <param name="sourcename"></param>
        /// <returns></returns>
        /// <remarks>
        ///  if there is no [source] attribute value, then use [name] attribute
        /// </remarks>
        public DataItemField GetSourceField(string sourcename)
        {
            return Items.Find(delegate(DataItemField f)
            {
                if (string.IsNullOrEmpty(f.Source))
                    return f.Name.Equals(sourcename, StringComparison.InvariantCultureIgnoreCase);
                else
                    return f.Source.Equals(sourcename, StringComparison.InvariantCultureIgnoreCase);
            });
        }

        /// <summary>
        ///  
        /// </summary>
        /// <param name="destinationname"></param>
        /// <returns></returns>
        /// <remarks>
        /// if there is no [Destination] attribute value, then use [name] attribute
        /// </remarks>
        public DataItemField GetDestinationField(string destinationname)
        {
            return Items.Find(delegate(DataItemField f)
            {
                if (string.IsNullOrEmpty(f.Destination))
                    return f.Name.Equals(destinationname,
                                         StringComparison.
                                            InvariantCultureIgnoreCase);
                else
                    return f.Destination.Equals(destinationname,
                                                StringComparison.
                                                    InvariantCultureIgnoreCase);
            });
        }

        public override string ToString()
        {
            string[] properties = new string[Items.Count];
            for (int i = 0; i < Items.Count; i++)
            {
                properties[i] = string.Format("{0}:{1}{2}", Items[i].Name, Items[i].Value, Environment.NewLine);
            }
            return string.Concat(properties);
        }
    }
}