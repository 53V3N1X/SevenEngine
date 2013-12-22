﻿// SEVENENGINE LISCENSE:
// You are free to use, modify, and distribute any or all code segments/files for any purpose
// including commercial use under the following condition: any code using or originally taken 
// from the SevenEngine project must include citation to its original author(s) located at the
// top of each source code file, or you may include a reference to the SevenEngine project as
// a whole but you must include the current SevenEngine official website URL and logo.
// - Thanks.  :)  (support: seven@sevenengine.com)

// Author(s):
// - Zachary Aaron Patten (aka Seven) seven@sevenengine.com
// Last Edited: 11-16-13

using System;

namespace SevenEngine.Mathematics
{
  /// <summary>An optimized matrix class for 3x3 tranfromation matrices only.</summary>
  public struct Matrix3x3
  {
    private float
      _r0c0, _r0c1, _r0c2,
      _r1c0, _r1c1, _r1c2,
      _r2c0, _r2c1, _r2c2;

    public float this[int row, int column]
    {
      get
      {
        switch (row)
        { case 0:
            switch (column)
            { case 0: return _r0c0;
              case 1: return _r0c1;
              case 2: return _r0c2; }
            break;
          case 1:
            switch (column)
            { case 0: return _r1c0;
              case 1: return _r1c1;
              case 2: return _r1c2; }
            break;
          case 2:
            switch (column)
            { case 0: return _r2c0;
              case 1: return _r2c1;
              case 2: return _r2c2; }
            break;
        }
        throw new MatrixException("index out of range.");
      }
      set
      {
        switch (row)
        { case 0:
            switch (column)
            { case 0: _r0c0 = value; return;
              case 1: _r0c1 = value; return;
              case 2: _r0c2 = value; return; }
            break;
          case 1:
            switch (column)
            { case 0: _r1c0 = value; return;
              case 1: _r1c1 = value; return;
              case 2: _r1c2 = value; return; }
            break;
          case 2:
            switch (column)
            { case 0: _r2c0 = value; return;
              case 1: _r2c1 = value; return;
              case 2: _r2c2 = value; return; }
            break;
        }
        throw new MatrixException("index out of range.");
      }
    }

    public Matrix3x3(
      float r0c0, float r0c1, float r0c2,
      float r1c0, float r1c1, float r1c2,
      float r2c0, float r2c1, float r2c2)
    {
      _r0c0 = r0c0; _r0c1 = r0c1; _r0c2 = r0c2;
      _r1c0 = r1c0; _r1c1 = r1c1; _r1c2 = r1c2;
      _r2c0 = r2c0; _r2c1 = r2c1; _r2c2 = r2c2;
    }

    public Matrix3x3(float[,] floatArray)
    {
      if (floatArray == null)
        throw new MatrixException("Attempting to create a matrix with an null float[,].");
      else if (floatArray.GetLength(0) != 3)
        throw new MatrixException("Attempting to create a matrix with an invalid sized float[,].");
      else if (floatArray.GetLength(1) != 3)
        throw new MatrixException("Attempting to create a matrix with an invalid sized float[,].");
      _r0c0 = floatArray[0, 0]; _r0c1 = floatArray[0, 1]; _r0c2 = floatArray[0, 2];
      _r1c0 = floatArray[1, 0]; _r1c1 = floatArray[1, 1]; _r1c2 = floatArray[1, 2];
      _r2c0 = floatArray[2, 0]; _r2c1 = floatArray[2, 1]; _r2c2 = floatArray[2, 2];
    }

    public static Matrix3x3 FactoryZero = new Matrix3x3(0, 0, 0, 0, 0, 0, 0, 0, 0);
    public static Matrix3x3 FactoryIdentity = new Matrix3x3(1, 0, 0, 0, 1, 0, 0, 0, 1);

    /// <param name="angle">Angle of rotation in radians.</param>
    public static Matrix3x3 FactoryRotationX(float angle)
    {
      float cos = Calc.Cos(angle);
      float sin = Calc.Sin(angle);
      return new Matrix3x3(1, 0, 0, 0, cos, sin, 0, -sin, cos);
    }

    /// <param name="angle">Angle of rotation in radians.</param>
    public static Matrix3x3 FactoryRotationY(float angle)
    {
      float cos = Calc.Cos(angle);
      float sin = Calc.Sin(angle);
      return new Matrix3x3(cos, 0, -sin, 0, 1, 0, sin, 0, cos);
    }

    /// <param name="angle">Angle of rotation in radians.</param>
    public static Matrix3x3 FactoryRotationZ(float angle)
    {
      float cos = Calc.Cos(angle);
      float sin = Calc.Sin(angle);
      return new Matrix3x3(cos, -sin, 0, sin, cos, 0, 0, 0, 1);
    }

