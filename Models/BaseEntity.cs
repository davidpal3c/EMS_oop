using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.Models
{
    public abstract class BaseEntity
    {
        private int _id;
        private DateTime _createdAt;
        private DateTime _updatedAt;


        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }   

        public DateTime CreatedAt
        {
            get { return _createdAt; }
            set { _createdAt = value; }
        }   

        public DateTime UpdatedAt
        {
            get { return _updatedAt; }
            set { _updatedAt = value; }
        }


        public BaseEntity() 
        {
            CreatedAt = DateTime.Now;
            UpdatedAt = DateTime.Now;
        }

        public BaseEntity(int id, DateTime createdAt, DateTime updatedAt)
        {
            Id = id;  
            CreatedAt = createdAt; 
            UpdatedAt = updatedAt; 
                       
        }

        public abstract override string ToString(); 

    }
}
