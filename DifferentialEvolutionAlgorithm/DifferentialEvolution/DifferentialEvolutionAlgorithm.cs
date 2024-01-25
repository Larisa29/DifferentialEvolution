
namespace DifferentialEvolution
{
    public class DifferentialEvolutionAlgorithm
    {
        public Parameters Parameters { get; set; }
        private static Random _rand = new Random();

        public DifferentialEvolutionAlgorithm(Parameters parameters)
        {
            Parameters = parameters;
        }

        public Chromosome[] CreatePopulation(IOptimizationProblem problem, int populationSize)
        {
            //initializare populatie
            Chromosome[] population = new Chromosome[populationSize]; //vector de indivizi
            for (int i = 0; i < population.Length; i++)
            {
                //initializez cromozom, in functie de genele necesare problemei pe care o rezolv
                population[i] = problem.MakeChromosome();//initializez pe rand fiecare cromozom
                problem.ComputeFitness(population[i]);
            }
            return population;
        }

        public Chromosome Mutation(Chromosome[] population, double F)
        {
            Chromosome mutantChild;
            Random random = new Random();
            List<int> randomValuesList = new List<int>();
            while (randomValuesList.Count < 3)
            {
                int index = random.Next(0, population.Length - 1);
                if (!randomValuesList.Contains(index))
                    randomValuesList.Add(index);
            }
            int i1 = randomValuesList[0];
            int i2 = randomValuesList[1];
            int i3 = randomValuesList[2];

            // initializez cromozomii folositi pt. mutatie cu indivizii selectati random de mai sus
            Chromosome cr1 = population[i1];
            Chromosome cr2 = population[i2];
            Chromosome cr3 = population[i3];

            mutantChild = new Chromosome(cr1);

            //creare copil mutant
            for (int i = 0; i < mutantChild.NoGenes; i++)
            {
                mutantChild.Genes[i] = cr1.Genes[i] + F * (cr2.Genes[i] - cr3.Genes[i]); 
            }

            return mutantChild;
        }

        private void Crossover(Chromosome[] population, Chromosome child, Chromosome mutantChild, double CR)
        {
            Chromosome resultCrossover = new Chromosome(child.NoGenes);
            int punctDivizare = _rand.Next(1, population.Length - 1);//punct divizare

            for (int gene = 0; gene < child.NoGenes; gene++)
            {
                double k = _rand.NextDouble(); //numar aleatoriu uniform intre 0 si 1
                if (k < CR || gene == punctDivizare)
                {
                    resultCrossover.Genes[gene] = mutantChild.Genes[gene];//resultCrossover = trial vector
                }
                else
                {
                    resultCrossover.Genes[gene] = child.Genes[gene];
                }
            }
        }


        public void Solve(IOptimizationProblem problem, int maxGenerations, double CR, double F, int populationSize)
        {

            for (int generation = 0; generation < maxGenerations; generation++)
            {
                Chromosome[] population = CreatePopulation(problem, populationSize);

                List<Chromosome> newGeneration = new List<Chromosome>();
                foreach (var child in population.ToList())
                {
                    Chromosome mutantChild = Mutation(population, F);

                    Chromosome resultCrossover = new Chromosome(17);
                    Crossover(population, child, mutantChild, CR);

                    // selectie
                    resultCrossover.Evaluate(problem);
                    child.Evaluate(problem);
                    if (resultCrossover.Fitness < child.Fitness)
                        newGeneration.Add(resultCrossover);
                    else
                        newGeneration.Add(child);
                }

                // actualizeaza populatie
                population = newGeneration.ToArray();
                Chromosome best = GetBest(population);

                Console.Write("Cel mai adaptat individ din generatia {0} are valoarea fitness {1} si genele [{2}", generation, Math.Round(best.Fitness, 3), best.ToString());
                Console.WriteLine(']');
                Console.WriteLine("----------------------------------------------------------------------------------------------");
            }
        }

        public static Chromosome GetBest(Chromosome[] population)
        {
            //pp. ca elementul minim este primul din populatie
            double min = population[0].Fitness;
            //obiect nou, copie a celui mai bun individ
            Chromosome chromWithMinFitness = new Chromosome(population[0]);

            for (int i = 1; i < population.Length; i++)
            {
                if (population[i].Fitness < min)
                {
                    min = population[i].Fitness;
                    chromWithMinFitness = new Chromosome(population[i]);
                }
            }

            return chromWithMinFitness;
        }
    }
}
