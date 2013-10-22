using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Anewluv.DataAccess.ExtentionMethods
{
    class profileextentionmethods
    {

        public static IEnumerable<promotionobject> getpromotionobjectsbypromotionobjecttype(this IRepository<promotionobject> repo, promotionobjecttypeenum type)
        {


            return repo.Find().OfType<promotionobject>().Where(p => p.promotionobjecttype.id == (int)type);
        }
    }
}
