using System;
using System.Text;

namespace DifferentialEvolution;

public class Chromosome
{
    public int NoGenes { get; set; } // numarul de gene ale individului
    public double Fitness { get; set; }
    public double[] Genes { get; set; } // valorile genelor

    public static double minBound = 0.5;
    public static double maxBound = 3;
    private static Random _rand = new Random();

    public Chromosome(Chromosome c) // constructor de copiere
    {
        NoGenes = c.NoGenes;
        Fitness = c.Fitness;

        Genes = new double[c.NoGenes];

        for (int i = 0; i < c.Genes.Length; i++)
        {
            Genes[i] = c.Genes[i];
        }
    }

    public Chromosome(int noGenes)
    {
        NoGenes = noGenes;
        Genes = new double[noGenes];

        for (int i = 0; i < noGenes ; i++)
        {
            Genes[i] = _rand.NextDouble() * (maxBound - minBound) + minBound;
        }
    }

    public void Evaluate(IOptimizationProblem problem)
    {
        problem.ComputeFitness(this);
    }

    public override string ToString()
    {
        StringBuilder s = new StringBuilder();
        foreach (var item in Genes)
        {
            s.Append(Math.Round(item, 4) + ", ");
        }
        s.Remove(s.Length - 2, 2);
        return s.ToString();
    }
}
