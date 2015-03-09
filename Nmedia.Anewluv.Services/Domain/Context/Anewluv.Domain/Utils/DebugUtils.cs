using Anewluv.Domain.Migrations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anewluv.Domain.Utils
{
    public class Utils
    {

        public static void SeedDebug(AnewluvContext context)
        {
            SeedMethods.seedgenerallookups(context);
        }
    }
}
