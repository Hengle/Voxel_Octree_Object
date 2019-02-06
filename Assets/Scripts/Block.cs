﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct Block { 
    public ushort collection;
    public ushort itemcode;
    public bool opaque;
    public bool translucent; 
    public string materialfile;

    public Block (ushort col, ushort cod, bool opaq, bool trans, string mfile)
    {
        this.collection = col;
        this.itemcode = cod;
        this.opaque = opaq;
        this.translucent = trans;
        this.materialfile = mfile;
    }

}