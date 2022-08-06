using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UMan.Core
{
    public class Paper
    {
        public List<Article> Articles { get; set; } = new List<Article>();

        public DateTime Created { get; set; }

        public Author? Author { get; set; }
    }
}
