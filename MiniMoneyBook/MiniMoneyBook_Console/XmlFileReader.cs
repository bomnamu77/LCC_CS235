using System.Collections.Generic;
using System.Xml;
using System.IO;

namespace Lab7.TidePrediction_Console
{
    public class XmlTideFileParser
    {
        // constants to use for XML element names and dictionary keys
        public const string ITEM = "item";   // this element holds the ones below
        public const string DATE = "date";
        public const string DAY = "day";
        public const string TIME = "time";
        public const string HEIGHT = "pred_in_ft";   
        public const string HI_LOW = "highlow";
        public const int NUM_FIELDS = 5; //set number of fields

        // This list will be filled with string Array objects
        List<string[]> rows = new List<string[]>();
         
        
        public List<string[]> TideList { get { return rows; } }

        public XmlTideFileParser(Stream xmlStream)
        {
                      
            
            // Parse the xml file and fill the list of Array objects with tide data
            using (XmlReader reader = XmlReader.Create(xmlStream))
            {
                string[] tempArray = null;
                while (reader.Read())
                {
                    // Only detect start elements.
                    if (reader.IsStartElement())
                    {
                        // Get element name and switch on it.
                        switch (reader.Name)
                        {
                            case ITEM:
                                // New word
                                tempArray = new string[NUM_FIELDS];
                                
                                break;
                            case DATE:
                                // Add date
                                
                                if (reader.Read() && tempArray != null)
                                {
                                    string temp = reader.Value.Trim();

                                    tempArray[0] = temp;
                                    
                                }
                                break;
                            case DAY:
                                // Add day of the week
                                if (reader.Read() && tempArray != null)
                                {
                                    tempArray[1] = reader.Value.Trim();
                                    
                                }
                                break;
                            case TIME:
                                // Add time of day
                                if (reader.Read() && tempArray != null)
                                {
                                    tempArray[2] = reader.Value.Trim();
                                    
                                }
                                break;
                            case HEIGHT:
                                // Add tide height in inches
                                if (reader.Read() && tempArray != null)
                                {
                                    tempArray[3] = reader.Value.Trim();
                                }
                                break;
                            case HI_LOW:
                                // Add H or L for high or low tied
                                if (reader.Read() && tempArray != null)
                                {
                                    tempArray[4] = reader.Value.Trim(); ;
                                }
                                break;
                        }
                    }
                    else if (reader.Name == ITEM)
                    {
                        // reached </item>
                        rows.Add(tempArray);

                        tempArray = null;
                    }

                }
            }

        }
    }
}

/**** Sample XML ****

<item>
    <date>2018/01/01</date>
    <day>Mon</day>
    <time>12:16 AM</time>
    <pred_in_ft>6.08</pred_in_ft>
    <pred_in_cm>185</pred_in_cm>
    <highlow>H</highlow>
</item>

*/