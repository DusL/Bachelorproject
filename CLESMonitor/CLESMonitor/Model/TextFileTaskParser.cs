using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace CLESMonitor.Model
{
    class TextFileTaskParser
    {

        public void TextFileTaskParser()
        {
            try
            {
                using (StreamReader sr = new StreamReader("TestInput.txt"))
                {
                    String line = sr.ReadToEnd();
                    Console.WriteLine(line);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(e.Message);
            }
        }

        
    }
}
