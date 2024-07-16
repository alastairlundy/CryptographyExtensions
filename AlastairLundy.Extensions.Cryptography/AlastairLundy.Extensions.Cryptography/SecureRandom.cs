/*
        MIT License

       Copyright (c) 2024 Alastair Lundy

       Permission is hereby granted, free of charge, to any person obtaining a copy
       of this software and associated documentation files (the "Software"), to deal
       in the Software without restriction, including without limitation the rights
       to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
       copies of the Software, and to permit persons to whom the Software is
       furnished to do so, subject to the following conditions:

       The above copyright notice and this permission notice shall be included in all
       copies or substantial portions of the Software.

       THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
       IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
       FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
       AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
       LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
       OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
       SOFTWARE.
   */


using System.Security.Cryptography;
// ReSharper disable IntroduceOptionalParameters.Global

namespace AlastairLundy.Extensions.Cryptography;

/// <summary>
/// Represents a more secure Pseudo random number generator.
/// </summary>
public class SecureRandom
{

    /// <summary>
    /// Fills a Span with a given number of randomly selected items from the available choices.
    /// </summary>
    /// <param name="choices">The choices to select items from.</param>
    /// <param name="destination">The destination span to be modified.</param>
    /// <typeparam name="T">The type of items in the Span</typeparam>
    public void GetItems<T>(ReadOnlySpan<T> choices, Span<T> destination)
    {
        destination = GetItems(choices, destination.Length);
    }

    /// <summary>
    /// Fills an array with a given number of randomly selected items from the available choices.
    /// </summary>
    /// <param name="choices">The choices to select items from.</param>
    /// <param name="length">The number of items to be randomly selected.</param>
    /// <typeparam name="T">The type of items in the Span</typeparam>
    /// <returns>an array with a given number of randomly selected items from the available choices.</returns>
    public T[] GetItems<T>(ReadOnlySpan<T> choices, int length)
    {
        T[] output = new T[length];

        for (int i = 0; i < length; i++)
        {
            output[length] = choices[Next(0, choices.Length)];
        }

        return output;
    }

    /// <summary>
    /// Fills an array with a given number of randomly selected items from the available choices.
    /// </summary>
    /// <param name="choices">The choices to select items from.</param>
    /// <param name="length">The number of items to be randomly selected.</param>
    /// <typeparam name="T">The type of items in the array</typeparam>
    /// <returns>an array with a given number of randomly selected items from the available choices.</returns>
    public T[] GetItems<T>(T[] choices, int length)
    {
        T[] output = new T[length];

        for (int i = 0; i < length; i++)
        {
            output[length] = choices[Next(0, choices.Length)];
        }

        return output;
    }

    /// <summary>
    /// Randomly shuffles a span of items.
    /// </summary>
    /// <param name="values">The items to be shuffled.</param>
    /// <typeparam name="T">The type of item in the span.</typeparam>
    public void Shuffle<T>(Span<T> values)
    {
       values = values.ToArray().OrderBy(x => Next()).ToArray();
    }

    /// <summary>
    /// Randomly shuffles an array of items.
    /// </summary>
    /// <param name="values">The items to be shuffled.</param>
    /// <typeparam name="T">The type of item in the array.</typeparam>
    public void Shuffle<T>(T[] values)
    {
        values = values.OrderBy(x => Next()).ToArray();
    }
    
