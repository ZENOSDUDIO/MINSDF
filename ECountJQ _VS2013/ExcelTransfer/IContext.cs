using System.Collections;

namespace ECount.Infrustructure
{
    internal interface IContext
    {
        IDictionary State { get; set; }
    }
}