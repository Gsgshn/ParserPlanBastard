using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ParserPlanBastard.Models.Entities
{
    public class User : IdentityUser<int>
    {
       public bool IsDeleted { get; set; }

        

        public ICollection<File> Files { get; set; }
        public ICollection<Logging> Loggings { get; set; }
    }
}