    /// <summary>
    /// Generates a random number as a byte.
    /// </summary>
    /// <returns></returns>
    public byte NextByte()
    {
        return RandomNumberGenerator.GetBytes(1)[0];
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="buffer"></param>
    /// <returns></returns>
    public Span<byte> NextBytes(Span<byte> buffer)
    {
        buffer = RandomNumberGenerator.GetBytes(buffer.Length);
        return buffer;
    }
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="buffer"></param>
    /// <returns></returns>
    public byte[] NextBytes(byte[] buffer)
    {
        buffer = RandomNumberGenerator.GetBytes(buffer.Length);

        return buffer;
    }

    /// <summary>
    /// Returns a non-negative random integer.
    /// </summary>
    /// <returns>a random integer between 0 and int.MaxValue .</returns>
    public int Next()
    {
        return RandomNumberGenerator.GetInt32(0, int.MaxValue);
    }

    /// <summary>
    /// Returns a random integer that is either non-negative (if upperBound is greater than 0) or negative (if upperBound is less than or equal to 0).
    /// </summary>
    /// <param name="maxValue">The maximum allowable value to be randomly generated.</param>
    /// <returns>the randomly generated 32 Bit integer.</returns>
    public int Next(int maxValue)
    {
        if (maxValue > 0)
        {
            return RandomNumberGenerator.GetInt32(0, maxValue);
        }
        else
        {
            return RandomNumberGenerator.GetInt32(int.MinValue, maxValue);
        }
    }

    /// <summary>
    /// Returns a random 32 Bit integer that is either non-negative (if upperBound is greater than 0) or negative (if upperBound is less than or equal to 0).
    /// </summary>
    /// <param name="minValue">The minimum allowable value to be randomly generated.</param>
    /// <param name="maxValue">The maximum allowable value to be randomly generated.</param>
    /// <returns>the randomly generated 32 Bit integer.</returns>
    public int Next(int minValue, int maxValue)
    {
        return RandomNumberGenerator.GetInt32(minValue, maxValue);
    }

    /// <summary>
    /// Returns a random 64 Bit integer that is non-negative.
    /// </summary>
    /// <returns>the randomly generated 64 Bit integer.</returns>
    public long NextInt64()
    {
        return NextInt64(0, long.MaxValue);
    }

    /// <summary>
    /// Returns a random 64 Bit integer that is either non-negative (if upperBound is greater than 0) or negative (if upperBound is less than or equal to 0).
    /// </summary>
    /// <param name="maxValue">The maximum allowable value to be randomly generated.</param>
    /// <returns>the randomly generated 64 Bit integer.</returns>
    public long NextInt64(long maxValue)
    {
        if (maxValue > 0)
        {
            return NextInt64(0, maxValue);
        }
        else
        {
            return NextInt64(long.MinValue, maxValue);
        }
    }
    
    /// <summary>
    /// Returns a random 64 Bit integer that is either non-negative (if upperBound is greater than 0) or negative (if upperBound is less than or equal to 0).
    /// </summary>
    /// <param name="minValue">The minimum allowable value to be randomly generated.</param>
    /// <param name="maxValue">The maximum allowable value to be randomly generated.</param>
    /// <returns>the randomly generated 64 Bit integer.</returns>
    public long NextInt64(long minValue, long maxValue)
    {
        return Convert.ToInt64(NextDouble(Convert.ToDouble(minValue), Convert.ToDouble(maxValue)));
    }
    
    /// <summary>
    /// Returns a double precision floating point number that is greater than or equal to 0.0 and less than or equal to 1.0.
    /// </summary>
    /// <returns>a double precision floating point number that is greater than or equal to 0.0 and less than or equal to 1.0.</returns>
    public double NextDouble()
    {
        double initialNumbers = Convert.ToDouble(RandomNumberGenerator.GetInt32(1, 100));

        return initialNumbers / 100.0;
    }

    /// <summary>
    /// Returns a double precision floating point number that is greater than or equal to 0.0 and less than or equal to 1.0.
    /// </summary>
    /// <param name="maxValue">The maximum allowable value to be randomly generated.</param>
    /// <returns>a double precision floating point number that is greater than or equal to 0.0 and less than or equal to 1.0.</returns>
    public double NextDouble(double maxValue)
    {
        if (maxValue > 0 || maxValue < 0)
        {
            return NextDouble() * maxValue;
        }
        else
        {
            return NextDouble() * int.MinValue;
        }
    }

    /// <summary>
    /// Returns a double precision floating point number that is greater than or equal to 0.0 and less than or equal to 1.0.
    /// </summary>
    /// <param name="minValue">The minimum allowable value to be randomly generated.</param>
    /// <param name="maxValue">The maximum allowable value to be randomly generated.</param>
    /// <returns>a double precision floating point number that is greater than or equal to 0.0 and less than or equal to 1.0.</returns>
    public double NextDouble(double minValue, double maxValue)
    {
        for (int i = 0; i < 1_000; i++)
        {
            double random;

            if (minValue < 0)
            {
                random = NextDouble() * double.MinValue;
            }
            else
            {
               random = NextDouble(maxValue);
            }

            if (random >= minValue && random <= maxValue)
            {
                return random;
            }
        }

        return Convert.ToDouble(NextInt64(Convert.ToInt64(minValue), Convert.ToInt64(maxValue)));
    }

    /// <summary>
    /// Returns a floating point number that is greater than or equal to 0.0 and less than or equal to 1.0.
    /// </summary>
    /// <returns>a floating point number that is greater than or equal to 0.0 and less than or equal to 1.0.</returns>
    public float NextSingle()
    {
        return Convert.ToSingle(NextDouble());
    }
    
    /// <summary>
    /// Returns a decimal that is greater than or equal to 0.0 and less than or equal to 1.0.
    /// </summary>
    /// <returns>a decimal that is greater than or equal to 0.0 and less than or equal to 1.0.</returns>
    public decimal NextDecimal()
    {
        decimal initialNumbers = Convert.ToDecimal(RandomNumberGenerator.GetInt32(1, 100));

        return decimal.Divide(initialNumbers, Convert.ToDecimal(100.0));
    }

    /// <summary>
    /// Returns a decimal that is greater than or equal to 0.0 and less than or equal to 1.0.
    /// </summary>
    /// <param name="maxValue">The maximum allowable value to be randomly generated.</param>
    /// <returns>a decimal that is greater than or equal to 0.0 and less than or equal to 1.0.</returns>
    public decimal NextDecimal(decimal maxValue)
    {
        if (maxValue > 0 || maxValue < 0)
        {
            return decimal.Multiply(NextDecimal(), maxValue);
        }
        else
        {
            return decimal.Multiply(NextDecimal(), decimal.MinValue);
        }
    }

    /// <summary>
    /// Returns a decimal that is greater than or equal to 0.0 and less than or equal to 1.0.
    /// </summary>
    /// <param name="minValue">The minimum allowable value to be randomly generated.</param>
    /// <param name="maxValue">The maximum allowable value to be randomly generated.</param>
    /// <returns>a decimal that is greater than or equal to 0.0 and less than or equal to 1.0.</returns>
    public decimal NextDecimal(decimal minValue, decimal maxValue)
    {
        for (int i = 0; i < 1_000; i++)
        {
            decimal random;

            if (minValue < 0)
            {
                random = decimal.Multiply(NextDecimal(), decimal.MinValue);
            }
            else
            {
                random = NextDecimal(maxValue);
            }

            if (random >= minValue && random <= maxValue)
            {
                return random;
            }
        }

        return Convert.ToDecimal(NextInt64(Convert.ToInt64(minValue), Convert.ToInt64(maxValue)));
    }
}