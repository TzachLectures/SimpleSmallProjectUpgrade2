using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SimpleSmallProjectUpgrade2.Models
{
    [Serializable]
    public class Person
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public string Location { get; set; }
    }
}