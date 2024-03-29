﻿namespace Common.MediatR;

public interface IIdempotentCommand<out TResponse> : ICommand<TResponse>
{
    Guid RequestId { get; set; }
}