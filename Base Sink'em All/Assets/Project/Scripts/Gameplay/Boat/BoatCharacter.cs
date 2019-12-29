using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using ProjetPirate.Controllers;
//using ProjetPirate.Physic;
using ProjetPirate.Data;

namespace ProjetPirate.Boat
{
    public enum ShipType
    {
        Military,
        Merchant,
        Player_Level_1,
        Player_Level_2,
        Player_Level_3,
    }

    public enum StructureState
    {
        Normal,
        Weakened,
        Endangered
    }

    public enum BoatMovementState
    {
        IDLE, // DON'T MOVE
        ACCELERATE,
        DECELERATE,
        CRUISE_SPEED // VITESSE DE CROISIERE
    }

    /// <summary>
    /// use for the future animations of the boat
    /// </summary>
    public enum BoatRotationState
    {
        FORWARD, //DON'T TURN
        BABORD, //TURN LEFT 
        TRIBORD //TURN RIGHT
    }

    ///// <summary>
    ///// Base class of the boat posess al the main data
    ///// </summary>
    //[RequireComponent(typeof(Rigidbody))]
    //[RequireComponent(typeof(FloatingObject))]
    //[RequireComponent(typeof(AttractObject))]
    public class BoatCharacter : ProjetPirate.IA.Character
    {

        [Header("CANNONS")]
        [SerializeField] private List<Cannon> _larboardCannons;
        [SerializeField] private List<Cannon> _starboardCannons;


        [SerializeField] private List<Transform> _larboardCannonPositions;
        [SerializeField] private List<Transform> _starboardCannonPositions;
        [SerializeField] private int _defaultCannonNumberBySide;

        [SerializeField] private GameObject _larboardCannonPrefab;
        [SerializeField] private GameObject _starboardCannonPrefab;


        public int DefaultCannonNumberBySide
        {
            get { return _defaultCannonNumberBySide; }
        }

        public List<Transform> LarboardCannonPositions
        {
            get { return _larboardCannonPositions; }
        }

        public List<Transform> StarboardCannonPositions
        {
            get { return _starboardCannonPositions; }
        }

        public void SetUpBoat(Player _player)
        {
            this.gameObject.transform.SetParent(_player.gameObject.transform);
            this.transform.localPosition = new Vector3(0, 0, 0);
            for (int i = 0; i < this.DefaultCannonNumberBySide; i++)
            {
                this.AddLarboardCannon();
                this.AddStarboardCannon();
            }
            _controller = _player;
        }

        //public void SetUpBoat(Ship_Controller pController)
        //{
        //    _controller = pController;
        //}

        public void AddLarboardCannon()
        {
            if (_larboardCannons.Count < LarboardCannonPositions.Count)
            {
                Cannon cannon = Instantiate(_larboardCannonPrefab).GetComponent<Cannon>();
                cannon.gameObject.transform.SetParent(LarboardCannonPositions[_larboardCannons.Count]);
                cannon.gameObject.transform.localPosition = Vector3.zero;
                cannon.SetOwner(this);
                _larboardCannons.Add(cannon);
            }
        }

        //public void RemoveLarboardCannon()
        //{
        //    if (_larboardCannons.Count > 0)
        //    {
        //        Cannon cannon = _larboardCannons[_larboardCannons.Count - 1];
        //        _larboardCannons.RemoveAt(_larboardCannons.Count - 1);
        //        Destroy(cannon.gameObject);
        //    }
        //}

        public void AddStarboardCannon()
        {
            if (_starboardCannons.Count < StarboardCannonPositions.Count)
            {
                Cannon cannon = Instantiate(_starboardCannonPrefab).GetComponent<Cannon>();
                cannon.gameObject.transform.SetParent(StarboardCannonPositions[_starboardCannons.Count]);
                cannon.gameObject.transform.localPosition = Vector3.zero;
                cannon.SetOwner(this);
                _starboardCannons.Add(cannon);
            }
        }

    }
}
