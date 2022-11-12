using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UMan.DataAccess.Entities
{
    public class HashTag
    {
        public int Id { get; set; }

        public string Title { get; set; } = string.Empty;

        public Paper Paper { get; set; }
    }
}
