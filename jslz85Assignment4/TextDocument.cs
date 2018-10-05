using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
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
            public void save(String filePath)
            {
            TextWriter txt = new StreamWriter(filePath);
            try
            {
            txt.Write(this.content);    
            }
            catch
            {
                System.Windows.MessageBox.Show("An error occured");
            }
            txt.Close();
        }
            public void saveAs()
            {

            }
            public void open(String filePath)
            {
                StreamReader txt = new StreamReader(filePath);
            try
            {
                this.content = txt.ReadToEnd();
            }
            catch
            {
                System.Windows.MessageBox.Show("An error occured");
            }
            txt.Close();
        }
    }
}