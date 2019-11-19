using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircularBuffer<T> : IEnumerable where T : IComparable<T> {

    private int _size;

    private T[] _buffer;

    private int _next;

    private int _first;

    public CircularBuffer(int size){
        _size = size;
        _buffer = new T[_size];
        _next = 0;
        _first = 0;
    }

    public void Push(T obj)
    {
        _buffer[_next] = obj;
        _next = (_next + 1) % _size;

        if (_next == _first)
            _first = (_first + 1) % _size;
    }

    public void Clear()
    {
        _buffer = new T[_size];
        _next = 0;
        _first = 0;
    }

    public int Count()
    {
        return (_next - _first + _size + 1) % (_size + 1);
    }

    public bool Empty()
    {
        return _first == _next;
    }

    public IEnumerator GetEnumerator()
    {
        int count = Count();
        for(int i = 0; i < count; i++)
        {
            yield return _buffer[(_first + i) % _size];
        }
    }

}
