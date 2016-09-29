using UnityEngine;
using System.Collections;

public class PaddleScript : MonoBehaviour {


    private HingeJoint2D hj;

    // Use this for initialization
    void Start () {
        hj = GetComponent<HingeJoint2D>();
    }

    public void ActivateMotor() {
        hj.useMotor = true;
    }

    public void DeactivateMotor() {
        hj.useMotor = false;
    }

    public void ActivateLimits() {
        hj.useLimits = true;
    }

    public void DeactivateLimits() {
        hj.useLimits = false;
    }

    public void SetAnglePosition(int pos) {
        SetLimits(pos, pos);
    }

    public void SetSpeed(int speed) {
        JointMotor2D motor = new JointMotor2D();
        motor.motorSpeed = speed;
        motor.maxMotorTorque = hj.motor.maxMotorTorque;

        hj.motor = motor;
    }

    public void SetAcceleration(int acceleration) {
        JointMotor2D motor = new JointMotor2D();
        motor.motorSpeed = hj.motor.motorSpeed;
        motor.maxMotorTorque = acceleration;

        hj.motor = motor;
    }

    public void SetLimits(int min, int max) {
        if (min < 0 || min > 360 || max < 0 || max > 360) {
            return;
        }

        JointAngleLimits2D jl = new JointAngleLimits2D();

        jl.min = min;
        jl.max = max;

        hj.limits = jl;
    }

}
