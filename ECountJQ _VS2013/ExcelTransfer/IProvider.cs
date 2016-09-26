namespace ECount.Infrustructure
{
    internal interface IProvider
    {
        int PerTransferCount { get; set; }
        bool IsCompleted { get; }
        string Name { get; set; }
        void Run(IContext context);
    }
}