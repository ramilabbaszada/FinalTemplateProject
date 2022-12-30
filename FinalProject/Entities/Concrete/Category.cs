
using Core.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Concrete
{
    // Ciplak Class Qalmasin !!!
    public class Category: IEntity
    {
        public int CategoryID { get; set; }
        public string CategoryName { get; set; }
    }
}
