using System.ComponentModel.DataAnnotations.Schema;

namespace FinalProjectMVC.Models
{
    public class Target
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Position { get; set; }

        public Location Location { get; set; }
        public int x { get; set; } = 0;

        public int y { get; set; } = 0;
       
        public string PhotoUrl { get; set; }


        public string Status { get; set; }

        public Target() { }
    }
}
