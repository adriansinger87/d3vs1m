﻿using System;
using System.Collections.Generic;
using System.Text;

namespace D3vS1m.Domain.Simulation
{
    public interface ISimulatable
    {
        void Prepare();
        void Execute();
    }
}
