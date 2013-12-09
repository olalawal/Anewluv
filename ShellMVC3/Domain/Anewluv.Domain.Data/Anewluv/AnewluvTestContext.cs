using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using Anewluv.Domain.Data.Mapping;

namespace Anewluv.Domain.Data
{
    public partial class AnewluvTestContext : DbContext
    {
        static AnewluvTestContext()
        {
            
        }

        public AnewluvTestContext()
            : base("Name=AnewluvContext")
        {
        }

       

        
    }
}
