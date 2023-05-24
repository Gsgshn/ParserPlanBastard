﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParserPlanBastard.Models
{
    public interface IXmlElement
    {
        public string ShortName { get; set; }
        public string FullName { get; set; }
        public string? Value { get; set; }
    }
}
