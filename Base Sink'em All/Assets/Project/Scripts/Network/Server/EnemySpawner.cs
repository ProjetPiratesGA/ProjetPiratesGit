using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;


namespace ProjetPirate.Network
{

    public class EnemySpawner : MonoBehaviour
    {

        //Boat
        [SerializeField]
        GameObject merchantEnemy;
        [SerializeField]
        GameObject militaryEnemy;
        //Shark
        [SerializeField]
        GameObject sharkEnemy;
        [SerializeField]
        GameObject sharkPatrol;
        [SerializeField]
        GameObject patrolWander;
        //Kraken
        [SerializeField]
        GameObject krakenEnemy;


        [System.Serializable]
        public struct structSpawnEnemyShark
        {
            public Transform transformSpawnEnemyShark;
            [System.NonSerialized]
            public bool spawnIsUsed;
            [System.NonSerialized]
            public float timeSinceLastSpawn;
            [System.NonSerialized]
            public float timeLastSpawn;
            [System.NonSerialized]
            public int timeToSpawn;
            [System.NonSerialized]
            public bool lockRandom;
            public int minTimeSpawn;
            public int maxTimeSpawn;
        }

        [System.Serializable]
        public struct structSpawnEnemyKraken
        {
            public Transform transformSpawnEnemyKraken;
            [System.NonSerialized]
            public bool isSpawn;
            [System.NonSerialized]
            public int timeToSpawn;
            public int minTimeSpawn;
            public int maxTimeSpawn;
        }

        [System.Serializable]
        public struct structSpawnEnemyMilitary
        {
            public Transform transformSpawnEnemyMilitary;
            [System.NonSerialized]
            public bool spawnIsUsed;
            [System.NonSerialized]
            public float timeSinceLastSpawn;
            [System.NonSerialized]
            public float timeLastSpawn;
            [System.NonSerialized]
            public int timeToSpawn;
            [System.NonSerialized]
            public bool lockRandom;
            public int minTimeSpawn;
            public int maxTimeSpawn;
        }

        [System.Serializable]
        public struct structSpawnEnemyMerchant
        {
            public Transform transformSpawnEnemyMerchant;
            [System.NonSerialized]
            public bool spawnIsUsed;
            [System.NonSerialized]
            public float timeSinceLastSpawn;
            [System.NonSerialized]
            public float timeLastSpawn;
            [System.NonSerialized]
            public int timeToSpawn;
            [System.NonSerialized]
            public bool lockRandom;
            public int minTimeSpawn;
            public int maxTimeSpawn;
        }


        [SerializeField]
        public List<structSpawnEnemyShark> spawnEnemyShark = new List<structSpawnEnemyShark>();

        [SerializeField]
        public List<structSpawnEnemyMilitary> spawnEnemyBoatMilitary = new List<structSpawnEnemyMilitary>();

        [SerializeField]
        public List<structSpawnEnemyMerchant> spawnEnemyBoatMerchant = new List<structSpawnEnemyMerchant>();

        [SerializeField]
        public List<structSpawnEnemyKraken> spawnEnemyKraken = new List<structSpawnEnemyKraken>();



        int timeToTrySpawnKraken = 3;

        float timeSinceLastSpawnKraken;
        float timeLastSpawnKraken;

