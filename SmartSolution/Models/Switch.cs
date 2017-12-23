using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebPortal.Models
{
    public class Switch
    {
        [DataType(DataType.Text)]
        public string Id { get; set; }

        [DataType(DataType.Text)]
        [Required(AllowEmptyStrings = false)]
        public string Name { get; set; }

        public bool State { get; set; }
    }
}
