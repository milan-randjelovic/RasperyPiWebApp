using System;
using System.ComponentModel.DataAnnotations;

namespace WebPortal.Models
{
    public class Device
    {
        [DataType(DataType.Text)]
        public string Id { get; set; }

        [DataType(DataType.Text)]
        [Required(AllowEmptyStrings = false)]
        public string Name { get; set; }

        [DataType(DataType.Text)]
        public string DeviceType { get; set; }

        public bool IsActive { get; set; }

        public Device()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Name = "";
            this.DeviceType = this.GetType().ToString();
            this.IsActive = false;
        }
    }
}