        bool canSpawnKrakenDebug;
        // Use this for initialization
        void Start()
        {
            //InitSpawnAllEnemy();


            //Random Initialisation
            //Shark
            for (int i = 0; i < spawnEnemyShark.Count; i++)
            {
                var spawnEnemySharkBuffer = spawnEnemyShark[i];
                spawnEnemySharkBuffer.timeToSpawn = Random.Range(spawnEnemySharkBuffer.minTimeSpawn, spawnEnemySharkBuffer.maxTimeSpawn);
                spawnEnemyShark[i] = spawnEnemySharkBuffer;
            }
            //Military Boat
            for (int i = 0; i < spawnEnemyBoatMilitary.Count; i++)
            {
                var spawnEnemyMilitaryBuffer = spawnEnemyBoatMilitary[i];
                spawnEnemyMilitaryBuffer.timeToSpawn = Random.Range(spawnEnemyMilitaryBuffer.minTimeSpawn, spawnEnemyMilitaryBuffer.maxTimeSpawn);
                spawnEnemyBoatMilitary[i] = spawnEnemyMilitaryBuffer;
            }

            //Merchant Boat
            for (int i = 0; i < spawnEnemyBoatMerchant.Count; i++)
            {
                var spawnEnemyMerchantBuffer = spawnEnemyBoatMerchant[i];
                spawnEnemyMerchantBuffer.timeToSpawn = Random.Range(spawnEnemyMerchantBuffer.minTimeSpawn, spawnEnemyMerchantBuffer.maxTimeSpawn);
                spawnEnemyBoatMerchant[i] = spawnEnemyMerchantBuffer;
            }

            //Kraken          
            for (int i = 0; i < spawnEnemyKraken.Count; i++)
            {
                var spawnEnemyKrakenBuffer = spawnEnemyKraken[i];
                spawnEnemyKrakenBuffer.timeToSpawn = Random.Range(spawnEnemyKrakenBuffer.minTimeSpawn, spawnEnemyKrakenBuffer.maxTimeSpawn);
                spawnEnemyKraken[i] = spawnEnemyKrakenBuffer;
            }

        }
        // Update is called once per frame
        void Update()
        {

            if (Input.GetKeyDown("m"))
            {
                SpawnEnemyMilitary();
            }

            canSpawnKrakenDebug = false;
            if (Input.GetKeyDown("p"))
            {
                canSpawnKrakenDebug = true;
                timeSinceLastSpawnKraken = timeToTrySpawnKraken;
                SpawnEnemyKraken();
            }


            if (Input.GetKeyDown("l"))
            {
                SpawnEnemyMerchant();
            }

            if (Input.GetKeyDown("o"))
            {
                SpawnEnemyShark();

            }

            UpdateSpawnUtilisation();

        }

        void SpawnEnemyMilitary()
        {
            for (int i = 0; i < spawnEnemyBoatMilitary.Count; i++)
            {
                GameObject enemyBoatInstance = Instantiate(militaryEnemy);
                enemyBoatInstance.gameObject.transform.position = spawnEnemyBoatMilitary[i].transformSpawnEnemyMilitary.position;
                NetworkServer.Spawn(enemyBoatInstance);
            }

        }

        void SpawnEnemyMerchant()
        {
            for (int i = 0; i < spawnEnemyBoatMerchant.Count; i++)
            {
                GameObject enemyBoatInstance = Instantiate(merchantEnemy);

                enemyBoatInstance.gameObject.transform.position = spawnEnemyBoatMerchant[i].transformSpawnEnemyMerchant.position;
                NetworkServer.Spawn(enemyBoatInstance);
            }
        }

