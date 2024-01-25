namespace DEAlgorithm.FileData;

public class ReadFromFile
{
    private static int index;
    private string[] fileLines;

    public Tuple<double[], double[]> readKcal()
    {
        string[] symbolsGenes = { "B", "F", "P", "F", "C", "P", "B", "L", "Lg", "Lg", "C", "B", "C", "P", "Lg", "F", "Lg" };
        Random _rand = new Random();
        double[] kcals = new double[17];
        double[] nutrients = new double[17];
        var kcalNutrients = new Tuple<double[], double[]>(kcals, nutrients);

        for (int i = 0; i < symbolsGenes.Length; i++)
        {
            var columnName = symbolsGenes[i];
            var fileName = ".\\" + columnName + ".txt";
            fileLines = File.ReadAllLines(fileName);
            string[] kcList = new string[fileLines.Length - 1];
            int k = 0;
            for (var j = 1; j < fileLines.Length; j++)
            {
                var lines = fileLines[j].Split(" ");
                var kc = lines[2];
                kcList[k] = kc;
                k++;
            }

            index = _rand.Next(fileLines.Length - 1);
            kcals[i] = double.Parse(kcList[index]);
            nutrients[i] = getAllNutrientsValueForProduct();
        }
        return kcalNutrients;
    }

    public double getAllNutrientsValueForProduct()
    {
        double res = 0.0;
        string product = fileLines[index + 1];
        double Mg = double.Parse(product.Split(" ")[3]);
        var Ca = double.Parse(product.Split(" ")[4]);
        res += Mg + Ca;
        return res;
    }
}
