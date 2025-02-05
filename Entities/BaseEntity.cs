using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project___ConsoleApp__Library_Management_Application_.Entitys
{
    public class BaseEntity
    {
        public int Id { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreatedTime { get; set; }
        public DateTime UpdatedTime { get; set; }
        public BaseEntity()
        {
            CreatedTime = DateTime.UtcNow.AddHours(4);
            UpdatedTime = CreatedTime;
        }
    }
}
