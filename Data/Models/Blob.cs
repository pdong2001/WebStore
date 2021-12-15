using System;
using System.ComponentModel.DataAnnotations;

namespace Data.Models
{
    public class Blob : Entity
    {
        public string Name { get; set; }

        public byte[] Content { get; set; }
    }
}
