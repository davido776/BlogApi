using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Blog.Models
{
    public class Connection
    {
       
        public string Id { get; set; }
  
        [ForeignKey("RequestedBy")]
        public virtual string RequestById { get; set; }
       
        public   virtual AppUser RequestBy{ get; set; }
        public  virtual AppUser RequestTo { get; set; }
        public ConnectionStatus connectionStatus { get; set; }

       
        public bool Approved => connectionStatus == ConnectionStatus.Approved;

        public enum ConnectionStatus
        {
            Pending,
            Approved,
            Rejected,

        };
    }
}
