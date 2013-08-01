using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Shell.MVC2.Infrastructure.Interfaces;
using Shell.MVC2.Infrastructure.Entities.UserRepairModel;
using Shell.MVC2.Services.Contracts;
using System.ServiceModel.Activation;
using System.ServiceModel;

namespace Shell.MVC2.Services.Logging
{
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall, ConcurrencyMode = ConcurrencyMode.Multiple)]
    public class UserRepairService : IUserRepairService 
    {
        private IUserRepairRepository _userrepairrepository;

        public UserRepairService(IUserRepairRepository userrepairrepository)
        {
            _userrepairrepository = userrepairrepository;
        }


        public int WriteCompleteLogEntry(UserRepairLog log)
        {

            return _userrepairrepository.WriteCompleteLogEntry(log);
        }
    }
}
