﻿using UnityEngine;
using System.Linq;
using System.Collections;
using Assets.Scripts;
using System;

public class RailScript : MonoBehaviour, IButtonPress {

    public float ChangeWidth;
    public float MoveSpeed;

    private bool _translate;
    private float _increment;

    void Start()
    {
        this._increment = 0;
    }

    public bool Trigger()
    {
        this._translate = true;
        return true;
    }

    public bool UnTrigger()
    {
        this._translate = false;
        return true;
    }

    void FixedUpdate()
    {
        if (_translate && _increment != ChangeWidth)
        {
            this.transform.Translate(Vector2.right * MoveSpeed);
            _increment++;
        }
        else if (!_translate && _increment != 0)
        {
            this.transform.Translate(Vector2.left * MoveSpeed);
            _increment--;
        }
    }
}
