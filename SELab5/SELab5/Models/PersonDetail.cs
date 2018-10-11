using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SELab5.Models;
namespace SELab5.Models
{
    public class PersonDetail
    {
        public Person person {get;set;}
        List<Contact> listContact { get; set; }
    }
}