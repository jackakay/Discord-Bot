using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Banshi.Searching
{
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 
    public class Definition
    {
        public string definition { get; set; }
        public string partOfSpeech { get; set; }
    }

    public class Root
    {
        public string word { get; set; }
        public List<Definition> definitions { get; set; }
    }






}
