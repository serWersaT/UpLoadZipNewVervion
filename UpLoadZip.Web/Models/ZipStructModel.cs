using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UpLoadZip.Web.Models
{
    public class ZipStructModel
    {
        public string FileName { get; set; }
        public Dictionary<string, int> Acrchive { get; set; }
    }
}