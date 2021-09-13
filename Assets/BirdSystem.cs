using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;


public class BirdSystem : SystemBase
{
    const float GRAVITY = -9.8f * 10f;

    protected override void OnUpdate()
    {
        float dt = Time.DeltaTime;

        float3 tr = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));
        float3 bl = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0));

        Entities.ForEach((ref Translation translation, ref Bird bird) =>
        {
            translation.Value.xy += bird.Velocity * dt;
            bird.Velocity.y += GRAVITY * dt;
        }).ScheduleParallel();

        Entities.ForEach((ref Bird bird, in Translation translation) =>
        {
            var vel = bird.Velocity;
            var p = translation.Value.xy;

            if( (vel.x > 0 && p.x + 0.5f > tr.x)
             || (vel.x <= 0 && p.x - 0.5f < bl.x) )
            {
                vel.x = -vel.x;
            }

            if ( (vel.y < 0 && p.y - 0.5f < bl.y)
             || (vel.y >= 0 && p.y - 0.5f > tr.y) )
            {
                vel.y = -vel.y;
            }

            bird.Velocity = vel;
        }).ScheduleParallel();
    }
}
