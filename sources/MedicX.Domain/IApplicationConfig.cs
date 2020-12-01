using System.Collections.Generic;

namespace DustInTheWind.MedicX.Domain
{
    public interface IApplicationConfig
    {
        IEnumerable<string> StartUpOpenArchives { get; }
    }
}