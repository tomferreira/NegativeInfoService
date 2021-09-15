using NegativeInfoService.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace NegativeInfoService.Application.Exceptions
{
    public class EntityNotFoundException : BusinessRuleException
    {
        public EntityNotFoundException()
            : base("Entity", "")
        {
        }
}
}
