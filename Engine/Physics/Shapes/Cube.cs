﻿// SEVENENGINE LISCENSE:
// You are free to use, modify, and distribute any or all code segments/files for any purpose
// including commercial use with the following condition: any code using or originally taken 
// from the SevenEngine project must include citation to its original author(s) located at the
// top of each source code file, or you may include a reference to the SevenEngine project as
// a whole but you must include the current SevenEngine official website URL and logo.
// - Thanks.  :)  (support: seven@sevenengine.com)

// Author(s):
// - Zachary Aaron Patten (aka Seven) seven@sevenengine.com
// Last Edited: 11-16-13

namespace SevenEngine.Physics.Primitives
{
  public class Cube
  {
    float _x, _y, _z, _scale;

    public float X { get { return _x; } set { _x = value; } }
    public float Y { get { return _y; } set { _y = value; } }
    public float Z { get { return _z; } set { _z = value; } }
    public float Scale { get { return _scale; } set { _scale = value; } }

    public float MinimumX { get { return _x - _scale; } }
    public float MaximumX { get { return _x + _scale; } }
    public float MinimumY { get { return _y - _scale; } }
    public float MaximumY { get { return _y + _scale; } }
    public float MinimumZ { get { return _z - _scale; } }
    public float MaximumZ { get { return _z + _scale; } }

    public Cube(float x, float y, float z, float scale)
    { _x = x; _y = y; _z = z; _scale = scale; }
  }
}