        void SpawnEnemyShark()
        {
            for (int i = 0; i < spawnEnemyShark.Count; i++)
            {
                //Debug.LogError(spawnEnemyShark[i].timeToSpawn + " | " + i);
                if (spawnEnemyShark[i].spawnIsUsed == false && spawnEnemyShark[i].timeSinceLastSpawn >= spawnEnemyShark[i].timeToSpawn)
                {
                    int randomSpawn = Random.Range(0, 4);
                    if (randomSpawn >= 0 && randomSpawn <= 2)
                    {

                        //Instantiate Enemy and Set Position
                        GameObject enemySpawn = Instantiate(sharkEnemy, spawnEnemyShark[i].transformSpawnEnemyShark);

                        enemySpawn.gameObject.transform.position = spawnEnemyShark[i].transformSpawnEnemyShark.position;

                        //Instantiate Enemy Patrol and Set Position
                        GameObject enemyPatrol = Instantiate(sharkPatrol, spawnEnemyShark[i].transformSpawnEnemyShark);
                        enemyPatrol.gameObject.transform.position = spawnEnemyShark[i].transformSpawnEnemyShark.position;

                        GameObject enemyPatrolWander = Instantiate(patrolWander, spawnEnemyShark[i].transformSpawnEnemyShark);
                        enemyPatrolWander.gameObject.transform.position = spawnEnemyShark[i].transformSpawnEnemyShark.position;

                        //Set Transform Patrol to Script Enemy
                        enemySpawn.gameObject.GetComponent<ProjetPirate.IA.Patrol_Wander>()._center = enemyPatrol.transform.Find("Center");
                        enemySpawn.gameObject.GetComponent<ProjetPirate.IA.Patrol_Wander>()._destination = enemyPatrol.transform.Find("Objectif");

                        Debug.LogError("Try to Set Detector");
                        enemyPatrolWander.GetComponent<ProjetPirate.IA.Detector>()._enemies[0] = enemySpawn.GetComponent<ProjetPirate.IA.Shark_Controller>();


                        NetworkServer.Spawn(enemySpawn);
                        //NetworkServer.Spawn(enemySpawn);
                        var spawnEnemySharkBuffer = spawnEnemyShark[i];
                        spawnEnemySharkBuffer.spawnIsUsed = true;
                        spawnEnemyShark[i] = spawnEnemySharkBuffer;
                    }
                }
                else
                {
                    //Debug.LogError("This Spawn is Used : " + i);
                }
            }
        }

        void SpawnEnemyKraken()
        {
            if (canSpawnKrakenDebug == false)
            {
                timeSinceLastSpawnKraken = Time.time - timeLastSpawnKraken;
            }
            bool canSpawnKraken = true;
            for (int j = 0; j < spawnEnemyKraken.Count; j++)
            {
                if (spawnEnemyKraken[j].isSpawn == true)
                {
                    canSpawnKraken = false;
                }
            }
            if (canSpawnKraken == true || canSpawnKrakenDebug == true)
            {

                int i = Random.Range(0, spawnEnemyKraken.Count);


                if (timeSinceLastSpawnKraken >= timeToTrySpawnKraken)
                {
                    var spawnEnemyKrakenBuffer = spawnEnemyKraken[i];

                    timeLastSpawnKraken = Time.time;
                    int randomSpawn;
                    randomSpawn = Random.Range(0, 101);
                    if (canSpawnKrakenDebug == true)
                    {
                        randomSpawn = 50;
                    }
                    GameObject _krakenEnemy;
                    //Attacher un script au spawn, déterminant dans quel type de zone il est.
                    //Récupérer la valeur présent sur le script avec un GetComponenent
                    Debug.Log("Try to Spawn KRAKEN---------------------");
                    //if(Check Zone);
                    if (randomSpawn >= 0 && randomSpawn <= 30)
                    {
                        Debug.Log("Spawn KRAKEN1---------------------");

                        _krakenEnemy = Instantiate(krakenEnemy, spawnEnemyKraken[i].transformSpawnEnemyKraken);
                        spawnEnemyKrakenBuffer.isSpawn = true;
                        NetworkServer.Spawn(_krakenEnemy);

                    }
                    //if(Check Zone);
                    if (randomSpawn >= 0 && randomSpawn <= 70)
                    {
                        Debug.Log("Spawn KRAKEN2---------------------");

                        _krakenEnemy = Instantiate(krakenEnemy, spawnEnemyKraken[i].transformSpawnEnemyKraken);
                        spawnEnemyKrakenBuffer.isSpawn = true;
                        NetworkServer.Spawn(_krakenEnemy);
                    }
                    spawnEnemyKraken[i] = spawnEnemyKrakenBuffer;
                }
            }

        }

