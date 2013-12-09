using System;
using System.Collections.Generic;

namespace Anewluv.Domain.Data
{
    public partial class lu_notetype
    {
        public lu_notetype()
        {
            this.abusereportnotes = new List<abusereportnote>();
            this.blocknotes = new List<blocknote>();
        }

        public int id { get; set; }
        public string description { get; set; }
        public virtual ICollection<abusereportnote> abusereportnotes { get; set; }
        public virtual ICollection<blocknote> blocknotes { get; set; }
    }
}
