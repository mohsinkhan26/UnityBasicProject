Which of the following examples will run faster?

1. 1000 GameObjects, each with a ```MonoBehaviour``` implementing the ```Update``` callback.
2. One GameObject with one ```MonoBehaviour``` with an Array of 1000 classes, each implementing a custom ```Update()``` callback.


### The correct answer is 2. ###

The ```Update``` callback is called using a C# Reflection, which is significantly slower than calling a function directly. In our example, 1000 GameObjects each with a ```MonoBehaviour``` means 1000 Reflection calls per frame.

Creating one ```MonoBehaviour``` with one Update, and using this single callback to Update a given number of elements, is a lot faster, due to the direct access to the method.


This folder's code is the implementation of it
