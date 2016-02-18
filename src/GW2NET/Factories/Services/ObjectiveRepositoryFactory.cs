using GW2NET.Worlds;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using GW2NET.Common;

namespace GW2NET.Factories.V2
{
    public class ObjectiveRepositoryFactory : RepositoryFactoryBase<IWorldRepository>
    {
        private readonly IServiceClient serviceClient;

        public ObjectiveRepositoryFactory(IServiceClient serviceClient)
        {
            if (serviceClient == null)
            {
                throw new ArgumentNullException("serviceClient");
            }

            this.serviceClient = serviceClient;
        }

        public override IWorldRepository ForCulture(CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        public override IWorldRepository ForDefaultCulture()
        {
            throw new NotImplementedException();
        }
    }
}
