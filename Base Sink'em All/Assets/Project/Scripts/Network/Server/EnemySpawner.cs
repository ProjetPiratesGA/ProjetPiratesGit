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
        GameObject krakenHeadEnemy;
        [SerializeField]
        List<GameObject> krakenTentaclesEnemy;

        [SerializeField]
        private List<Transform> posEnemyBoat;

        [System.Serializable]
        public struct structSpawnEnemyShark
        {

            public Transform posSpawnEnemyShark;
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
        }

        [System.Serializable]
        public struct structSpawnEnemyKraken
        {
            public Transform posSpawnEnemyKrakenHead;
            public List<Transform> posSpawnEnemyKrakenTentacles;
        }

        [SerializeField]
        public List<structSpawnEnemyShark> spawnEnemyShark = new List<structSpawnEnemyShark>();



        [SerializeField]
        public structSpawnEnemyKraken spawnEnemyKraken;

        int minTimeSpawn = 2;
        int maxTimeSpawn = 6;

        // Use this for initialization
        void Start()
        {
            //InitSpawnAllEnemy();
            for (int i = 0; i < spawnEnemyShark.Count; i++)
            {
                var spawnEnemySharkBuffer = spawnEnemyShark[i];
                spawnEnemySharkBuffer.timeToSpawn = Random.Range(minTimeSpawn, maxTimeSpawn);
                spawnEnemyShark[i] = spawnEnemySharkBuffer;
            }
        }
        // Update is called once per frame
        void Update()
        {

            if (Input.GetKeyDown("m"))
            {
                SpawnEnemyBoat();
            }
            if (Input.GetKeyDown("p"))
            {
                SpawnEnemyKraken();
            }

            //Debug.LogError("Shark Enemy Size List : " + listEnemyShark.Count);
            if (Input.GetKeyDown("l"))
            {

                SpawnEnemyShark();
            }

            UpdateSpawnUtilisation();

        }

        void SpawnEnemyBoat()
        {
            for (int i = 0; i < posEnemyBoat.Count; i++)
            {
                int randomSpawn = Random.Range(0, 3);
                GameObject enemyBoatInstance = null;
                if (randomSpawn == 0)
                {
                    enemyBoatInstance = Instantiate(militaryEnemy);
                }
                else if (randomSpawn == 1)
                {
                    enemyBoatInstance = Instantiate(merchantEnemy);
                }
                else
                {
                    break;
                }

                if (enemyBoatInstance != null)
                {
                    enemyBoatInstance.gameObject.transform.position = posEnemyBoat[i].position;
                    NetworkServer.Spawn(enemyBoatInstance);
                }

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
                        GameObject enemySpawn = Instantiate(sharkEnemy, spawnEnemyShark[i].posSpawnEnemyShark);

                        enemySpawn.gameObject.transform.position = spawnEnemyShark[i].posSpawnEnemyShark.position;

                        //Instantiate Enemy Patrol and Set Position
                        GameObject enemyPatrol = Instantiate(sharkPatrol, spawnEnemyShark[i].posSpawnEnemyShark);
                        enemyPatrol.gameObject.transform.position = spawnEnemyShark[i].posSpawnEnemyShark.position;

                        GameObject enemyPatrolWander = Instantiate(patrolWander, spawnEnemyShark[i].posSpawnEnemyShark);
                        enemyPatrolWander.gameObject.transform.position = spawnEnemyShark[i].posSpawnEnemyShark.position;

                        //Set Transform Patrol to Script Enemy
                        enemySpawn.gameObject.GetComponent<ProjetPirate.IA.Patrol_Wander>()._center = enemyPatrol.transform.Find("Center");
                        enemySpawn.gameObject.GetComponent<ProjetPirate.IA.Patrol_Wander>()._destination = enemyPatrol.transform.Find("Objectif");

                        Debug.LogError("Try to Set Detector");
                        enemyPatrolWander.GetComponent<ProjetPirate.IA.Detector>()._enemies[0] = enemySpawn.GetComponent<ProjetPirate.IA.Shark_Controller>();

                        NetworkServer.Spawn(enemyPatrol);
                        NetworkServer.Spawn(enemyPatrolWander);
                        NetworkServer.Spawn(enemySpawn);

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
            int randomSpawn;
            randomSpawn = Random.Range(0, 101);
            GameObject enemyKrakenHeadInstance;
            List<GameObject> listEnemyKrakenTentaclesInstance = new List<GameObject>();
            //if(Check Zone);
            if (randomSpawn >= 0 && randomSpawn <= 30)
            {
                enemyKrakenHeadInstance = Instantiate(krakenHeadEnemy);
                for (int i = 0; i < krakenTentaclesEnemy.Count; i++)
                {
                    listEnemyKrakenTentaclesInstance.Add(new GameObject());
                    listEnemyKrakenTentaclesInstance[i] = Instantiate(krakenTentaclesEnemy[i]);
                }
            }
            //if(Check Zone);
            if (randomSpawn >= 0 && randomSpawn <= 70)
            {
                enemyKrakenHeadInstance = Instantiate(krakenHeadEnemy);
                for (int i = 0; i < krakenTentaclesEnemy.Count; i++)
                {
                    listEnemyKrakenTentaclesInstance.Add(new GameObject());
                    listEnemyKrakenTentaclesInstance[i] = Instantiate(krakenTentaclesEnemy[i]);
                }
            }
        }

        void InitSpawnAllEnemy()
        {
            #region Shark
            for (int i = 0; i < spawnEnemyShark.Count; i++)
            {
                //Instantiate Enemy and Set Position

                GameObject enemySpawn = Instantiate(sharkEnemy, spawnEnemyShark[i].posSpawnEnemyShark);
                enemySpawn.gameObject.transform.position = spawnEnemyShark[i].posSpawnEnemyShark.position;

                //Instantiate Enemy Patrol and Set Position
                GameObject enemyPatrol = Instantiate(sharkPatrol, spawnEnemyShark[i].posSpawnEnemyShark);
                enemyPatrol.gameObject.transform.position = spawnEnemyShark[i].posSpawnEnemyShark.position;

                GameObject enemyPatrolWander = Instantiate(patrolWander, spawnEnemyShark[i].posSpawnEnemyShark);
                enemyPatrolWander.gameObject.transform.position = spawnEnemyShark[i].posSpawnEnemyShark.position;

                //Set Transform Patrol to Script Enemy
                enemySpawn.gameObject.GetComponent<ProjetPirate.IA.Patrol_Wander>()._center = enemyPatrol.transform.Find("Center");
                enemySpawn.gameObject.GetComponent<ProjetPirate.IA.Patrol_Wander>()._destination = enemyPatrol.transform.Find("Objectif");

                Debug.LogError("Try to Set Detector");
                enemyPatrolWander.GetComponent<ProjetPirate.IA.Detector>()._enemies[0] = enemySpawn.GetComponent<ProjetPirate.IA.Shark_Controller>();

                //NetworkServer.Spawn(enemyPatrol);
                //NetworkServer.Spawn(enemyPatrolWander);
                NetworkServer.Spawn(enemySpawn);


                var spawnEnemySharkBuffer = spawnEnemyShark[i];
                spawnEnemySharkBuffer.spawnIsUsed = true;
                spawnEnemyShark[i] = spawnEnemySharkBuffer;
            }
            #endregion
        }

        void UpdateSpawnUtilisation()
        {
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
                        spawnEnemySharkBuffer.timeToSpawn = Random.Range(minTimeSpawn, maxTimeSpawn);
                        spawnEnemySharkBuffer.lockRandom = true;
                    }

                }


                spawnEnemyShark[i] = SetSpawnIsUsed(spawnEnemySharkBuffer);
                ClearEnemySpawner(i);
            }
        }

        structSpawnEnemyShark SetSpawnIsUsed(structSpawnEnemyShark _SpawnShark)
        {


            if (_SpawnShark.posSpawnEnemyShark.gameObject.GetComponentInChildren<ProjetPirate.IA.Shark_Character>())
            {
                _SpawnShark.spawnIsUsed = true;
            }
            else
            {

                _SpawnShark.spawnIsUsed = false;
                //Destroy All GameObject Related to the Enemy Shark (Detctor and SharkPatrol)
            }

            return _SpawnShark;
        }

        void ClearEnemySpawner(int _index)
        {

            if (!spawnEnemyShark[_index].posSpawnEnemyShark.gameObject.GetComponentInChildren<ProjetPirate.IA.Shark_Character>())
            {
                foreach (Transform _child in spawnEnemyShark[_index].posSpawnEnemyShark)
                {
                    Destroy(_child.gameObject);

                }
            }
        }

    }

}
