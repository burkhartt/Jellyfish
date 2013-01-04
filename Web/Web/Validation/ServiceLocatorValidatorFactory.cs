using System;
using System.Collections.Generic;
using System.Reflection;
using Autofac;
using FluentValidation;

namespace Web.Validation {
    public class ServiceLocatorValidatorFactory : IValidatorFactory {
        private readonly IComponentContext container;
        private readonly Dictionary<string, Type> validatorMap;

        public ServiceLocatorValidatorFactory(IComponentContext container) {
            this.container = container;
            var validators = AssemblyScanner.FindValidatorsInAssembly(Assembly.GetExecutingAssembly());
            validatorMap = new Dictionary<string, Type>();

            foreach (var validator in validators) {
                validatorMap[validator.InterfaceType.GetGenericArguments()[0].Name] = validator.ValidatorType;
            }
        }

        public IValidator<T> GetValidator<T>() {
            return (IValidator<T>) GetValidator(typeof (T));
        }

        public IValidator GetValidator(Type type) {
            if (validatorMap.ContainsKey(type.Name)) {
                return (IValidator) container.Resolve(validatorMap[type.Name]);
            }
            return null;
        }

        public IValidator CreateInstance(Type validatorType) {
            return container.Resolve(validatorType) as IValidator;
        }
    }
}