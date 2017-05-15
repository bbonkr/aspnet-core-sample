using System;
using System.Collections.Generic;
using System.Text;

namespace SampleMvc.Identity.Models
{
    public class Role
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string NormalizedRoleName { get; set; }

        public string Description { get; set; }
    }
}
