using System;

namespace DifferentialEvolution;

public class Parameters
{
    public double F { get; set; }
    public double CR { get; set; }
    public int ChromosomesCount { get; set; }
    public int Iterations { get; set; }

    public Parameters()
    {
        F = 0.8;
        CR = 0.9;
        ChromosomesCount = 20;
        Iterations = 100;
    }
}