    /// <param name="angleX">Angle about the X-axis in radians.</param>
    /// <param name="angleY">Angle about the Y-axis in radians.</param>
    /// <param name="angleZ">Angle about the Z-axis in radians.</param>
    public static Matrix3x3 FactoryRotationXthenYthenZ(float angleX, float angleY, float angleZ)
    {
      float
        xCos = Calc.Cos(angleX), xSin = Calc.Sin(angleX),
        yCos = Calc.Cos(angleY), ySin = Calc.Sin(angleY),
        zCos = Calc.Cos(angleZ), zSin = Calc.Sin(angleZ);
      return new Matrix3x3(
        yCos * zCos, -yCos * zSin, ySin,
        xCos * zSin + xSin * ySin * zCos, xCos * zCos + xSin * ySin * zSin, -xSin * yCos,
        xSin * zSin - xCos * ySin * zCos, xSin * zCos + xCos * ySin * zSin, xCos * yCos);
    }

    /// <param name="angleX">Angle about the X-axis in radians.</param>
    /// <param name="angleY">Angle about the Y-axis in radians.</param>
    /// <param name="angleZ">Angle about the Z-axis in radians.</param>
    public static Matrix3x3 FactoryRotationZthenYthenX(float angleX, float angleY, float angleZ)
    {
      float
        xCos = Calc.Cos(angleX), xSin = Calc.Sin(angleX),
        yCos = Calc.Cos(angleY), ySin = Calc.Sin(angleY),
        zCos = Calc.Cos(angleZ), zSin = Calc.Sin(angleZ);
      return new Matrix3x3(
        yCos * zCos, zCos * xSin * ySin - xCos * zSin, xCos * zCos * ySin + xSin * zSin,
        yCos * zSin, xCos * zCos + xSin * ySin * zSin, -zCos * xSin + xCos * ySin * zSin,
        -ySin, yCos * xSin, xCos * yCos);
    }

    /// <param name="shearXbyY">The shear along the X-axis in the Y-direction.</param>
    /// <param name="shearXbyZ">The shear along the X-axis in the Z-direction.</param>
    /// <param name="shearYbyX">The shear along the Y-axis in the X-direction.</param>
    /// <param name="shearYbyZ">The shear along the Y-axis in the Z-direction.</param>
    /// <param name="shearZbyX">The shear along the Z-axis in the X-direction.</param>
    /// <param name="shearZbyY">The shear along the Z-axis in the Y-direction.</param>
    public static Matrix3x3 FactoryShear(
      float shearXbyY, float shearXbyZ, float shearYbyX,
      float shearYbyZ, float shearZbyX, float shearZbyY)
    {
      return new Matrix3x3(
        1, shearYbyX, shearZbyX,
        shearXbyY, 1, shearYbyZ,
        shearXbyZ, shearYbyZ, 1);
    }

    public static Matrix3x3 operator +(Matrix3x3 left, Matrix3x3 right) { return left.Add(right); }
    public static Matrix3x3 operator -(Matrix3x3 left, Matrix3x3 right) { return left.Add(-right); }
    public static Matrix3x3 operator -(Matrix3x3 matrix) { return matrix.Negate(); }
    public static Matrix3x3 operator *(Matrix3x3 left, Matrix3x3 right) { return left.Multiply(right); }
    public static Vector operator *(Matrix3x3 matrix, Vector vector) { return matrix.Multiply(vector); }
    public static Matrix3x3 operator *(Matrix3x3 matrix, float scalar) { return matrix.Multiply(scalar); }
    public static Matrix3x3 operator /(Matrix3x3 matrix, float scalar) { return matrix.Divide(scalar); }
    public static Matrix3x3 operator ^(Matrix3x3 matrix, int power) { return matrix.Power(power); }
    public static implicit operator Matrix(Matrix3x3 matrix) { return Matrix3x3.ToMatrix(matrix); }
    public static implicit operator float[,](Matrix3x3 matrix) { return Matrix3x3.ToFloats(matrix); }

    public float Determinant
    {
      get
      {
        return
          _r0c0 * _r1c1 * _r2c2 -
          _r0c0 * _r1c2 * _r2c1 -
          _r0c1 * _r1c0 * _r2c2 +
          _r0c2 * _r1c0 * _r2c1 +
          _r0c1 * _r1c2 * _r2c0 -
          _r0c2 * _r1c1 * _r2c0;
      }
    }

