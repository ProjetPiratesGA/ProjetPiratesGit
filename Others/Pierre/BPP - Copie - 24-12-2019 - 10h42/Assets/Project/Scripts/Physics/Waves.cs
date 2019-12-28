using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Waves : MonoBehaviour
{
    [SerializeField]
    private int _dimensions = 10;
    [SerializeField]
    private Octave[] _Octaves; // structure use to define the movement of the mesh
    [SerializeField]
    private float _UVScale;

    protected MeshFilter _meshFilter;
    protected Mesh _mesh;

    private int[] _tries;
    private Vector3[] _verts;
    private Vector2[] _UVs;

    /// <summary>
    /// Structure use to create Octave Water effect
    /// </summary>
    [Serializable]
    public struct Octave
    {
        public Vector2 speed;
        public Vector2 scale;
        public float height;
        public bool alternate;
    }

    // Use this for initialization
    void Start ()
    {
        //setup the mesh
        _mesh = new Mesh();
        _mesh.name = "GenerateMesh" + gameObject.name;

        _mesh.vertices = GenerateVerts();
        _mesh.triangles = GenerateTries();

        _mesh.uv = GenerateUVs();
        _mesh.RecalculateBounds();
        _mesh.RecalculateNormals();

        _meshFilter = gameObject.AddComponent<MeshFilter>();
        _meshFilter.mesh = _mesh;

	}

    // Update is called once per frame
    void Update()
    {
        this.ManipulateVertices();
    }

    /// <summary>
    /// all the manipulation of vertices must be done in this function
    /// - Manipulate the vertices of the mesh
    /// - use the Octave structure
    /// </summary>
    private void ManipulateVertices()
    {
        // Manipulate the vertices of the mesh
        _verts = _mesh.vertices;

        for (int x = 0; x <= _dimensions; x++)
        {
            for (int z = 0; z <= _dimensions; z++)
            {
                //set a height value for the vertices
                float y = 0f;

                for (int _indexOctave = 0; _indexOctave < _Octaves.Length; _indexOctave++)
                {
                    //for alteration on the height (TO VERIFY)
                    if (_Octaves[_indexOctave].alternate)
                    {
                        float perl = Mathf.PerlinNoise(
                            (x * _Octaves[_indexOctave].scale.x) / _dimensions,
                            (z * _Octaves[_indexOctave].scale.y) / _dimensions)
                            * Mathf.PI * 2f;
                        y += Mathf.Cos(perl + _Octaves[_indexOctave].speed.magnitude * Time.time) * _Octaves[_indexOctave].height;
                    }
                    //for alteration on the x & y (TO VERIFY)
                    else
                    {
                        // -0.5f at the end od the perl calcul because the "Mathf.PerlinNoise" function return a value between 0 & 1
                        // So i reduce the value by 0.5f to have the height of the wave between -0.5f & 0.5f
                        float perl = Mathf.PerlinNoise(
                            (x * _Octaves[_indexOctave].scale.x + Time.time * _Octaves[_indexOctave].speed.x) / _dimensions,
                            (z * _Octaves[_indexOctave].scale.y + Time.time * _Octaves[_indexOctave].speed.y) / _dimensions
                            ) - 0.5f;
                        y += perl * _Octaves[_indexOctave].height;
                    }
                }

                _verts[index(x, z)] = new Vector3(x, y, z);
            }
        }

        //apply the verts to the mesh vertices
        _mesh.vertices = _verts;

        //recalculate the normal map every time we update the vertices array
        //useful to see the shadow at each frame
        _mesh.RecalculateNormals();
    }

    /// <summary>
    /// Generate a verts array
    /// - Generate a new verts array
    /// - Distribute the verts equally in the array
    /// - Return the array
    /// </summary>
    /// <returns></returns>
    private Vector3[] GenerateVerts()
    {
        //Generate a new verts array
        _verts = new Vector3[(_dimensions + 1) * (_dimensions + 1)];

        //Distribute the verts equally in the array
        for (int x = 0; x <= _dimensions; x++)
        {
            for(int z = 0; z <= _dimensions; z++)
            {
                _verts[index(x, z)] = new Vector3(x, 0, z);
            }
        }

        //Return the verts array
        return _verts;
    }

    /// <summary>
    /// a voir car pas tout compris sur cette fonction
    /// </summary>
    /// <param name="x"></param>
    /// <param name="z"></param>
    /// <returns></returns>
    private int index(int x, int z)
    {
        return x * (_dimensions + 1) + z;
    }
    private int index(float x, float z)
    {
        return (int)(x * (_dimensions + 1) + z);
    }

    /// <summary>
    /// Need 6 points for a square because 1 square is 2 triangles & 1 triangle is made with 3 points
    /// 3*2 = 6 so we have 6 points for 1 square
    /// </summary>
    /// <returns></returns>
    private int[] GenerateTries()
    {
        _tries = new int[_mesh.vertices.Length * 6];

        //two triangles are one tile
        for (int x = 0; x < _dimensions; x++)
        {
            for (int z = 0; z < _dimensions; z++)
            {
                _tries[index(x, z) * 6 + 0] = index(x, z);
                _tries[index(x, z) * 6 + 1] = index(x + 1,z + 1);
                _tries[index(x, z) * 6 + 2] = index(x + 1, z);
                _tries[index(x, z) * 6 + 3] = index(x, z);
                _tries[index(x, z) * 6 + 4] = index(x, z + 1);
                _tries[index(x, z) * 6 + 5] = index(x + 1, z + 1);


            }
        }

        //Return the triangles array
        return _tries;
    }

    /// <summary>
    /// adjust the texture map to the wireframe.
    /// must be done to see the shadows effects on the water & waves
    /// - define the size of the "_UVs" array by the length of the "vertices" array.
    /// - Generate uv's for the mesh
    /// </summary>
    /// <returns></returns>
    private Vector2[] GenerateUVs()
    {
        // define the size of the "_UVs" array by the length of the "vertices" array.        
        _UVs = new Vector2[_mesh.vertices.Length];

        for (int x = 0; x <= _dimensions; x++)
        {
            for (int z = 0; z <= _dimensions; z++)
            {
                // Generate uv's for the mesh
                Vector2 _tempVec = new Vector2((x/_UVScale) % 2, (z/_UVScale) % 2);
                _UVs[index(x, z)] = new Vector2(
                    _tempVec.x <= 1 ? _tempVec.x : 2 - _tempVec.x,
                    _tempVec.y <= 1 ? _tempVec.y : 2 - _tempVec.y);
            }
        }

        return _UVs;
    }

    public float GetHeight(Vector3 pPosition)
    {
        //scale factor & position in local space
        Vector3 scale = new Vector3(1 / this.transform.lossyScale.x, 0, 1 / this.transform.lossyScale.z);
        Vector3 localPosition = Vector3.Scale((pPosition - this.transform.position), scale);

        //get the edges point
        Vector3 p1 = new Vector3(Mathf.Floor(localPosition.x), 0, Mathf.Floor(localPosition.z));
        Vector3 p2 = new Vector3(Mathf.Floor(localPosition.x), 0, Mathf.Ceil(localPosition.z));
        Vector3 p3 = new Vector3(Mathf.Ceil(localPosition.x), 0, Mathf.Floor(localPosition.z));
        Vector3 p4 = new Vector3(Mathf.Ceil(localPosition.x), 0, Mathf.Ceil(localPosition.z));

        //Do a clamp if the position is outside the plane
        p1.x = Mathf.Clamp(p1.x, 0, _dimensions);
        p1.z = Mathf.Clamp(p1.z, 0, _dimensions);
        p2.x = Mathf.Clamp(p2.x, 0, _dimensions);
        p2.z = Mathf.Clamp(p2.z, 0, _dimensions);
        p3.x = Mathf.Clamp(p3.x, 0, _dimensions);
        p3.z = Mathf.Clamp(p3.z, 0, _dimensions);
        p4.x = Mathf.Clamp(p4.x, 0, _dimensions);
        p4.z = Mathf.Clamp(p4.z, 0, _dimensions);

        //get the max distance to one of the edges and take that to compute max - dist
        float max = Mathf.Max(
            Vector3.Distance(p1, localPosition), Vector3.Distance(p2, localPosition),
            Vector3.Distance(p3, localPosition), Vector3.Distance(p4, localPosition) + Mathf.Epsilon);
        float dist = 
            (max - Vector3.Distance(p1, localPosition))
            + (max - Vector3.Distance(p2, localPosition))
            + (max - Vector3.Distance(p3, localPosition))
            + (max - Vector3.Distance(p4, localPosition) + Mathf.Epsilon);

        //weighted sum
        float height =
            _mesh.vertices[index(p1.x, p1.z)].y * (max - Vector3.Distance(p1, localPosition))
            + _mesh.vertices[index(p2.x, p2.z)].y * (max - Vector3.Distance(p2, localPosition))
            + _mesh.vertices[index(p3.x, p3.z)].y * (max - Vector3.Distance(p3, localPosition))
            + _mesh.vertices[index(p4.x, p4.z)].y * (max - Vector3.Distance(p4, localPosition));

        //scale
        return height * this.transform.lossyScale.y / dist;

    }

}
