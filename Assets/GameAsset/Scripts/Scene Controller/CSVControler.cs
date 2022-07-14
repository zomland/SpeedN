using System.Collections.Generic;
using System.IO;
using System;
using Newtonsoft.Json;
using UnityEngine;
using System.Linq;

public static class CSVControler
{
    public static List<string> ListJsonDataFromCSV(string _nameFile)
    {
        string path = Application.persistentDataPath + "/" + _nameFile;
        if (File.Exists(path))
        {
            Dictionary<string, object> dataDict;
            List<string> JsonDataList = new List<string>();
            string[] Lines = File.ReadAllLines(path);
            if (Lines.Length <= 2) return null;
            string nameDataLine = Lines[0];
            string typeDataLine = Lines[1];
            string[] namesData = nameDataLine.Split(',');
            string[] typesData = typeDataLine.Split(',');

            for (int i = 2; i <= Lines.Length - 1; i++)
            {
                string[] dataStrings = Lines[i].Split(',');
                dataDict = new Dictionary<string, object>();
                for (int index = 0; index < dataStrings.Length; index++)
                {
                    dataDict.Add(namesData[index], ConvertStringToType(typesData[index], dataStrings[index]));
                }
                JsonDataList.Add(JsonConvert.SerializeObject(dataDict));
            }
            return JsonDataList;
        }
        return null;
    }

    public static string JsonDataFromCSV(string _nameFile)
    {
        string path = Application.persistentDataPath + "/" + _nameFile;
        if (File.Exists(path))
        {
            Dictionary<string, object> dataDict;
            string[] Lines = File.ReadAllLines(path);
            if (Lines.Length <= 2) return null;
            string nameDataLine = Lines[0];
            string typeDataLine = Lines[1];
            string[] namesData = nameDataLine.Split(',');
            string[] typesData = typeDataLine.Split(',');
            string[] dataStrings = Lines[2].Split(',');
            dataDict = new Dictionary<string, object>();
            for (int index = 0; index < dataStrings.Length; index++)
            {
                dataDict.Add(namesData[index], ConvertStringToType(typesData[index], dataStrings[index]));
            }
            return JsonConvert.SerializeObject(dataDict);
        }
        return null;
    }


    public static void OverWriteCSV(string _nameFile, string[] content)
    {
        string path = Application.persistentDataPath + "/" + _nameFile;
        Dictionary<string, System.Object> dataDict;
        string nameDataLine = GetNameDataLine(content[0]);
        string typeDataLine = GetTypeDataLine(content[0]);
        string[] namesData = nameDataLine.Split(',');
        List<string> DataLines = new List<string> { nameDataLine, typeDataLine };

        foreach (string child in content)
        {
            dataDict = JsonConvert.DeserializeObject<Dictionary<string, System.Object>>(child);
            string lineData = "";
            for (int i = 0; i <= namesData.Length - 1; i++)
            {
                lineData += dataDict[namesData[i]].ToString();
                if (i != namesData.Length - 1) lineData += ",";
            }
            DataLines.Add(lineData);
        }
        File.WriteAllLines(path, DataLines);

    }

    public static void AppendALineToCsv(string _nameFile, string dataJson)
    {
        string path = Application.persistentDataPath + "/" + _nameFile;
        if (!File.Exists(path)) CreateCSV(_nameFile, dataJson);
        Dictionary<string, System.Object> clientDataDict;
        string[] Lines = File.ReadAllLines(path);
        string nameDataLine = Lines[0];
        string typeDataLine = Lines[1];
        string[] namesData = nameDataLine.Split(',');

        clientDataDict = JsonConvert.DeserializeObject<Dictionary<string, System.Object>>(dataJson);
        string lineData = "";
        for (int i = 0; i <= namesData.Length - 1; i++)
        {
            lineData += clientDataDict[namesData[i]].ToString();
            if (i != namesData.Length - 1) lineData += ",";
        }
        string[] content = new string[] { lineData };
        File.AppendAllLines(path, content);

    }

    public static void CreateCSV(string _nameFile, string json)
    {
        string path = Application.persistentDataPath + "/" + _nameFile;
        Dictionary<string, object> dataDict = JsonConvert.DeserializeObject<Dictionary<string, object>>(json);
        string dataNameLine = GetNameDataLine(json);
        string dataTypeLine = GetTypeDataLine(json); ;
        string[] content = new string[] { dataNameLine, dataTypeLine };
        File.WriteAllLines(path, content);
    }


    static string GetNameDataLine(string json)
    {
        Dictionary<string, object> dataDict = JsonConvert.DeserializeObject<Dictionary<string, object>>(json);
        string dataNameLine = "";
        for (int index = 0; index <= dataDict.Count - 1; index++)
        {
            dataNameLine += dataDict.ElementAt(index).Key;
            if (index != dataDict.Count - 1)
            {
                dataNameLine += ",";
            }
        }
        return dataNameLine;
    }

    static string GetTypeDataLine(string json)
    {
        Dictionary<string, object> dataDict = JsonConvert.DeserializeObject<Dictionary<string, object>>(json);
        string dataTypeLine = "";
        for (int index = 0; index <= dataDict.Count - 1; index++)
        {
            dataTypeLine += dataDict.ElementAt(index).Value.GetType().ToString();
            if (index != dataDict.Count - 1)
            {
                dataTypeLine += ",";
            }
        }
        return dataTypeLine;
    }

    static System.Object ConvertStringToType(string Type, string data)
    {
        switch (Type)
        {
            case "System.String":
                return data;
            case "System.Int32":
                return Convert.ToInt32(data);
            case "System.Double":
                return Convert.ToSingle(data);
            case "System.Int64":
                return Convert.ToInt64(data);
            default:
                return null;
        }
    }
}
