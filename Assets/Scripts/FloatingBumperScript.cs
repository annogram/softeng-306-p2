using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

class FloatingBumperScript : BumperScript{
    public float ChangeHeight;
    public float moveSpeed;

    private bool _translate;
    private Transform _startingPos;

    protected override void Start() {
        base.Start();
        this._startingPos = this.transform;
    }

    void FixedUpdate() {
        if (_translate) {

        }
    }

    public override bool Trigger() {
        this._translate = true;
        return base.Trigger();
    }
}

