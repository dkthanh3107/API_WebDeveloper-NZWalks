using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MyNZWalks.API.Models.Domain
{
    public class Difficulty
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}
