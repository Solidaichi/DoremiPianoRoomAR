using System.Collections.Generic;
using UnityEngine;

public class Boid : MonoBehaviour
{
    public Simulation simulation { get; set; }
    public Param param { get; set; }
    public Vector3 pos { get; set; }
    public Vector3 velocity { get; private set; }

    [SerializeField]
    GameObject posObj;



    Vector3 accel = Vector3.zero;
    List<Boid> neighbors = new List<Boid>();

    void Start()
    {
        //Debug.Log(simulation);
        posObj = GameObject.FindGameObjectWithTag("domino");
        //Debug.Log("posobj : " + posObj.transform.position);
        pos = transform.position;
        Debug.Log("pos : " + pos);
        velocity = transform.forward * param.initSpeed;
    }

    void Update()
    {
        //pos = posObj.transform.position;
        pos = transform.position;
        // 近隣の個体を探して neighbors リストを更新
        UpdateNeighbors();

        // 壁に当たりそうになったら向きを変える
        UpdateWalls();

        // 近隣の個体から離れる
        UpdateSeparation();

        // 近隣の個体と速度を合わせる
        UpdateAlignment();

        // 近隣の個体の中心に移動する
        UpdateCohesion();

        // 上記 4 つの結果更新された accel を velocity に反映して位置を動かす
        UpdateMove();
    }

    private void UpdateCohesion()
    {
        if (neighbors.Count == 0) return;

        var averagePos = Vector3.zero;
        foreach (var neighbor in neighbors)
        {
            averagePos += neighbor.pos;
        }
        averagePos /= neighbors.Count;

        accel += (averagePos - pos) * param.cohesionWeight;
    }

    void UpdateMove()
    {
        var dt = Time.deltaTime;

        velocity += accel * dt;
        var dir = velocity.normalized;
        var speed = velocity.magnitude;
        velocity = Mathf.Clamp(speed, param.minSpeed, param.maxSpeed) * dir;
        pos += velocity * dt;

        var rot = Quaternion.LookRotation(velocity);
        transform.SetPositionAndRotation(pos, rot);

        accel = Vector3.zero;
        //Debug.Log("UpdateMove");
    }

    private void UpdateAlignment()
    {
        if (neighbors.Count == 0) return;

        var averageVelocity = Vector3.zero;
        foreach (var neighbor in neighbors)
        {
            averageVelocity += neighbor.velocity;
        }
        averageVelocity /= neighbors.Count;

        accel += (averageVelocity - velocity) * param.alignmentWeight;
    }

    private void UpdateSeparation()
    {
        if (neighbors.Count == 0) return;

        Vector3 force = Vector3.zero;
        foreach (var neighbor in neighbors)
        {
            force += (pos - neighbor.pos).normalized;
        }
        force /= neighbors.Count;

        accel += force * param.separationWeight;
    }

    private void UpdateWalls()
    {
        if (!simulation) { return; }

        var scale = param.wallScale * 0.5f;
        Debug.Log(scale);
        //float accelCalc = -scale - pos.x;
        //Debug.Log("AccelCalc : " + accelCalc);
        accel +=
            CalcAccelAgainstWall(-scale - pos.x, new Vector3(pos.x + 1, 0, 0)) +
            CalcAccelAgainstWall(-scale - pos.y, new Vector3(0, pos.y + 1, 0)) +
            CalcAccelAgainstWall(-scale - pos.z, new Vector3(0, 0, pos.z + 1)) +
            CalcAccelAgainstWall(+scale - pos.x, new Vector3(pos.x - 1, 0, 0)) +
            CalcAccelAgainstWall(+scale - pos.y, new Vector3(0, pos.y - 1, 0)) +
            CalcAccelAgainstWall(+scale - pos.z, new Vector3(0, 0, pos.z - 1));

        //Debug.Log("accel True : " + accel);
    }

    Vector3 CalcAccelAgainstWall(float distance, Vector3 dir)
    {
        Vector3 wallDir;
        Debug.Log("dir True : " + dir);
        if (distance < param.wallDistance)
        {
            wallDir = dir * (param.wallWeight / Mathf.Abs(distance / param.wallDistance));
            //Debug.Log("wallDir True : " + wallDir);
            return wallDir;
        }
        return Vector3.zero;
    }

    private void UpdateNeighbors()
    {
        neighbors.Clear();

        if (!simulation) return;

        var prodThresh = Mathf.Cos(param.neighborFov * Mathf.Deg2Rad);
        var distThresh = param.neighborDistance;

        foreach (var other in simulation.boids)
        {
            if (other == this) continue;

            var to = other.pos - pos;
            var dist = to.magnitude;
            if (dist < distThresh)
            {
                var dir = to.normalized;
                var fwd = velocity.normalized;
                var prod = Vector3.Dot(fwd, dir);
                if (prod > prodThresh)
                {
                    neighbors.Add(other);
                }
            }
        }
    }
}
