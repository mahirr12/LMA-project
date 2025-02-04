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
        private DateTime _createdTime;
        public DateTime CreatedTime
        {
            get => _createdTime;
            set
            {
                _createdTime = value;
                UpdatedTime = value;
            }
        }
        public DateTime UpdatedTime { get; set; }
        public BaseEntity()
        {
            UpdatedTime = CreatedTime;
        }
    }
}
