// Copyright (c) Honua. All rights reserved.
// Licensed under the Apache License 2.0. See LICENSE in the project root.

namespace Honua.Core.Exceptions;

/// <summary>
/// Base exception for all Honua-related errors.
/// </summary>
public abstract class HonuaException : Exception
{
    /// <summary>
    /// Gets the error code associated with this exception.
    /// </summary>
    public virtual string? ErrorCode { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="HonuaException"/> class.
    /// </summary>
    protected HonuaException()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="HonuaException"/> class with a specified error message.
    /// </summary>
    /// <param name="message">The message that describes the error.</param>
    protected HonuaException(string message)
        : base(message)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="HonuaException"/> class with a specified error message and error code.
    /// </summary>
    /// <param name="message">The message that describes the error.</param>
    /// <param name="errorCode">The error code associated with this exception.</param>
    protected HonuaException(string message, string errorCode)
        : base(message)
    {
        ErrorCode = errorCode;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="HonuaException"/> class with a specified error message and a reference to the inner exception that is the cause of this exception.
    /// </summary>
    /// <param name="message">The error message that explains the reason for the exception.</param>
    /// <param name="innerException">The exception that is the cause of the current exception.</param>
    protected HonuaException(string message, Exception innerException)
        : base(message, innerException)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="HonuaException"/> class with a specified error message, error code, and a reference to the inner exception that is the cause of this exception.
    /// </summary>
    /// <param name="message">The error message that explains the reason for the exception.</param>
    /// <param name="errorCode">The error code associated with this exception.</param>
    /// <param name="innerException">The exception that is the cause of the current exception.</param>
    protected HonuaException(string message, string errorCode, Exception innerException)
        : base(message, innerException)
    {
        ErrorCode = errorCode;
    }
}

/// <summary>
/// Exception thrown when request validation fails.
/// Messages from this exception are considered safe to expose to clients.
/// </summary>
public sealed class ValidationException : HonuaException
{
    /// <summary>
    /// Additional validation error details that are safe to expose.
    /// </summary>
    public IReadOnlyList<string>? Details { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="ValidationException"/> class.
    /// </summary>
    public ValidationException()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ValidationException"/> class with a specified error message.
    /// </summary>
    /// <param name="message">The message that describes the error.</param>
    public ValidationException(string message)
        : base(message)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ValidationException"/> class with a specified error message and validation details.
    /// </summary>
    /// <param name="message">The message that describes the error.</param>
    /// <param name="details">Additional validation error details that are safe to expose to clients.</param>
    public ValidationException(string message, IReadOnlyList<string> details)
        : base(message)
    {
        Details = details;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ValidationException"/> class with a specified error message and a reference to the inner exception that is the cause of this exception.
    /// </summary>
    /// <param name="message">The error message that explains the reason for the exception.</param>
    /// <param name="innerException">The exception that is the cause of the current exception.</param>
    public ValidationException(string message, Exception innerException)
        : base(message, innerException)
    {
    }
}

/// <summary>
/// Exception thrown when a requested resource is not found.
/// </summary>
public sealed class ResourceNotFoundException : HonuaException
{
    /// <summary>
    /// Gets the type of resource that was not found.
    /// </summary>
    public string? ResourceType { get; }

    /// <summary>
    /// Gets the identifier of the resource that was not found.
    /// </summary>
    public object? ResourceId { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="ResourceNotFoundException"/> class.
    /// </summary>
    public ResourceNotFoundException()
        : base("The requested resource was not found.")
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ResourceNotFoundException"/> class with a specified error message.
    /// </summary>
    /// <param name="message">The message that describes the error.</param>
    public ResourceNotFoundException(string message)
        : base(message)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ResourceNotFoundException"/> class with resource details.
    /// </summary>
    /// <param name="resourceType">The type of resource that was not found.</param>
    /// <param name="resourceId">The identifier of the resource that was not found.</param>
    public ResourceNotFoundException(string resourceType, object resourceId)
        : base($"The {resourceType} with ID '{resourceId}' was not found.")
    {
        ResourceType = resourceType;
        ResourceId = resourceId;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ResourceNotFoundException"/> class with a specified error message and a reference to the inner exception that is the cause of this exception.
    /// </summary>
    /// <param name="message">The error message that explains the reason for the exception.</param>
    /// <param name="innerException">The exception that is the cause of the current exception.</param>
    public ResourceNotFoundException(string message, Exception innerException)
        : base(message, innerException)
    {
    }
}

/// <summary>
/// Exception thrown when there is a conflict with the current state of a resource.
/// </summary>
public sealed class ResourceConflictException : HonuaException
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ResourceConflictException"/> class.
    /// </summary>
    public ResourceConflictException()
        : base("There was a conflict with the current state of the resource.")
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ResourceConflictException"/> class with a specified error message.
    /// </summary>
    /// <param name="message">The message that describes the error.</param>
    public ResourceConflictException(string message)
        : base(message)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ResourceConflictException"/> class with a specified error message and a reference to the inner exception that is the cause of this exception.
    /// </summary>
    /// <param name="message">The error message that explains the reason for the exception.</param>
    /// <param name="innerException">The exception that is the cause of the current exception.</param>
    public ResourceConflictException(string message, Exception innerException)
        : base(message, innerException)
    {
    }
}

/// <summary>
/// Exception thrown when a service is temporarily unavailable.
/// </summary>
public sealed class ServiceUnavailableException : HonuaException
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ServiceUnavailableException"/> class.
    /// </summary>
    public ServiceUnavailableException()
        : base("The service is temporarily unavailable.")
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ServiceUnavailableException"/> class with a specified error message.
    /// </summary>
    /// <param name="message">The message that describes the error.</param>
    public ServiceUnavailableException(string message)
        : base(message)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ServiceUnavailableException"/> class with a specified error message and a reference to the inner exception that is the cause of this exception.
    /// </summary>
    /// <param name="message">The error message that explains the reason for the exception.</param>
    /// <param name="innerException">The exception that is the cause of the current exception.</param>
    public ServiceUnavailableException(string message, Exception innerException)
        : base(message, innerException)
    {
    }
}