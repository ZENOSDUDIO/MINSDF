using System;

namespace ECount.Infrustructure.Data.Integration
{
    [Flags]
    public enum IntegrationMode
    {
        /// <summary>
        /// Create data schema from the storage based on sample data
        /// </summary>
        CreateSchema = 0x01,
        /// <summary>
        /// Get data schema from the storage
        /// </summary>
        GetSchema = 0x02,
        /// <summary>
        /// Save data schema to the storage
        /// </summary>
        SaveSchema = 0x04,
        /// <summary>
        /// Get data from source
        /// </summary>
        GetData = 0x08,
        /// <summary>
        /// Transfer data to the destination storage
        /// </summary>
        TransferData = 0x10,
    } ;
}