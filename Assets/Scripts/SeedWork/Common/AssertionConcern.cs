using System.Collections;
using System.Collections.Generic;
using System;

public abstract class AssertionConcern
{
    protected void assertArgumentEquals<T>(Object anObject1, Object anObject2, string aMessage)
    {
        if (!anObject1.Equals(anObject2)) throw new ArgumentException(aMessage);
    }

    protected void assertArgumentReferenceEquals<T>(Object anObject1, Object anObject2, string aMessage)
    {
        if (!ReferenceEquals(anObject1, anObject2)) throw new ArgumentException(aMessage);
    }

    protected void assertArgumentFalse(bool abool, string aMessage)
    {
        if (abool) throw new ArgumentException(aMessage);
    }

    protected void assertArgumentLength(string astring, int aMaximum, string aMessage)
    {
        int length = astring.Trim().Length;
        if (length > aMaximum) throw new ArgumentException(aMessage);
    }

    protected void assertArgumentLength(string astring, int aMinimum, int aMaximum, string aMessage)
    {
        int length = astring.Trim().Length;
        if (length < aMinimum || length > aMaximum) throw new ArgumentException(aMessage);
    }

    protected void assertArgumentNotEmpty(string astring, string aMessage)
    {
        if (astring == null || String.IsNullOrWhiteSpace(astring)) throw new ArgumentException(aMessage);
    }

    protected void assertArgumentNotEquals(Object anObject1, Object anObject2, string aMessage)
    {
        if (anObject1.Equals(anObject2)) throw new ArgumentException(aMessage);
    }

    protected void assertArgumentNotNull(Object anObject, string aMessage)
    {
        if (anObject == null) throw new ArgumentException(aMessage);
    }

    protected void assertArgumentRange(double aValue, double aMinimum, double aMaximum, string aMessage)
    {
        if (aValue < aMinimum || aValue > aMaximum) throw new ArgumentException(aMessage);
    }

    protected void assertArgumentRange(float aValue, float aMinimum, float aMaximum, string aMessage)
    {
        if (aValue < aMinimum || aValue > aMaximum) throw new ArgumentException(aMessage);
    }

    protected void assertArgumentRange(int aValue, int aMinimum, int aMaximum, string aMessage)
    {
        if (aValue < aMinimum || aValue > aMaximum) throw new ArgumentException(aMessage);
    }

    protected void assertArgumentRange(long aValue, long aMinimum, long aMaximum, string aMessage)
    {
        if (aValue < aMinimum || aValue > aMaximum) throw new ArgumentException(aMessage);
    }

    protected void assertArgumentTrue(bool abool, string aMessage)
    {
        if (!abool) throw new ArgumentException(aMessage);
    }

    protected void assertStateFalse(bool abool, string aMessage)
    {
        if (abool) throw new InvalidOperationException(aMessage);
    }

    protected void assertStateTrue(bool abool, string aMessage)
    {
        if (!abool) throw new InvalidOperationException(aMessage);
    }
}
