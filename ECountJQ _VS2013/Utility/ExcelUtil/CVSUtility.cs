//----------------------------------------------------------------------------------------------------
//Name:     ImportUtiltiy
//Function: Help to split the string form CSV file
//Author:   Jun Xing
//Date:     8/6/2008
//----------------------------------------------------------------------------------------------------
//Change History:
// Date         Who         Changes Made
//----------------------------------------------------------------------------------------------------
// 8/6/2008     Jun Xing     Create
// 8/7/2008     Jun Xing     Add a new function checkExportCSV for Export
// 8/7/2008     Jun Xing     Modify the function SplitCSV to return two-dimension array
//----------------------------------------------------------------------------------------------------
using System.Collections;

namespace SGM.ECount.Utility
{
    public class CSVUtiltiy
    {

        /// <summary>
        /// To split the string form CSV file
        /// </summary>
        /// <param name="csvString">String form CSV file, all line of the file</param>
        /// <returns>String array list</returns>
        public static string[][] SplitCSV(string csvString)
        {
            ArrayList CSVList = new ArrayList();
            ArrayList CSVListLine = new ArrayList();

            int indexOfColon = 0;
            int Counter = 0;
            string getString = csvString + ",";

            // To check every char in the string
            foreach (char subStr in getString)
            {
                // If char is a colon, mark it in the counter
                if (subStr == '\"')
                    indexOfColon++;
                // If there are double colon and there is a comma or a enter, that is a field
                if (indexOfColon % 2 == 0 && (subStr == ',' || subStr == '\n'))
                {
                    string subString = getString.Substring(0, Counter);

                    // If the string is not empty and there is a enter and a wrap at the end of it,remove the wrap
                    if (subString != "")
                    {
                        if (subString.Substring(subString.Length - 1, 1) == "\r" && subStr == '\n')
                        {
                            subString = subString.Substring(0, subString.Length - 1);
                        }
                    }
                    // If there a colon in the begining of the string , that must be a string include colon or comma. We must remove the colon on both sides of the string.
                    if (subString.IndexOf("\"") == 0)
                    {
                        subString = subString.Substring(1, subString.Length - 2);
                    }

                    // To replace the colon in the field as actual. If there is a colon, CSV will saved as double.
                    CSVListLine.Add(subString.Replace("\"\"", "\""));
                    getString = getString.Substring(Counter + 1, getString.Length - Counter - 1);
                    Counter = -1;

                    if (subStr == '\n')
                    {
                        CSVList.Add(CSVListLine);
                        CSVListLine = new ArrayList();
                    }
                }
                Counter++;
            }

            // To add the last line
            if (CSVListLine.Count > 1)
                CSVList.Add(CSVListLine);

            // To copy the arrylist to a string list
            string[][] strArray = new string[CSVList.Count][];
            for (int i = 0; i < CSVList.Count; i++)
            {
                strArray[i] = new string[((ArrayList)CSVList[i]).Count];
                ((ArrayList)CSVList[i]).CopyTo(strArray[i]);
            }
            return strArray;
        }

        /// <summary>
        /// To check the string for Export the csv file
        /// </summary>
        /// <param name="csvString">A field for Export</param>
        /// <returns></returns>
        public static string checkExportCSV(string csvString)
        {
            string ResultStr = csvString;
            // To replace a colon to tow colons
            ResultStr = ResultStr.Replace("\"", "\"\"");

            // To add colons on both side of the field and add a comma at the end of the field
            ResultStr = "\"" + ResultStr + "\",";

            return ResultStr;
        }
    }
}