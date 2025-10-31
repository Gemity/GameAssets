using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum EP_NodeState
{
    Notset,  // Gần như không dùng đến
    Idle,    // Có thể click
    Cacul,   // Đợi click vào node thứ 2
    Result   // Đã nối node 1 vào node 2
}

public class EP_Node : MonoBehaviour
{
    [SerializeField] private GameObject _borderGreen, _borderRed;
    [SerializeField] private SpriteRenderer _content;
    [SerializeField] private LineRenderer _line;
    [SerializeField] private Gradient _gradientGreen, _gradientRed;
    [SerializeField] private int _rowId;

    public int _matchId;
    public int _oppId = -1;

    public int MatchId => _matchId;
    private EP_NodeState _state;
    public int RowId => _rowId;

    public Action<EP_Node> onClick;

    public void Setup(NodeData data)
    {
        _content.sprite = data.Spr;
        _matchId = data.MatchId;
        _state = EP_NodeState.Idle;
    }

    private void OnMouseDown()
    {
        if (_state != EP_NodeState.Idle)
            return;

        onClick?.Invoke(this);
    }

    public void SetupNode1()
    {
        _borderGreen.SetActive(true);
        _line.gameObject.SetActive(true);
    }

    public void ConnectNode2(EP_Node node)
    {
        _line.SetPosition(0, transform.position);
        _line.SetPosition(1, node.transform.position);
        _line.colorGradient = _gradientGreen;
        _oppId = node.MatchId;
    }

    public bool IsCorrect()
    {
        return _matchId == _oppId;
    }

    public bool IsConnect()
    {
        return _oppId != -1;
    }
}
