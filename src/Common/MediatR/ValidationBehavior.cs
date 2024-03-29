﻿using FluentValidation;
using FluentValidation.Results;
using MediatR;

namespace Common.MediatR;

public sealed class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
{
    private readonly IEnumerable<IValidator<TRequest>> _validators;

    public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators) => _validators = validators;

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        if (!_validators.Any())
        {
            return await next();
        }
        var context = new ValidationContext<TRequest>(request);
        var validationResults = new List<ValidationResult>();
        foreach ( var validator in _validators )
        {
            var result = await validator.ValidateAsync(context);
            validationResults.Add(result);
        }
        var errorsDictionary = validationResults
            .SelectMany(x => x.Errors)
            .Where(x => x != null)
            .GroupBy(
                x => x.PropertyName,
                x => x.ErrorMessage,
                (propertyName, errorMessages) => new
                {
                    Key = propertyName,
                    Values = errorMessages.Distinct().ToArray()
                })
            .ToDictionary(x => x.Key, x => x.Values);
        if (errorsDictionary.Any())
        {
            var firstError = errorsDictionary.First();
            throw new ValidationException(firstError.Key,
                firstError.Value.Select(e => new FluentValidation.Results.ValidationFailure() { ErrorMessage = e }));
        }
        return await next();
    }
}