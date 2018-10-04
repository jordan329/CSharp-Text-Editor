using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace jslz85Assignment4
{
    [Serializable]
    class TextDocument
    {

        public string content;
        public string Content { get; set; } 
            public TextDocument()
            {

            }
            public TextDocument(string input)
            {
                content = input;
            }
            public void save()
            {

            }
            public void saveAs()
            {

            }
            public void open()
            {

            }
    }
}