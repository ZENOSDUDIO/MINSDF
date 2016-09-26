using System;
using System.IO;
using System.Xml.Serialization;

namespace ECount.Infrustructure.Data.Integration
{
    internal abstract class StorageProviderBase : IProvider
    {
        private int _perTransferCount;

        protected bool isCompleted;
        protected string name;

        public StorageProviderBase(string providername)
        {
            name = providername;
        }

        #region IProvider Members

        public int PerTransferCount
        {
            get { return _perTransferCount; }
            set { _perTransferCount = value; }
        }

        public bool IsCompleted
        {
            get { return isCompleted; }
        }

        public virtual string Name
        {
            get { return name; }
            set { name = value; }
        }

        public virtual void Run(IContext context)
        {
            IntegrationContext context1 = (IntegrationContext)context;
            IntegrationMode mode;
            try
            {
                mode = (IntegrationMode)context.State[name];
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("cannot get transfer action for provider [{0}]", name), ex);
            }
            if ((mode & IntegrationMode.CreateSchema) == IntegrationMode.CreateSchema)
                CreateDataSchema(context1);
            if ((mode & IntegrationMode.SaveSchema) == IntegrationMode.SaveSchema)
                SaveDataSchema(context1);
            if ((mode & IntegrationMode.GetSchema) == IntegrationMode.GetSchema)
                GetDataSchema(context1);
            if ((mode & IntegrationMode.GetData) == IntegrationMode.GetData)
                GetData(context1);
            //ValidateData(context1);
            if ((mode & IntegrationMode.TransferData) == IntegrationMode.TransferData &&
                ((context1.HaltTransferWhenHasSkippedData && context1.SkippedCount == 0) ||
                 !context1.HaltTransferWhenHasSkippedData))
                TransferData(context1);

            if (PerTransferCount <= 0)
                context1.Status = IntegrationStatus.Success;
        }

        #endregion

        public event DataItemValidationHandler OnValidating;

        internal virtual void CreateDataSchema(IntegrationContext context)
        {
            throw new NotImplementedException();
        }

        internal virtual void SaveDataSchema(IntegrationContext context)
        {
            string targetPath = context.State[ContextState.SchemaFilePath] as string;
            if (string.IsNullOrEmpty(targetPath))
                throw new ArgumentNullException(string.Format("argument [{0}] missed!", ContextState.SchemaFilePath));

            using (TextWriter writer = new StreamWriter(targetPath))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(DataSchema));
                serializer.Serialize(writer, context.Schema);
                writer.Close();
            }
        }

        internal virtual void GetDataSchema(IntegrationContext context)
        {
            string targetPath = context.State[ContextState.SchemaFilePath] as string;
            context.Schema = ValidationUtils.GetDataSchemaFromFile(targetPath);
            if (context.Schema == null)
                throw new Exception(string.Format("can not parsing schema based on [{0}]", targetPath));
        }

        internal virtual void GetData(IntegrationContext context)
        {
            throw new NotImplementedException();
        }

        internal virtual void TransferData(IntegrationContext context)
        {
            throw new NotImplementedException();
        }

        protected void ValidateData(IntegrationContext context)
        {
            foreach (DataItem item in context.Data)
            {
                if (OnValidating != null)
                {
                    DataItemValidationHandler handler = (DataItemValidationHandler)OnValidating.Clone();
                    handler.Invoke(item, new DataItemValidationArgs(item.ValidationResults));

                    if (!item.IsValid)
                    {
                        context.SkippedData.Add(item);
                        context.Data.Remove(item);
                    }
                }
            }

            // TODO: release the event handler
        }
    }
}