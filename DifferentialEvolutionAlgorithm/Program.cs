
using DEAlgorithm.FileData;

namespace DifferentialEvolution
{
    class Program
    {

        static void Main()
        {
            Parameters parameters = new Parameters
            {
                F = 0.5,
                CR = 0.9,
                ChromosomesCount = 20,
                Iterations = 100
            };

            DifferentialEvolutionAlgorithm de = new DifferentialEvolutionAlgorithm(parameters);

            de.Solve(new CRDP(), parameters.Iterations, parameters.CR, parameters.F, parameters.ChromosomesCount);
        }
    }

    public class CRDP : IOptimizationProblem
    {
        public CRDP() { }

        public Chromosome MakeChromosome()
        {
            return new Chromosome(17);
        }

        public void ComputeFitness(Chromosome cr)
        {
            double kcal = 0;
            ReadFromFile rf = new ReadFromFile();
            var kcalNutrients = rf.readKcal();
            var KC = kcalNutrients.Item1;
            var nutrientsValue = kcalNutrients.Item2;

            for (int i = 0; i < cr.NoGenes; i++)
            {
                kcal += KC[i] * cr.Genes[i];
            }

            var difference = Math.Abs(1200 - kcal);

            var minimumNutrientsValuePerDay = 80;  

            if (nutrientsValue.Sum() >= minimumNutrientsValuePerDay)
            {
                cr.Fitness = difference;
                return;
            }

            double penalty = 0.0;

            for (int i = 0; i < cr.NoGenes; i++)
            {
                penalty += nutrientsValue[i] * cr.Genes[i];
            }

            
             cr.Fitness = difference + (Math.Abs(penalty - minimumNutrientsValuePerDay) / minimumNutrientsValuePerDay);
        }
    }
}
