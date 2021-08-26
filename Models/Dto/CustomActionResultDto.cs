using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace books.Models.Dto
{
    public class CustomActionResultDto
    {
        public Exception Exception { get; set; }
        public Publisher Publisher { get; set; }
    }
}
