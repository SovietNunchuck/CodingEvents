using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CodingEvents.ViewModels
{
    public class AddEventViewModel
    {
        [Required(ErrorMessage = "Required field")]
        [StringLength(20, MinimumLength = 3, ErrorMessage = "Event name must be between 3 and 20 characters long.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Required field")]
        [StringLength(500, ErrorMessage = "Description is too long.")]
        public string Description { get; set; }

        [EmailAddress(ErrorMessage = "Please enter a valid email address.")]
        public string ContactEmail { get; set; }


    }
}
