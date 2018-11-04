# VPKSoft.RandomizationUtils
Some randomization utilities which includes biased randomization, weighted randomization and some extensions for double randomization for the Random class.

## The biased randomization
The biased randomization randoms to either lower or higher end of the given randomization minimum and maxmimum values.

## The weighted randomization
The weighted randomization randomizes items with a given arbitrary weight number.

## The extensions methods
The extension methods to the Random class mostly extends the floating-point randomization to match the integer randomization overload method counterparts.

### Sample of the weighted randomization
```C#
  List<WeightedItem<string, double>> pairs = new List<WeightedItem<string, double>>();
  pairs.Add(new WeightedItem<string, double>() { Weight = 13, Value = "Bird" });
  pairs.Add(new WeightedItem<string, double>() { Weight = 2, Value = "Cat" });
  pairs.Add(new WeightedItem<string, double>() { Weight = 1, Value = "Dog" });
  pairs.Add(new WeightedItem<string, double>() { Weight = 2, Value = "Human(oid)" });
  pairs.Add(new WeightedItem<string, double>() { Weight = 0.25, Value = "Squirrel" });

  List<WeightedItem<string, double>> randomized = new List<WeightedItem<string, double>>();

  for (int i = 0; i < 1000000; i++)
  {
    WeightedItem<string, double> pair = WieghtedRandom.RandomWeighted(pairs);
    if (!randomized.Exists(f => f.Value == pair.Value))
    {
      randomized.Add(pair);
    }

    int idx = randomized.FindIndex(f => f.Value == pair.Value);
    randomized[idx].AdditionalData++;
  }

  double countTotal = randomized.Sum(f => f.AdditionalData);
  for (int i = 0; i < randomized.Count; i++)
  {
    randomized[i].AdditionalData = randomized[i].AdditionalData * 100 / countTotal;
  }

  foreach (WeightedItem<string, double> pair in randomized)
  {
    textBox1.Text += string.Format("{0}: {1:0.000}{2}", pair.Value, pair.AdditionalData, Environment.NewLine);
  }
```
The result after a million iterations:
```
Human(oid): 10.946
Bird:       71.185
Dog:         5.511
Cat:        10.977
Squirrel:    1.382
```
