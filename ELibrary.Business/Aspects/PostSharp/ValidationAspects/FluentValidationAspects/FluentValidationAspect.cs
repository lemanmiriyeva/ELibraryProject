using System;
using System.Linq;
using ELibrary.Business.CrossCuttingConcerns.Validation;
using FluentValidation;
using PostSharp.Aspects;

namespace ELibrary.Business.Aspects.PostSharp.ValidationAspects.FluentValidationAspects
{
    [Serializable]
    public class FluentValidationAspect : OnMethodBoundaryAspect
    {
        private Type _validationType;

        public FluentValidationAspect(Type validationType)
        {
            _validationType = validationType;
        }

        public override void OnEntry(MethodExecutionArgs args)
        {
            var validator = (IValidator)Activator.CreateInstance(_validationType);

            var entityType = _validationType.BaseType.GetGenericArguments()[0];

            var entities = args.Arguments
                .Where(t => t.GetType() == entityType).ToList();

            foreach (var entity in entities)
            {
                ValidatorTool.FluentValidate(validator, entity);
            }
        }
    }
}
