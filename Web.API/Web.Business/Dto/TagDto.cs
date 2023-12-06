using System;
using Web.Business;
using System.Linq;
using Web.Business.Models;
using System.Collections.Generic;
using Web.Business.IServices;

namespace Web.Business.Dto
{
    public class TagDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}