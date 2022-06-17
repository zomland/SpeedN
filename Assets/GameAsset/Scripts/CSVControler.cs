using System.Collections.Generic;
using System.IO;
using System;


public static class CSVControler
{
    public static List<Dictionary<string, object>> DataFromCSV(string path)
    {
        Dictionary<string, object> dataDict;
        List<Dictionary<string, object>> DataDicts = new List<Dictionary<string, object>>();
        string[] Lines = File.ReadAllLines(path);
        string[] nameData = Lines[0].Split(',');
        string[] typeData = Lines[1].Split(',');

        for (int i = 2; i <= Lines.Length - 1; i++)
        {
            string[] dataStrings = Lines[i].Split(',');
            dataDict = new Dictionary<string, object>();
            for (int index = 0; index < dataStrings.Length - 1; index++)
            {
                dataDict.Add(nameData[index], ConvertStringToType(typeData[index], dataStrings[index]));
            }
            DataDicts.Add(dataDict);
        }
        return DataDicts;
    }

    static System.Object ConvertStringToType(string Type, string data)
    {
        switch (Type)
        {
            case "string":
                return data;
            case "int":
                return Convert.ToInt32(data);
            case "float":
                return Convert.ToSingle(data);
            default:
                return null;
        }
    }
}
