 
using System.Linq; 

namespace wikix
{
    class WikiPage
    {
        public string title;
        public string text;
    }

    class Program
    {

        static string formatCounter(System.Int64 counter)
        {
            return System.String.Format("{0:n}", counter).Replace(",00", "");
        }

        static bool isMatch(string title, string[] regularExpressionList)
        {
            bool result = true;
            foreach (string regex in regularExpressionList)
            {
                result = System.Text.RegularExpressions.Regex.IsMatch(title, regex, System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                if (result)
                {
                    break;  
                } 
            }

            return result;
        }

        static void processWikiXMLDump(string sourceFilename, string targetFilename, string[] regexList)
        {
            System.IO.StreamWriter sw = System.IO.File.CreateText(targetFilename);
            System.Int64 pageCounter = 0;
            System.Int64 foundCounter = 0;
            sw.WriteLine("[");

            WikiPage p = new WikiPage();
            System.Console.WriteLine("Processing " + System.IO.Path.GetFileName(sourceFilename) + " to write data to " + System.IO.Path.GetFileName(targetFilename) + "");
            System.Console.WriteLine("Stay tuned. This could take a few minutes...");
            using (System.Xml.XmlReader reader = System.Xml.XmlReader.Create(sourceFilename))
            {
                while (reader.Read())
                {
                    // Only detect start elements.
                    if (reader.IsStartElement())
                    {
                        // Get element name and switch on it.
                        switch (reader.Name)
                        {
                            case "page":
                                pageCounter++;
                                // Detect this element.
                                // Console.WriteLine(reader.Name);
                                string title = "";
                                if (reader.ReadToDescendant("title"))
                                {
                                    title = reader.ReadElementContentAsString("title", reader.NamespaceURI);
                                }

                                if (isMatch(title, regexList))
                                {
                                    if (reader.ReadToNextSibling("revision"))
                                    {
                                        if (reader.ReadToDescendant("text"))
                                        {
                                            foundCounter++;
                                            string text = reader.ReadElementContentAsString("text", reader.NamespaceURI);
                                            p.title = title;
                                            p.text = text;
                                            string json = Newtonsoft.Json.JsonConvert.SerializeObject(p, Newtonsoft.Json.Formatting.None);
                                            sw.WriteLine(json + ",");
                                            sw.Flush();
                                            if (foundCounter % 100 == 0)
                                            {
                                                System.Console.WriteLine(formatCounter(foundCounter) + " pages found. Currently \"" + title + "\" at #" + formatCounter(pageCounter) + "");
                                            }
                                        }
                                    }


                                }
                                if (pageCounter % 100000 == 0)
                                {
                                    System.Console.WriteLine("Crawled " + formatCounter(pageCounter) + " and found " + formatCounter(foundCounter) + " pages...");
                                }
                                break;
                        }
                    }
                }
                sw.WriteLine("]");
            }
            System.Console.WriteLine("flushing file contents...");
            sw.Flush();
            System.Console.WriteLine("Crawled " + formatCounter(pageCounter) + " pages. done."); 
        }



        static void Main(string[] args)
        {
            System.Diagnostics.Stopwatch stopWatch = new System.Diagnostics.Stopwatch();
            stopWatch.Start(); 
            string sourceFilename = args[0]; // "c:\\git\\wiki\\data-dumps\\enwiki-20170420-pages-articles.xml";
            string targetFilename = args[1]; // "c:\\git\\wiki\\data-dumps\\enwiki-20170420-pages-articles.json";
            System.Collections.Generic.List<string> regexList = new System.Collections.Generic.List<string>();

            for (int i = 2; i < args.Count()  ; i++)
            {
                regexList.Add(args[i]);
            }

            try
            { 
                if (System.IO.File.Exists(targetFilename))
                {
                    throw new System.Exception(targetFilename + " already exists.");
                }
                processWikiXMLDump(sourceFilename, targetFilename, regexList.ToArray());
            }
            catch (System.Exception exception)
            {
                System.Console.BackgroundColor = System.ConsoleColor.Red;
                System.Console.ForegroundColor = System.ConsoleColor.White;
                System.Console.WriteLine("Error: " + exception.Message);
                System.Environment.Exit(1);
            }

            stopWatch.Stop(); 
            System.TimeSpan ts = stopWatch.Elapsed; 
            string elapsedTime = System.String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                ts.Hours, ts.Minutes, ts.Seconds,
                ts.Milliseconds / 10);
            System.Console.ForegroundColor = System.ConsoleColor.Green;
            System.Console.WriteLine("RunTime " + elapsedTime); 
        }
    }
}
