using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternalTraining
{
    public class Class1
    {
        public static void Main(String[] args)
        {

            //Console.WriteLine(RomanNumbers.convertRomanToDecimal("MCMXLIV"));
            //Console.WriteLine(RomanNumbers.convertRomanToDecimal("DXXI"));
            //Console.WriteLine(RomanNumbers.convertRomanToDecimal("CMXCII"));
            //Console.WriteLine(RomanNumbers.convertRomanToDecimal("VI"));
            InterGalatic i = new InterGalatic();
            i.Run();

        }
    }
    public class InterGalatic
    {
        Dictionary<string, char> interGalaticConstants;
        Dictionary<string, float> specialNames;
        List<String> outputList = new List<string>();
        public void Run()
        {
            TakeInput();
            //print output
            foreach (string val in outputList)
                Console.WriteLine(val);
        }
        private void TakeInput()
        {
            string inputLine = Console.ReadLine();
            interGalaticConstants = new Dictionary<string, char>();
            specialNames = new Dictionary<string, float>();
            while (inputLine != null && inputLine.Length>0)
            {  
                inputLine = inputLine.Trim();
                inputLine=inputLine.ToLower();
                string[] stringArray = inputLine.Split(" ");
                char[] charArray = inputLine.ToCharArray();
                if (inputLine.Contains("how much"))
                {   // to handle how much is pish tegj glob glob ?
                    findValueOfGalaticConstantsEquation(stringArray);
                }
                else if (inputLine.Contains("how many"))
                {   //to handle how many Credits is glob prok Silver ?
                    findNItemsValue(stringArray);
                }
                else if(inputLine.Contains("is") && inputLine.Contains("credits"))
                {   //to handle glob glob Silver is 34 Credits
                    setValueOfItem(stringArray);
                }
                else
                {   //to handle glob is I
                    setValueOfInterGalaticConstant(stringArray, charArray);   
                }
                inputLine = Console.ReadLine();
            }
            
        }
        private void findValueOfGalaticConstantsEquation(string[] stringArray)
        {
            string output = "";
            int value = getValue(stringArray, 3, stringArray.Length - 2);
            if (value == 0)
            {
                outputList.Add("I dont know anything about this");
                return;
            }
                
            for (int i = 3; i < stringArray.Length-1; i++)
            {
                output+=stringArray[i]+" ";
            }
            
            output +="is "+ value;
            outputList.Add(output);
        }
        private void findNItemsValue(string[] stringArray)
        {
            double value = getValue(stringArray, 4, stringArray.Length - 3) * specialNames.GetValueOrDefault(stringArray[stringArray.Length - 2]);
            //Console.WriteLine("value per item is"+ specialNames.GetValueOrDefault(stringArray[stringArray.Length - 2]));
            if (value == 0)
            {
                outputList.Add("I dont know anything about this");
                return;
            }
                
            string output = "";
            for (int i = 4; i < stringArray.Length - 1; i++)
            {
                output += stringArray[i] + " ";
            }
            output += "is " + value + " credits";
            outputList.Add(output);
            
        }
        private void setValueOfItem(string[] stringArray)
        {
            int i = 0;
            for (i = 0; i < stringArray.Length; i++)
            {
                int value = 0;
                if (!interGalaticConstants.ContainsKey(stringArray[i]))
                {
                    value = getValue(stringArray, 0, i - 1);
                    specialNames.Add(stringArray[i], int.Parse(stringArray[stringArray.Length - 2]) / (float)value);
                    break;
                }

            }
        }
        private void setValueOfInterGalaticConstant(string[] stringArray,char[] charArray)
        {
            int count = 0;
            string constantName = stringArray[0];

            interGalaticConstants.Add(constantName, charArray[charArray.Length-1]);
        }
        private int getValue(string[] stringArray, int startValue,int endValue)
        {
            string Roman= convertConstantToRoman(stringArray, startValue,endValue);
            if (Roman.Length == 0)
                return 0;
            return RomanNumbers.convertRomanToDecimal(Roman);
        }
        private string convertConstantToRoman(string []  stringArray,int startvalue,int endValue)
        {
            string Roman="";
            for(int i=startvalue;i<=endValue; i++)
            {
                if (!interGalaticConstants.ContainsKey(stringArray[i]))
                    return "";
                Roman+=interGalaticConstants.GetValueOrDefault(stringArray[i]).ToString();
            }
            return Roman;
        }
    }
     
    public static class RomanNumbers
    {
        static Dictionary<char, int> romanValues = new Dictionary<char, int>{
                                        { 'i',1 },
                                        { 'v',5 },
                                        { 'x',10 },
                                        { 'l',50 },
                                        { 'c',100 },
                                        { 'd',500 },
                                        { 'm',1000 },
                                        };
        private static int valueForRoman(char c)
        {
            return romanValues[c];
        }
        public static int convertRomanToDecimal(String s)
        {
            int value = 0;
            char[] stringArray = s.ToCharArray();
            for(int i = 0; i < s.Length; i++)
            {
                char currentChar = stringArray[i];
                if (i < s.Length-1)
                {
                    if (romanValues.GetValueOrDefault(stringArray[i+1])> romanValues.GetValueOrDefault(currentChar))
                    {
                        value += romanValues.GetValueOrDefault(stringArray[i + 1])- romanValues.GetValueOrDefault(currentChar);
                        i++;
                        continue;
                    }
                }
                value += romanValues.GetValueOrDefault(currentChar);
            }
            return value;
        }
    }

}
