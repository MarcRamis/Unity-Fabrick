using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class IK_FABRIK2 : MonoBehaviour
{
    public Transform[] joints;
    public Transform target;

    private Vector3[] copy;
    private float[] distances;
    private bool done;

    void Start()
    {
        distances = new float[joints.Length - 1];
        copy = new Vector3[joints.Length];
        for (int i = 0; i < joints.Length - 1; i++)
        {

            distances[i] = Vector3.Distance(joints[i].position, joints[i + 1].position);

        }
    }

    void Update()
    {
        // Copy the joints positions to work with
        //TODO
        for(int i = 0; i < joints.Length; i++)
        {
            copy[i] = joints[i].position;
        }
        
        //done = TODO
        if (!done)
        {
            float targetRootDist = Vector3.Distance(copy[0], target.position);

            // Update joint positions
            if (targetRootDist > distances.Sum())
            {
                // The target is unreachable
                Debug.Log(distances.Sum());
                Debug.Log(targetRootDist);
            }
            else
            {
                // The target is reachable
                //while (TODO)
                while(Vector3.Distance(copy[joints.Length - 1], target.position) > 0.1f)
                {
                    // STAGE 1: FORWARD REACHING
                    //TODO
                    for(int i = copy.Length - 1; i >= 0; i--)
                    {
                        if(i == copy.Length - 1)
                        {
                            copy[i] = target.position;
                        }
                        else
                        {
                            copy[i] = Vector3.Normalize(copy[i] - copy[i + 1]) * Vector3.Distance(joints[i].position, joints[i + 1].position) + copy[i + 1];
                        }
                    }

                    // STAGE 2: BACKWARD REACHING
                    //TODO
                    for (int i = 0; i < copy.Length; i++)
                    {
                        if (i == 0)
                        {
                            copy[i] = joints[0].position;
                        }
                        else
                        {
                            copy[i] = Vector3.Normalize(copy[i] - copy[i - 1]) * Vector3.Distance(joints[i].position, joints[i - 1].position) + copy[i - 1];
                        }
                    }
                }
                //for(int i = 0; i < joints.Length; i++)
                //{
                //    joints[i].position = copy[i];
                //}
            }

            // Update original joint rotations
            for (int i = 0; i <= joints.Length - 2; i++)
            {
                //TODO 
                Vector3 vector1 = Vector3.Normalize(joints[i + 1].position - joints[i].position);
                Vector3 vector2 = Vector3.Normalize(copy[i + 1] - copy[i]);
                float angle = Vector3.Angle(vector1, vector2);
                Vector3 axis = Vector3.Cross(vector1, vector2);
                //joints[i].position = copy[i];
                joints[i].transform.Rotate(axis, angle, Space.World); 
            }
            //joints[joints.Length - 1].position = copy[joints.Length - 1];
        }
    }

}