        void InitSpawnAllEnemy()
        {
            #region Shark
            for (int i = 0; i < spawnEnemyShark.Count; i++)
            {
                //Instantiate Enemy and Set Position

                GameObject enemySpawnShark = Instantiate(sharkEnemy, spawnEnemyShark[i].transformSpawnEnemyShark);
                enemySpawnShark.gameObject.transform.position = spawnEnemyShark[i].transformSpawnEnemyShark.position;

                //Instantiate Enemy Patrol and Set Position
                GameObject enemyPatrol = Instantiate(sharkPatrol, spawnEnemyShark[i].transformSpawnEnemyShark);
                enemyPatrol.gameObject.transform.position = spawnEnemyShark[i].transformSpawnEnemyShark.position;

                GameObject enemyPatrolWander = Instantiate(patrolWander, spawnEnemyShark[i].transformSpawnEnemyShark);
                enemyPatrolWander.gameObject.transform.position = spawnEnemyShark[i].transformSpawnEnemyShark.position;

                //Set Transform Patrol to Script Enemy
                enemySpawnShark.gameObject.GetComponent<ProjetPirate.IA.Patrol_Wander>()._center = enemyPatrol.transform.Find("Center");
                enemySpawnShark.gameObject.GetComponent<ProjetPirate.IA.Patrol_Wander>()._destination = enemyPatrol.transform.Find("Objectif");

                // Debug.LogError("Try to Set Detector");
                enemyPatrolWander.GetComponent<ProjetPirate.IA.Detector>()._enemies[0] = enemySpawnShark.GetComponent<ProjetPirate.IA.Shark_Controller>();

                //NetworkServer.Spawn(enemyPatrol);
                //NetworkServer.Spawn(enemyPatrolWander);
                NetworkServer.Spawn(enemySpawnShark);


                var spawnEnemySharkBuffer = spawnEnemyShark[i];
                spawnEnemySharkBuffer.spawnIsUsed = true;
                spawnEnemyShark[i] = spawnEnemySharkBuffer;
            }
            #endregion

            #region Military Boat

            for (int i = 0; i < spawnEnemyBoatMilitary.Count; i++)
            {
                //Instantiate Enemy and Set Position

                GameObject enemySpawnMilitary = Instantiate(militaryEnemy, spawnEnemyBoatMilitary[i].transformSpawnEnemyMilitary);
                enemySpawnMilitary.gameObject.transform.position = spawnEnemyBoatMilitary[i].transformSpawnEnemyMilitary.position;

                NetworkServer.Spawn(enemySpawnMilitary);

                var spawnEnemyMilitaryBuffer = spawnEnemyBoatMilitary[i];
                spawnEnemyMilitaryBuffer.spawnIsUsed = true;
                spawnEnemyBoatMilitary[i] = spawnEnemyMilitaryBuffer;
            }

            #endregion

            #region Merchant Boat

            for (int i = 0; i < spawnEnemyBoatMerchant.Count; i++)
            {
                //Instantiate Enemy and Set Position

                GameObject enemySpawnMerchant = Instantiate(merchantEnemy, spawnEnemyBoatMerchant[i].transformSpawnEnemyMerchant);
                enemySpawnMerchant.gameObject.transform.position = spawnEnemyBoatMerchant[i].transformSpawnEnemyMerchant.position;

                NetworkServer.Spawn(enemySpawnMerchant);

                var spawnEnemyMilitaryBuffer = spawnEnemyBoatMerchant[i];
                spawnEnemyMilitaryBuffer.spawnIsUsed = true;
                spawnEnemyBoatMerchant[i] = spawnEnemyMilitaryBuffer;
            }

            #endregion
            //Remove comment if u want to spawn a Kraken when launching server
            /*
            #region Kraken

            GameObject enemySpawnKraken = Instantiate(krakenEnemy, spawnEnemyKraken.transformSpawnEnemyKraken);
            enemySpawnKraken.gameObject.transform.position = spawnEnemyKraken.transformSpawnEnemyKraken.position;
            NetworkServer.Spawn(enemySpawnKraken);

            var spawnEnemyKrakenBuffer = spawnEnemyKraken;
            spawnEnemyKrakenBuffer.isSpawn = true;
            spawnEnemyKraken = spawnEnemyKrakenBuffer;

            #endregion
            */
        }

