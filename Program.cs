using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text.RegularExpressions;
using System.Text;
using System;




class Result
{

    /*
     * Complete the 'chooseFlask' function below.
     *
     * The function is expected to return an INTEGER.
     * The function accepts following parameters:
     *  1. INTEGER_ARRAY requirements
     *  2. INTEGER flaskTypes
     *  3. 2D_INTEGER_ARRAY markings
     */

    public static int chooseFlask(List<int> requirements, int flaskTypes, List<List<int>> markings)
    {
        int flaskId = -1;
        int lastFlaskVisited = -1;
        int maxRequirement = requirements.Max();

        Dictionary<int, List<int>> markingsDict = new Dictionary<int, List<int>>(); 
        Dictionary<int, int> maxMarkingDict = new Dictionary<int, int>(); 

        for(int i=0;i<markings.Count();i++){
            int index = markings[i][0];
            int value = markings[i][1];
            if(index == lastFlaskVisited){
                List<int> valueList = markingsDict[index];
                valueList.Add(value);
                markingsDict[index] =  valueList;
            }
            else{
                lastFlaskVisited=index;
                List<int> valueList = new List<int>();
                valueList.Add(value);
                markingsDict.Add(index, valueList);
            }
        }


        foreach(KeyValuePair<int, List<int>> keyVal in markingsDict){
            maxMarkingDict.Add(keyVal.Key, keyVal.Value.Max());
        }

        Dictionary<int, int> sumLossDict = new Dictionary<int, int>();

        for(int i=0;i<markingsDict.Count();i++){
            int sum = 0;
            int maxMarkingAvailable = maxMarkingDict[i];
            if(maxMarkingAvailable >= maxRequirement){
                List<int> markingList = markingsDict[i];
                for(int j=0;j<requirements.Count();j++){
                    for(int k=0;k<markingList.Count();k++){
                        if(markingList[k]>=requirements[j]){
                            sum+=markingList[k]-requirements[j];
                            break;
                        }
                    }
                }
                sumLossDict.Add(i, sum);
            }
        }

        if(sumLossDict.Count()>0){
            var keyAndValue = sumLossDict.OrderBy(kvp => kvp.Value).First();
            flaskId = keyAndValue.Key;
        }
        return flaskId;
    }

}



class Solution
{
    public static void Main(string[] args)
    {
        TextWriter textWriter = new StreamWriter(@System.Environment.GetEnvironmentVariable("OUTPUT_PATH"), true);

        int requirementsCount = Convert.ToInt32(Console.ReadLine().Trim());

        List<int> requirements = new List<int>();

        for (int i = 0; i < requirementsCount; i++)
        {
            int requirementsItem = Convert.ToInt32(Console.ReadLine().Trim());
            requirements.Add(requirementsItem);
        }

        int flaskTypes = Convert.ToInt32(Console.ReadLine().Trim());

        int markingsRows = Convert.ToInt32(Console.ReadLine().Trim());
        int markingsColumns = Convert.ToInt32(Console.ReadLine().Trim());

        List<List<int>> markings = new List<List<int>>();

        for (int i = 0; i < markingsRows; i++)
        {
            markings.Add(Console.ReadLine().TrimEnd().Split(' ').ToList().Select(markingsTemp => Convert.ToInt32(markingsTemp)).ToList());
        }

        int result = Result.chooseFlask(requirements, flaskTypes, markings);

        textWriter.WriteLine(result);

        textWriter.Flush();
        textWriter.Close();
    }
}
