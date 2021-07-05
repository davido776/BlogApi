using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Blog.Models
{
    public class Connection
    {
       
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }
  
        [ForeignKey("RequestedBy")]
        public string RequestById { get; set; }
       
        public   AppUser RequestBy{ get; set; }

        public  string RequestToId { get; set; }
        public   AppUser RequestTo { get; set; }
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