        void UpdateSpawnUtilisation()
        {
            #region Shark
            for (int i = 0; i < spawnEnemyShark.Count; i++)
            {
                var spawnEnemySharkBuffer = spawnEnemyShark[i];
                spawnEnemySharkBuffer.timeSinceLastSpawn = Time.time - spawnEnemySharkBuffer.timeLastSpawn;

                if (spawnEnemySharkBuffer.spawnIsUsed == true)
                {
                    spawnEnemySharkBuffer.timeLastSpawn = Time.time;
                    spawnEnemySharkBuffer.lockRandom = false;
                }
                else
                {
                    if (spawnEnemySharkBuffer.lockRandom == false)
                    {
                        spawnEnemySharkBuffer.timeToSpawn = Random.Range(spawnEnemySharkBuffer.minTimeSpawn, spawnEnemySharkBuffer.maxTimeSpawn);
                        spawnEnemySharkBuffer.lockRandom = true;
                    }

                }


                spawnEnemyShark[i] = SetSpawnSharkIsUsed(spawnEnemySharkBuffer);
                ClearEnemySharkSpawner(i);
            }

            #endregion

            #region Kraken

            for (int i = 0; i < spawnEnemyKraken.Count; i++)
            {
                var spawnEnemyKrakenBuffer = spawnEnemyKraken[i];

                spawnEnemyKraken[i] = SetSpawnKrakenIsUsed(spawnEnemyKrakenBuffer);

                ClearEnemyKrakenSpawner(i);
            }

            #endregion

            #region Military Boat

            for (int i = 0; i < spawnEnemyBoatMilitary.Count; i++)
            {
                var spawnEnemyMilitaryBuffer = spawnEnemyBoatMilitary[i];
                spawnEnemyMilitaryBuffer.timeSinceLastSpawn = Time.time - spawnEnemyMilitaryBuffer.timeLastSpawn;

                if (spawnEnemyMilitaryBuffer.spawnIsUsed == true)
                {
                    spawnEnemyMilitaryBuffer.timeLastSpawn = Time.time;
                    spawnEnemyMilitaryBuffer.lockRandom = false;
                }
                else
                {
                    if (spawnEnemyMilitaryBuffer.lockRandom == false)
                    {
                        spawnEnemyMilitaryBuffer.timeToSpawn = Random.Range(spawnEnemyMilitaryBuffer.minTimeSpawn, spawnEnemyMilitaryBuffer.maxTimeSpawn);
                        spawnEnemyMilitaryBuffer.lockRandom = true;
                    }

                }


                spawnEnemyBoatMilitary[i] = SetSpawnMilitaryIsUsed(spawnEnemyMilitaryBuffer);
                ClearEnemyMilitarySpawner(i);
            }

            #endregion

            #region Merchant Boat

            for (int i = 0; i < spawnEnemyBoatMilitary.Count; i++)
            {
                var spawnEnemyMerchantBuffer = spawnEnemyBoatMilitary[i];
                spawnEnemyMerchantBuffer.timeSinceLastSpawn = Time.time - spawnEnemyMerchantBuffer.timeLastSpawn;

                if (spawnEnemyMerchantBuffer.spawnIsUsed == true)
                {
                    spawnEnemyMerchantBuffer.timeLastSpawn = Time.time;
                    spawnEnemyMerchantBuffer.lockRandom = false;
                }
                else
                {
                    if (spawnEnemyMerchantBuffer.lockRandom == false)
                    {
                        spawnEnemyMerchantBuffer.timeToSpawn = Random.Range(spawnEnemyMerchantBuffer.minTimeSpawn, spawnEnemyMerchantBuffer.maxTimeSpawn);
                        spawnEnemyMerchantBuffer.lockRandom = true;
                    }

                }


                spawnEnemyBoatMilitary[i] = SetSpawnMilitaryIsUsed(spawnEnemyMerchantBuffer);
                ClearEnemyMilitarySpawner(i);
            }

            #endregion
        }

