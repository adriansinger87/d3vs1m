using System;
using System.Collections.Generic;
using System.Text;

namespace D3vS1m.Domain.IO
{
    public interface ICasteable
    {
        Tout CastTo<Tout, Tin>(Tin input) where Tout : new();
    }
}
