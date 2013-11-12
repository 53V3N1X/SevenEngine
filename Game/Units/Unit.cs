﻿using System;

using SevenEngine.DataStructures;
using SevenEngine.Models;
using SevenEngine.Mathematics;
using SevenEngine.DataStructures.Interfaces;

namespace Game.Units
{
  public abstract class Unit : InterfacePositionVector, InterfaceStringId
  {
    protected string _id;
    protected int _health;
    protected int _damage;
    protected float _viewDistance;
    protected float _moveSpeed;
    protected StaticModel _staticModel;
    protected bool _isDead;

    public string Id { get { return _id; } set { _id = value; } }
    public int Health { get { return _health; } set { _health = value; } }
    public int Damage { get { return _damage; } set { _damage = value; } }
    public float ViewDistance { get { return _viewDistance; } set { _viewDistance = value; } }
    public float MoveSpeed { get { return _moveSpeed; } set { _moveSpeed = value; } }
    public StaticModel StaticModel { get { return _staticModel; } set { _staticModel = value; } }
    public Vector Position { get { return _staticModel.Position; } set { _staticModel.Position = value; } }
    public bool IsDead { get { return _isDead; } }
    
    public Unit(string id, StaticModel staticModel)
    {
      _id = id;
      _staticModel = staticModel;
      _isDead = false;
    }

    public virtual void AI(ListArray<Unit> unitsInView) { }
  }
}