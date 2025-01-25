using System;

namespace ProductManager.Exceptions;

public class NotFoundException : Exception
{
    public NotFoundException(string message) : base(message) { }
}