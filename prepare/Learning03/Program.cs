using System;

class Program{
    static void Main(string[] args){
        Fraction frac1 = new Fraction();
        Console.WriteLine(frac1.GetFracString());
        Console.WriteLine(frac1.GetDecValue());

        Fraction frac2 = new Fraction(5);
        Console.WriteLine(frac2.GetFracString());
        Console.WriteLine(frac2.GetDecValue());

        Fraction frac3 = new Fraction(3, 4);
        Console.WriteLine(frac3.GetFracString());
        Console.WriteLine(frac3.GetDecValue());

        Fraction frac4 = new Fraction(1, 3);
        Console.WriteLine(frac4.GetFracString());
        Console.WriteLine(frac4.GetDecValue());
    }
}