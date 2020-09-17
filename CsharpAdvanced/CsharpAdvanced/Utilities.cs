using System;
using Microsoft.VisualBasic;

namespace CsharpAdvanced
{
    //EX. use of 'new()' adds a second constraint
    public class Utilities<T> where T : IComparable, new()
    {
        //5 Constraints...
        //where T : IComparable ...specifies that T must implement the IComparable interface
        //where T : Product ...specifies that T must be of a given type or any of its subclasses i.e. 'Product'
        //where T : struct (value type) ...specifies that T must be a value type
        //where T : class (reference type) ...specifies that T must be a reference type
        //where T : new() ...specifies that T must have a default constructor

        public void DoSomething(T value)
        {
            //in order to instantiate T, we need to add a second constraint... 'new()'
            var obj = new T();
        }

        public int Max(int a, int b)
        {
            return a > b ? a : b;
        }

        public T Max(T a, T b)
        {
            return a.CompareTo(b) > 0 ? a : b;
        }
    }
}