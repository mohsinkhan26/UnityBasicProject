/* 
 * Author : Mohsin Khan
 * Portfolio : http://mohsinkhan26.github.io/ 
 * LinkedIn : http://pk.linkedin.com/in/mohsinkhan26/
 * Github : https://github.com/mohsinkhan26/
*/

using System;
using System.Collections.Generic;

namespace MK.Common.Extensions
{
    public class StackExtension<T> : Stack<T>
    {
        public Stack<T> Stack;

        public int Count
        {
            get { return Stack.Count; }
        }

        public StackExtension() : base()
        {
            Stack = new Stack<T>();
        }

        // inserts an object at the top of the Stack.
        public void Push(T item, Action<T> afterPush = null)
        {
            Stack.Push(item);
            afterPush?.Invoke(item);
        }

        // removes and returns the object at the top of the Stack
        public T Pop(Action<T> afterPop = null)
        {
            T item = Stack.Pop();
            afterPop?.Invoke(item);
            return item;
        }

        // returns the object at the top of the Stack without removing it
        public T Peek(Action<T> afterPeek = null)
        {
            T item = Stack.Peek();
            afterPeek?.Invoke(item);
            return item;
        }

        public void Clear()
        {
            Stack.Clear();
        }
    }
}
