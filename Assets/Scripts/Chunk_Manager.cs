﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// TODO: Debating Whether to add seed to this or world?

public class Chunk_Manager : MonoBehaviour
{
    //World Parameters
    private int worldSeed;

    //World Boundaries - Null if no boundary exists.
    private bool isBoundaries;
    private float[] boundaries = { float.PositiveInfinity, float.PositiveInfinity, float.PositiveInfinity, float.PositiveInfinity };

    //Chunk Parameters
    private int chunkSize;
    private byte chunkMaxDepth;
    private int blockMinSize;


    //Dictionary of Chunks that is currently loaded into the game.
    public Dictionary<Vector3Int, GameObject> chunkPool = new Dictionary<Vector3Int, GameObject>();
    
    //Block Manager for Block Details - Mainly to hand off as reference to chunk
    private Block_Manager blockManager;


    private void Awake()
    {
        blockMinSize = (int)(chunkSize / (Mathf.Pow(2, chunkMaxDepth)));

        //TODO: Remove Tests
        //Testing Chunks
        
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        

    }

    public void SetChunkManager(int a_worldseed, int a_chunksize, byte a_chunkmaxdepth, Block_Manager a_blockmanager)
    {
        //Set Procedural Seed
        worldSeed = a_worldseed;

        //Set Chunk Parameters
        chunkSize = a_chunksize;
        chunkMaxDepth = a_chunkmaxdepth;

        //Set Block Manager
        blockManager = a_blockmanager;
    }

    private GameObject ChunkSetUp(Vector3 a_pos)
    {
        GameObject chunk = new GameObject();
        chunk.AddComponent(typeof(MeshFilter));
        chunk.AddComponent(typeof(MeshRenderer));
        chunk.AddComponent<Octree_Controller>();
        chunk.GetComponent<Octree_Controller>().octreeSize = chunkSize;
        chunk.name = "Chunk" + Octree_Controller.count.ToString();
        chunk.transform.position = a_pos;
        return chunk;
    }

    private void AddChunk(Vector3 a_pos)
    {
        chunkPool.Add(new Vector3Int((int)a_pos.x, (int)a_pos.y, (int)a_pos.z), ChunkSetUp(a_pos));
    }

    public void AddBlock(Vector3 a_pos, int type)
    {
        Vector3 targetVec3 = new Vector3((float)Math.Floor(a_pos.x / chunkSize) * chunkSize, (float)Math.Floor(a_pos.y / chunkSize) * chunkSize, (float)Math.Floor(a_pos.z / chunkSize) * chunkSize);
        Vector3Int targetkey = new Vector3Int((int)targetVec3.x, (int)targetVec3.y, (int)targetVec3.z);
        if (chunkPool.ContainsKey(targetkey) == true)
        {
            chunkPool[targetkey].GetComponent<Octree_Controller>().AddNodeAbsPos(a_pos, chunkMaxDepth, type);
        }
        else
        {
            AddChunk(new Vector3((float)Math.Floor(a_pos.x / chunkSize) * chunkSize, (float)Math.Floor(a_pos.y / chunkSize) * chunkSize, (float)Math.Floor(a_pos.z / chunkSize) * chunkSize));
            chunkPool[targetkey].GetComponent<Octree_Controller>().AddNodeAbsPos(a_pos, chunkMaxDepth, type);
        }
    }

}