    public bool EqualsApproximation(Matrix3x3 matrix, float tolerance)
    {
      return
        Calc.Abs(_r0c0 - matrix._r0c0) <= tolerance &&
        Calc.Abs(_r0c1 - matrix._r0c1) <= tolerance &&
        Calc.Abs(_r0c2 - matrix._r0c2) <= tolerance &&
        Calc.Abs(_r1c0 - matrix._r1c0) <= tolerance &&
        Calc.Abs(_r1c1 - matrix._r1c1) <= tolerance &&
        Calc.Abs(_r1c2 - matrix._r1c2) <= tolerance &&
        Calc.Abs(_r2c0 - matrix._r2c0) <= tolerance &&
        Calc.Abs(_r2c1 - matrix._r2c1) <= tolerance &&
        Calc.Abs(_r2c2 - matrix._r2c2) <= tolerance;
    }

    public Matrix3x3 Negate()
    {
      return new Matrix3x3(
        -_r0c0, -_r0c1, -_r0c2,
        -_r1c0, -_r1c1, -_r1c2,
        -_r2c0, -_r2c1, -_r2c2);
    }

    public Matrix3x3 Add(Matrix3x3 matrix)
    {
      return new Matrix3x3(
        _r0c0 + matrix._r0c0, _r0c1 + matrix._r0c1, _r0c2 + matrix._r0c2,
        _r1c0 + matrix._r1c0, _r1c1 + matrix._r1c1, _r1c2 + matrix._r1c2,
        _r2c0 + matrix._r2c0, _r2c1 + matrix._r2c1, _r2c2 + matrix._r2c2);
    }

    public Matrix3x3 Multiply(Matrix3x3 matrix)
    {
      return new Matrix3x3(
        matrix._r0c0 * _r0c0 + matrix._r0c1 * _r1c0 + matrix._r0c2 * _r2c0,
        matrix._r0c0 * _r0c1 + matrix._r0c1 * _r1c1 + matrix._r0c2 * _r2c1,
        matrix._r0c0 * _r0c2 + matrix._r0c1 * _r1c2 + matrix._r0c2 * _r2c2,
        matrix._r1c0 * _r0c0 + matrix._r1c1 * _r1c0 + matrix._r1c2 * _r2c0,
        matrix._r1c0 * _r0c1 + matrix._r1c1 * _r1c1 + matrix._r1c2 * _r2c1,
        matrix._r1c0 * _r0c2 + matrix._r1c1 * _r1c2 + matrix._r1c2 * _r2c2,
        matrix._r2c0 * _r0c0 + matrix._r2c1 * _r1c0 + matrix._r2c2 * _r2c0,
        matrix._r2c0 * _r0c1 + matrix._r2c1 * _r1c1 + matrix._r2c2 * _r2c1,
        matrix._r2c0 * _r0c2 + matrix._r2c1 * _r1c2 + matrix._r2c2 * _r2c2);
    }

    public Vector Multiply(Vector vector)
    {
      return new Vector(
        _r0c0 * vector.X + _r0c1 * vector.Y + _r0c2 * vector.Z,
        _r1c0 * vector.X + _r1c1 * vector.Y + _r1c2 * vector.Z,
        _r2c0 * vector.X + _r2c1 * vector.Y + _r2c2 * vector.Z);
    }

    public Matrix3x3 Multiply(float scalar)
    {
      return new Matrix3x3(
        scalar * _r0c0, scalar * _r0c1, scalar * _r0c2,
        scalar * _r1c0, scalar * _r1c1, scalar * _r1c2,
        scalar * _r2c0, scalar * _r2c1, scalar * _r2c2);
    }

    public Matrix3x3 Divide(float scalar)
    {
      return new Matrix3x3(
        _r0c0 / scalar, _r0c1 / scalar, _r0c2 / scalar,
        _r1c0 / scalar, _r1c1 / scalar, _r1c2 / scalar,
        _r2c0 / scalar, _r2c1 / scalar, _r2c2 / scalar);
    }

    public Matrix3x3 Power(int power)
    {
      if (power < 0)
        throw new MatrixException("Attempting to raise a matrix by a power less than zero. (can't do dat)");
      else if (power == 0)
        return FactoryIdentity;
      else
      {
        Matrix3x3 result = Clone();
        for (int i = 1; i < power; i++)
          result = result * result;
        return result;
      }
    }

    public Matrix3x3 Transpose()
    {
      return new Matrix3x3(
        _r0c0, _r1c0, _r2c0,
        _r0c1, _r1c1, _r2c1,
        _r0c2, _r1c1, _r2c2);
    }

