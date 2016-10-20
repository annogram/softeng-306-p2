using UnityEngine;
using System.Collections;
using Assets.Scripts;
using System;
using Managers;

/// <summary>
/// This class is responsible for the Paddle logic
/// </summary>

public class PaddleScript : MonoBehaviour, IButtonPress {

    private HingeJoint2D hj;
    private GameController _gameController;
    private AudioSource _paddleAudio;

    public int HitPwr;
    public AudioClip PaddleWhackClip;

    // Use this for initialization
    void Start () {
        _gameController = GameController.Instance;
        hj = GetComponent<HingeJoint2D>();
        _paddleAudio = GetComponent<AudioSource>();
        _paddleAudio.clip = PaddleWhackClip;
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
        _paddleAudio.volume = _gameController.GetSFXVolume();
        _paddleAudio.Play();
        this.SetSpeed(-HitPwr);
        return true;
    }

    public bool UnTrigger() {
        this.SetSpeed(HitPwr);
        return true;
    }
}
