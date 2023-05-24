using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParserPlanBastard.Models.DTO
{
	public class DTOXmlRepresentation
	{
		public string ShortName { get; set; }
		public string FullName { get; set; }
		public List<Models.Attribute> Attribute { get; set; }

		public DTOXmlRepresentation(string shortName, string fullName, List<Models.Attribute> attribute)
		{
			ShortName = shortName;
			FullName = fullName;
			Attribute = attribute;
		}
	}
}
