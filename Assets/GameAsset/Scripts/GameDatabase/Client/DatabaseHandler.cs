using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Base.Helper;

public static class DatabaseHandler
{
    public delegate void DatabaseHandlerCallback(string message);
    public delegate void DatabaseHandlerFallback(string message);

    public enum TypePath
    {
        User, Vehicle, Coin, MovingRecord, ModelVehicle
    }

    static Dictionary<TypePath, string> pathDict = new Dictionary<TypePath, string>()
    {
        {TypePath.User,"ClientUserData.csv"},
        {TypePath.Vehicle,"ClientVehicleData.csv"},
        {TypePath.Coin,"ClientCoin.csv"},
        {TypePath.MovingRecord,"ClientMovingRecord.csv"},
        {TypePath.ModelVehicle,"ModelVehicle.csv"},
    };

    public static void LoadUserData(DatabaseHandlerCallback callback, DatabaseHandlerFallback fallback)
    {
        string JsonUserData = CSVControler.JsonDataFromCSV(pathDict[TypePath.User]);
        if (JsonUserData != null)
        {
            ClientData.Instance.ClientUser = JsonConvert.DeserializeObject<ClientUser>(JsonUserData);
            callback.Invoke("success");
        }
        else
        {
            CSVControler.CreateCSV(pathDict[TypePath.User], ClientData.Instance.ClientUser.GetStringJsonData());
            fallback.Invoke("failed");
        }
    }

    public static void SaveUserData(DatabaseHandlerCallback callback)
    {
        string Json = ClientData.Instance.ClientUser.GetStringJsonData();
        string[] content = new string[] { Json };
        CSVControler.OverWriteCSV(pathDict[TypePath.User], content);
        callback.Invoke("success");
    }

    public static void LoadMovingRecords(DatabaseHandlerCallback callback, DatabaseHandlerFallback fallback)
    {
        Dictionary<string, MovingRecordDetail> movingRecordDetails = new Dictionary<string, MovingRecordDetail>();
        List<string> MovingRecordDetailsJson = CSVControler.ListJsonDataFromCSV(pathDict[TypePath.MovingRecord]);

        if (MovingRecordDetailsJson != null)
        {
            foreach (string json in MovingRecordDetailsJson)
            {
                MovingRecordDetail movingRecordDetail = JsonConvert.DeserializeObject<MovingRecordDetail>(json);
                movingRecordDetails.Add(movingRecordDetail.RecordID, movingRecordDetail);
            }
            ClientData.Instance.ClientMovingRecord.LoadMovingRecords(movingRecordDetails);
            callback.Invoke("success");
        }
        else
        {
            CSVControler.CreateCSV(pathDict[TypePath.MovingRecord], new MovingRecordDetail().GetStringJsonData());
            fallback.Invoke("failed");
        }
    }

    public static void SaveAMovingRecord(MovingRecordDetail movingRecordDetail, DatabaseHandlerCallback callback)
    {
        string json = movingRecordDetail.GetStringJsonData();
        CSVControler.AppendALineToCsv(pathDict[TypePath.MovingRecord], json);
        callback.Invoke("success");
    }

    public static void LoadVehicleData(DatabaseHandlerCallback callback, DatabaseHandlerFallback fallback)
    {
        List<string> ListVehicleDataJson = CSVControler.ListJsonDataFromCSV(pathDict[TypePath.Vehicle]);
        if (ListVehicleDataJson != null)
        {
            foreach (string child in ListVehicleDataJson)
            {
                VehicleData vehicleData = JsonConvert.DeserializeObject<VehicleData>(child);
                ClientData.Instance.ClientVehicle.Vehicles.Add(new Vehicle(vehicleData));
            }
            callback.Invoke("success");
        }
        else
        {
            CSVControler.CreateCSV(pathDict[TypePath.Vehicle], new VehicleData().GetStringJsonData());
            fallback.Invoke("failed");
        }
    }

    public static void SaveVehicleData(DatabaseHandlerCallback callback)
    {
        List<string> content = new List<string>();
        foreach (Vehicle child in ClientData.Instance.ClientVehicle.Vehicles)
        {
            content.Add(child.Data.GetStringJsonData());
        }
        CSVControler.OverWriteCSV(pathDict[TypePath.Vehicle], content.ToArray());
        callback.Invoke("success");
    }

    public static void LoadModelVehicle(DatabaseHandlerCallback callback, DatabaseHandlerFallback fallback)
    {
        List<string> ListModelVehicleJson = CSVControler.ListJsonDataFromCSV(pathDict[TypePath.ModelVehicle]);
        if (ListModelVehicleJson != null)
        {
            foreach (string child in ListModelVehicleJson)
            {
                ModelVehicleBaseStats modelVehicleBaseStats = JsonConvert.DeserializeObject<ModelVehicleBaseStats>(child);
                ModelVehicle.AddModelStat(modelVehicleBaseStats);
            }
            callback.Invoke("success");
        }
        else
        {
            fallback.Invoke("failed");
        }
    }

    public static void LoadClientCoin(DatabaseHandlerCallback callback, DatabaseHandlerFallback fallback)
    {
        List<string> ListCoinDataJson = CSVControler.ListJsonDataFromCSV(pathDict[TypePath.Coin]);
        if (ListCoinDataJson != null)
        {
            foreach (string child in ListCoinDataJson)
            {
                Coin coin = JsonConvert.DeserializeObject<Coin>(child);
                ClientData.Instance.ClientCoin.Coins.Add(new Coin(coin.nameCoin, coin.amount));
            }
            callback.Invoke("success");
        }
        else
        {
            CSVControler.CreateCSV(pathDict[TypePath.Coin], new Coin().GetStringJsonData());
            fallback.Invoke("failed");
        }
    }

    public static void SaveClientCoin(DatabaseHandlerCallback callback)
    {
        List<string> content = new List<string>();
        foreach (Coin child in ClientData.Instance.ClientCoin.Coins)
        {
            content.Add(child.GetStringJsonData());
        }
        CSVControler.OverWriteCSV(pathDict[TypePath.Coin], content.ToArray());
        callback.Invoke("success");
    }
}

