using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Match2Game : MonoBehaviour, IMiniGame
{
    [SerializeField] private List<EP_Node> _row1;
    [SerializeField] private List<EP_Node> _row2;
    [SerializeField] private TMP_Text _levelDisplay;

    private LevelData _levelData;
    private EP_Node _node1, _node2;

    public void Init(LevelData lvData)
    {
        for (int i = 0; i < _row1.Count; i++)
        {
            _row1[i].Setup(_levelData.NodeData[i]);
            _row1[i].onClick += ClickNode;
        }

        for (int i = 0; i < _row2.Count; i++)
        {
            int index = i + _row1.Count;
            _row2[i].Setup(_levelData.NodeData[index]);
            _row2[i].onClick += ClickNode;
        }
    }

    public void ClickNode(EP_Node node)
    {
        if(_node1 == null)
        {
            _node1 = node;
            _node1.SetupNode1();
        }
        else if(node.RowId != _node1.RowId)
        {
            _node2 = node;
            _node1.ConnectNode2(_node2);
            _node2._oppId = _node1.MatchId;
            if(CheckConnectAll())
            {
                if (CheckWin())
                    Win();
                else
                    Lose();
            }
            _node1 = null;
            _node2 = null;
        }
    }

    private bool CheckConnectAll()
    {
        foreach (var node in _row1)
        {
            if (!node.IsConnect())
                return false;
        }

        foreach (var node in _row2)
        {
            if (!node.IsConnect())
                return false;
        }

        return true;
    }

    public bool CheckWin()
    {
        foreach (var node in _row1)
        {
            if (!node.IsCorrect())
                return false;
        }

        foreach (var node in _row2)
        {
            if (!node.IsCorrect())
                return false;
        }

        return true;
    }

    public void Win()
    {
        Debug.Log("Win");
    }

    public void Lose()
    {
        Debug.Log("Lose");
    }
}
