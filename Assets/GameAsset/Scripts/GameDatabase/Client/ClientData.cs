using Base;
using UnityEngine;

public class ClientData : Singleton<ClientData>
{
    private  ClientUser _clientUser;
    public ClientUser ClientUser => _clientUser;
    public MovingRecordManager _movingRecordManager;
    [field: SerializeField] public SpeedNDefault speedNDefault { get; private set; }

    void Awake()
    {
        _clientUser = new ClientUser(speedNDefault);
        //Test add Vehicle
        AddVehicle();
        _clientUser.InitialVehicle();
        _movingRecordManager= new MovingRecordManager();
    }

    public SpriteIcon GetSpriteIcon(string name)
    {
        foreach (var child in speedNDefault.spriteIcons)
        {
            if (child.name == name) return child;
        }
        return null;
    }

    //Vehicle Sprite
    public SpriteVehicle GetSpriteVehicle(string name)
    {
        foreach (var child in speedNDefault.spriteVehicles)
        {
            if (child.name == name) return child;
        }
        return null;
    }

    public void GetMovingRecords()
    {
        
    }

    private void AddVehicle()
    {
        int i = 100;
        foreach (var child in speedNDefault.spriteVehicles)
        {
            CarAttribute carAttribute= new CarAttribute(child.name,i.ToString(),VehicleRarity.Common
                ,CarType.Urban,2000f,1f,1f);
            _clientUser.clientNFT.clientVehicles.Add(new ClientCar(carAttribute));
        }
    }
}