    public Quaternion ToQuaternion()
    {
      float qX = ( _r0c0 + _r1c1 + _r2c2 + 1.0f) / 4.0f;
      float qY = ( _r0c0 - _r1c1 - _r2c2 + 1.0f) / 4.0f;
      float qZ = (-_r0c0 + _r1c1 - _r2c2 + 1.0f) / 4.0f;
      float qW = (-_r0c0 - _r1c1 + _r2c2 + 1.0f) / 4.0f;

      if (qX < 0.0f) qX = 0.0f;
      if (qY < 0.0f) qY = 0.0f;
      if (qZ < 0.0f) qZ = 0.0f;
      if (qW < 0.0f) qW = 0.0f;
      
      qX = Calc.SquareRoot(qX);
      qY = Calc.SquareRoot(qY);
      qZ = Calc.SquareRoot(qZ);
      qW = Calc.SquareRoot(qW);

      if (qX >= qY && qX >= qZ && qX >= qW)
      {
        qX *= +1.0f;
        qY *= Calc.Sin(_r2c1 - _r1c2);
        qZ *= Calc.Sin(_r0c2 - _r2c0);
        qW *= Calc.Sin(_r1c0 - _r0c1);
      }
      else if (qY >= qX && qY >= qZ && qY >= qW)
      {
        qX *= Calc.Sin(_r2c1 - _r1c2);
        qY *= +1.0f;
        qZ *= Calc.Sin(_r1c0 + _r0c1);
        qW *= Calc.Sin(_r0c2 + _r2c0);
      }
      else if (qZ >= qX && qZ >= qY && qZ >= qW)
      {
        qX *= Calc.Sin(_r0c2 - _r2c0);
        qY *= Calc.Sin(_r1c0 + _r0c1);
        qZ *= +1.0f;
        qW *= Calc.Sin(_r2c1 + _r1c2);
      }
      else if (qW >= qX && qW >= qY && qW >= qZ)
      {
        qX *= Calc.Sin(_r1c0 - _r0c1);
        qY *= Calc.Sin(_r2c0 + _r0c2);
        qZ *= Calc.Sin(_r2c1 + _r1c2);
        qW *= +1.0f;
      }
      else
        throw new MatrixException("There is a glitch in my my matrix to quaternion function. Sorry..");

      float length = Calc.SquareRoot(qX * qX + qY * qY + qZ * qZ + qW * qW);

      return new Quaternion(
        qX /= length,
        qY /= length,
        qZ /= length,
        qW /= length);
    }

    public Matrix3x3 Clone()
    {
      return new Matrix3x3(
        _r0c0, _r0c1, _r0c2,
        _r1c0, _r1c1, _r1c2,
        _r2c0, _r2c1, _r2c2);
    }

    public bool Equals(Matrix3x3 matrix)
    {
      return
        _r0c0 == matrix._r0c0 && _r0c1 == matrix._r0c1 && _r0c2 == matrix._r0c2 &&
        _r1c0 == matrix._r1c0 && _r1c1 == matrix._r1c1 && _r1c2 == matrix._r1c2 &&
        _r2c0 == matrix._r2c0 && _r2c1 == matrix._r2c1 && _r2c2 == matrix._r2c2;
    }

    public static Matrix ToMatrix(Matrix3x3 matrix)
    {
      Matrix result = new Matrix(3, 3);
      result[0, 0] = matrix._r0c0; result[0, 1] = matrix._r0c1; result[0, 2] = matrix._r0c2;
      result[1, 0] = matrix._r1c0; result[1, 1] = matrix._r1c1; result[1, 2] = matrix._r1c2;
      result[1, 0] = matrix._r2c0; result[2, 1] = matrix._r2c1; result[2, 2] = matrix._r2c2;
      return result;
    }

    public static float[,] ToFloats(Matrix3x3 matrix)
    {
      return new float[,]
        { { matrix[0, 0], matrix[0, 1], matrix[0, 2] },
        { matrix[1, 0], matrix[1, 1], matrix[1, 2] },
        { matrix[2, 0], matrix[2, 1], matrix[2, 2] } };
    }

    public override int GetHashCode()
    {
      return
        _r0c0.GetHashCode() ^ _r0c1.GetHashCode() ^ _r0c2.GetHashCode() ^
        _r1c0.GetHashCode() ^ _r1c1.GetHashCode() ^ _r1c2.GetHashCode() ^
        _r2c0.GetHashCode() ^ _r2c1.GetHashCode() ^ _r2c2.GetHashCode();
    }

    public override string ToString()
    {
      return base.ToString();
      //return
      //  _r0c0 + " " + _r0c1 + " " + _r0c2 + "\n" +
      //  _r1c0 + " " + _r1c1 + " " + _r1c2 + "\n" +
      //  _r2c0 + " " + _r2c1 + " " + _r2c2 + "\n";
    }

    private class MatrixException : Exception
    {
      public MatrixException(string message) : base(message) { }
    }

    #region Framework Dependent Convieniencies



    #endregion
  }
}