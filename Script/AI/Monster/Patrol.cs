using System.Collections.Generic;
using UnityEngine;

public class Patrol
{
    private List<Vector3> PatrolPositionList { get; set; }
    public Transform[] PatrolTransforms { get; set; }

    private Monster _monster;
    private int _patrolPositionsIndex = 0;

    public Patrol(Monster monster)
    {
        _monster = monster;
        PatrolPositionList = new();
    }

    public void SetPatrolPositions(bool isKeep)
    {
        if (isKeep)
        {
            for (int i = 1; i < PatrolTransforms.Length; i++)
            {
                PatrolPositionList.Add(PatrolTransforms[i].position);
            }
            PatrolTransforms = null;
            SufflePositions(PatrolPositionList);
        }
        else SufflePositions(PatrolTransforms);
    }

    public Vector3 GetPatrolPosition(bool isKeep)
    {
        if (isKeep)
        {
            if (_patrolPositionsIndex < PatrolPositionList.Count)
            {
                _patrolPositionsIndex++;
                return PatrolPositionList[_patrolPositionsIndex - 1];
            }
            else
            {
                SufflePositions(PatrolPositionList);
                _patrolPositionsIndex = 0;
                _patrolPositionsIndex++;
                return PatrolPositionList[_patrolPositionsIndex - 1];
            }
        }
        else
        {
            if (_patrolPositionsIndex < PatrolTransforms.Length)
            {
                _patrolPositionsIndex++;
                return PatrolTransforms[_patrolPositionsIndex - 1].position;
            }
            else
            {
                SufflePositions(PatrolTransforms);
                _patrolPositionsIndex = 0;
                _patrolPositionsIndex++;
                return PatrolTransforms[_patrolPositionsIndex - 1].position;
            }
        }
    }

    private void SufflePositions(List<Vector3> datas)
    {
        for (int i = 0; i < datas.Count; i++)
        {
            int j = UnityEngine.Random.Range(0, i + 1);
            (datas[i], datas[j]) = (datas[j], datas[i]);
        }
    }

    private void SufflePositions(Transform[] datas)
    {
        for (int i = 0; i < datas.Length; i++)
        {
            int j = UnityEngine.Random.Range(0, i + 1);
            (datas[i], datas[j]) = (datas[j], datas[i]);
        }
    }
}
