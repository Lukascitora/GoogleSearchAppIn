using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace GoogleSearchApp.Shared.Models
{
    
    [XmlRoot("ResultItem")]
    public class ResultItem
    {
        [XmlElement("Title")]
        public string Title { get; set; }

        [XmlElement("Link")]
        public string Link { get; set; }

        [XmlElement("Snippet")]
        public string Snippet { get; set; }
    }


    public class GoogleSearchResponse
    {
        public List<ResultItem> Items { get; set; }
    }
}
