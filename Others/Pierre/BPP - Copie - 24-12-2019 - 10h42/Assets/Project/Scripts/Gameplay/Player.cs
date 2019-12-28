using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ProjetPirate.Boat;
using ProjetPirate.Data;

public class Player : Controller {

    [Header("NAME")]
    [SerializeField]
    private string _name = "";
    [SerializeField] private ProjetPirate.Boat.BoatCharacter _boat;

    [SerializeField] [Range(1, 3)] private int _shipLevel = 1;
    [SerializeField] private List<GameObject> _structuresPerLevel;

    [SerializeField] public int _maxXp = 10000; // max XP
    public int _currentXp;
    [SerializeField] public int _maxPlank = 10000; // max XP
    [SerializeField] public int _currentPlank;
    [SerializeField] public int _maxMoney = 10000; // max XP
    public int _currentMoney;

    [SerializeField] public int _xpLostByDeath = 10;
    [SerializeField] public float _goldRatiolostByDeath = 0.1f;
    [SerializeField] Transform _respawnPoint;

    static public GameObject Instance;

    private Data_Player data_player = new Data_Player();

    // Use this for initialization
    void Start() {
        Instance = this.gameObject;
        CreateShip();
        data_player.Boat = new Data_Boat();
    }

    // Update is called once per frame
    void Update() {
        data_player.dRessource.WoodBoard = _currentPlank;
        data_player.dRessource.Golds = _currentMoney;
        data_player.dRessource.Reputation = _currentXp;

        data_player.Boat.dStats.Speed = _boat.CurrentMovingSpeed;
        data_player.Boat.dStats.Life = _boat.CurrentLifePoint;
        data_player.Boat.ReverseReloadTransform(_boat.gameObject);

    }

    public void SetUpData(Data_Player pNewData)
    {
        data_player = pNewData;
        _currentMoney = data_player.dRessource.Golds;
        _currentPlank = data_player.dRessource.WoodBoard;
        _currentXp = (int)data_player.dRessource.Reputation;

        _boat.SetUpData(pNewData.Boat);
    }

    public void ChangeShipLevel(int pNewShipLevel)
    {
        _shipLevel = pNewShipLevel;
        BoatCharacter previousBoat = _boat;
        CreateShip();
        Destroy(previousBoat.gameObject);
    }

    public void CreateShip()
    {
        _boat = Instantiate(_structuresPerLevel[_shipLevel - 1]).GetComponent<BoatCharacter>();
        _boat.SetUpBoat(this);
        _boat.gameObject.AddComponent<BoatController>();
    }

    public void AddLarboardCannon()
    {
        _boat.AddLarboardCannon();
    }

    public void RemoveLarboardCannon()
    {
        _boat.RemoveLarboardCannon();
    }

    public void AddStarboardCannon()
    {
        _boat.AddStarboardCannon();
    }

    public void RemoveStarboardCannon()
    {
        _boat.RemoveStarboardCannon();
    }

    public virtual void GainXP(int pEarnedXP)
    {
        _currentXp += pEarnedXP;
        if (_currentXp > _maxXp)
        {
            _currentXp = _maxXp;
        }
    }

    public virtual void GainPlank(int pGainedPlank)
    {
        _currentPlank += pGainedPlank;
        if (_currentPlank > _maxPlank)
        {
            _currentPlank = _maxPlank;
        }
    }

    public virtual void GainMoney(int pGainedMoney)
    {
        _currentMoney += pGainedMoney;
        if (_currentMoney > _maxMoney)
        {
            _currentMoney = _maxMoney;
        }
    }

    public virtual void LoseXP(int pLostXP)
    {
        _currentXp -= pLostXP;
        if (_currentXp < 0)
        {
            _currentXp = 0;
        }
    }

    public virtual void LosePlank(int pLostPlank)
    {
        _currentPlank += pLostPlank;
        if (_currentPlank < 0)
        {
            _currentPlank = 0;
        }
    }

    public virtual void LoseMoney(int pLostMoney)
    {
        _currentMoney += pLostMoney;
        if (_currentMoney < 0)
        {
            _currentMoney = 0;
        }
    }

    public override void Death()
    {
        LoseXP(_xpLostByDeath);
        int lostMoney = (int)(_currentMoney * _goldRatiolostByDeath);
        PlankOnSea.SpawnPlank(_boat._droppedPlank, _boat.transform.position, _boat._plankDroppedByDeath);
        Chest.SpawnChest(_boat.DroppedChest, _boat.transform.position, lostMoney, _currentPlank);
        LoseMoney(lostMoney);
        LosePlank(_currentPlank);
        

    }

    public override void Disappear()
    {
        _boat.transform.position = _respawnPoint.position;
        _boat.transform.eulerAngles = _respawnPoint.eulerAngles;
    }

}