        #region Set Spawn Is Used

        structSpawnEnemyShark SetSpawnSharkIsUsed(structSpawnEnemyShark _spawnShark)
        {


            if (_spawnShark.transformSpawnEnemyShark.gameObject.GetComponentInChildren<ProjetPirate.IA.Shark_Character>())
            {
                _spawnShark.spawnIsUsed = true;
            }
            else
            {

                _spawnShark.spawnIsUsed = false;
                //Destroy All GameObject Related to the Enemy Shark (Detctor and SharkPatrol)
            }

            return _spawnShark;
        }

        structSpawnEnemyKraken SetSpawnKrakenIsUsed(structSpawnEnemyKraken _spawnKraken)
        {
            if (_spawnKraken.transformSpawnEnemyKraken.gameObject.GetComponentInChildren<ProjetPirate.IA.Kraken_Character>())
            {
                _spawnKraken.isSpawn = true;
            }
            else
            {
                _spawnKraken.isSpawn = false;

            }
            return _spawnKraken;
        }

        structSpawnEnemyMilitary SetSpawnMilitaryIsUsed(structSpawnEnemyMilitary _spawnMilitary)
        {
            if (_spawnMilitary.transformSpawnEnemyMilitary.gameObject.GetComponentInChildren<ProjetPirate.Boat.BoatCharacter>())
            {
                _spawnMilitary.spawnIsUsed = true;
            }
            else
            {
                _spawnMilitary.spawnIsUsed = false;

            }
            return _spawnMilitary;
        }

        structSpawnEnemyMerchant SetSpawnMerchantIsUsed(structSpawnEnemyMerchant _spawnMerchant)
        {
            if (_spawnMerchant.transformSpawnEnemyMerchant.gameObject.GetComponentInChildren<ProjetPirate.Boat.BoatCharacter>())
            {
                _spawnMerchant.spawnIsUsed = true;
            }
            else
            {
                _spawnMerchant.spawnIsUsed = false;

            }
            return _spawnMerchant;
        }

        #endregion

        #region Clear Spawner

        void ClearEnemySharkSpawner(int _index)
        {

            if (!spawnEnemyShark[_index].transformSpawnEnemyShark.gameObject.GetComponentInChildren<ProjetPirate.IA.Shark_Character>())
            {
                foreach (Transform _child in spawnEnemyShark[_index].transformSpawnEnemyShark)
                {
                    Destroy(_child.gameObject);

                }
            }
        }

        void ClearEnemyKrakenSpawner(int _index)
        {
            if (!spawnEnemyKraken[_index].transformSpawnEnemyKraken.gameObject.GetComponentInChildren<ProjetPirate.IA.Kraken_Character>())
            {
                foreach (Transform _child in spawnEnemyKraken[_index].transformSpawnEnemyKraken)
                {
                    Destroy(_child.gameObject);
                }
            }
        }

        void ClearEnemyMilitarySpawner(int _index)
        {
            if (!spawnEnemyBoatMilitary[_index].transformSpawnEnemyMilitary.gameObject.GetComponentInChildren<ProjetPirate.Boat.BoatCharacter>())
            {
                foreach (Transform _child in spawnEnemyBoatMilitary[_index].transformSpawnEnemyMilitary)
                {
                    Destroy(_child.gameObject);
                }
            }
        }

        void ClearEnemyMerchantSpawner(int _index)
        {
            if (!spawnEnemyBoatMerchant[_index].transformSpawnEnemyMerchant.gameObject.GetComponentInChildren<ProjetPirate.Boat.BoatCharacter>())
            {
                foreach (Transform _child in spawnEnemyBoatMerchant[_index].transformSpawnEnemyMerchant)
                {
                    Destroy(_child.gameObject);
                }
            }
        }

        #endregion

    }

}
