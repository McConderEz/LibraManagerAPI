﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraManager.Domain.ValueObjects
{
    public abstract class ValueObject
    {
        protected abstract IEnumerable<object> GetEqualityComponents();

        public override bool Equals(object? obj)
        {
            if(obj == null)
                return false;

            if(GetType() != obj.GetType())
                return false;

            var valueObject = (ValueObject)obj;

            return GetEqualityComponents().SequenceEqual(valueObject.GetEqualityComponents());
        }

        public override int GetHashCode() =>
            GetEqualityComponents().Aggregate(default(int), (hashcode, value) => 
                                    HashCode.Combine(hashcode, value.GetHashCode()));

        public static bool operator ==(ValueObject? a, ValueObject? b)
        {
            if (a is null && b is null)
                return true;

            if (a is null || b is null)
                return false;

            return a.Equals(b);
        }

        public static bool operator !=(ValueObject a, ValueObject b) => !(a == b);

    }
}
