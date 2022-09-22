using System;

namespace PartnerPortal.Repository.Infrastructure
{
    public interface IDatabaseFactory : IDisposable
    {
        DataContext Get();
    }
}
