using UnityEngine;
using System.Collections;
using Assets.Scripts;
using System;

public class PaddleScript : MonoBehaviour, IButtonPress {


    private HingeJoint2D hj;

    public int HitPwr;

    // Use this for initialization
    void Start () {
        hj = GetComponent<HingeJoint2D>();
    }

    protected void ActivateMotor() {
        hj.useMotor = true;
    }

    protected void DeactivateMotor() {
        hj.useMotor = false;
    }

    protected void ActivateLimits() {
        hj.useLimits = true;
    }

    protected void DeactivateLimits() {
        hj.useLimits = false;
    }

    protected void SetAnglePosition(int pos) {
        SetLimits(pos, pos);
    }

    protected void SetSpeed(int speed) {
        JointMotor2D motor = new JointMotor2D();
        motor.motorSpeed = speed;
        motor.maxMotorTorque = hj.motor.maxMotorTorque;

        hj.motor = motor;
    }

    protected void SetAcceleration(int acceleration) {
        JointMotor2D motor = new JointMotor2D();
        motor.motorSpeed = hj.motor.motorSpeed;
        motor.maxMotorTorque = acceleration;

        hj.motor = motor;
    }

    protected void SetLimits(int min, int max) {
        if (min < 0 || min > 360 || max < 0 || max > 360) {
            return;
        }

        JointAngleLimits2D jl = new JointAngleLimits2D();

        jl.min = min;
        jl.max = max;

        hj.limits = jl;
    }

    public bool Trigger() {
        this.SetSpeed(-HitPwr);
        return true;
    }

    public bool UnTrigger() {
        this.SetSpeed(HitPwr);
        return true;
    }
}
